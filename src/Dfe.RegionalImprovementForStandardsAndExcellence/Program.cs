using Microsoft.AspNetCore.CookiePolicy;
using Dfe.Academisation.CorrelationIdMiddleware;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services.AzureAd;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services.Http;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Authorization;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Security;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add User Secrets in Development
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

var config = builder.Configuration;

var _authenticationExpiration = TimeSpan.FromMinutes(int.Parse(config["AuthenticationExpirationInMinutes"] ?? "60"));

builder.Services.Configure<AzureAdOptions>(config.GetSection("AzureAd"));

builder.Services.AddHttpClient(DfeHttpClientFactory.AcademiesClientName, (sp, client) =>
{
    var academiesApiSection = config.GetSection("AcademiesApi");
    client.BaseAddress = new Uri(academiesApiSection["Url"]);
    client.DefaultRequestHeaders.Add("ApiKey", academiesApiSection["ApiKey"]);
    client.DefaultRequestHeaders.Add("User-Agent", "RISE/1.0");

});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = new TimeSpan(10000);
    options.Cookie.Name = ".RISE.Session";
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;

    if (string.IsNullOrWhiteSpace(config["CI"]))
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddScoped(sp => sp.GetService<IHttpContextAccessor>()?.HttpContext?.Session);
// Add services to the container.
builder.Services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizeFolder("/");
            options.Conventions.AllowAnonymousToPage("/public/maintenance");
            options.Conventions.AllowAnonymousToPage("/public/accessibilitystatement");
            options.Conventions.AllowAnonymousToPage("/public/cookiepreferences");
        })
        .AddViewOptions(options =>
        {
            options.HtmlHelperOptions.ClientValidationEnabled = false;
        }).AddMvcOptions(options =>
        {
            options.MaxModelValidationErrors = 50;
        });

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApp(options =>
{
    builder.Configuration.Bind("AzureAd", options);

    options.Events = new OpenIdConnectEvents
    {
        OnRemoteFailure = context =>
        {
            // Log error details
            Console.WriteLine("Authentication failure: " + context.Failure?.Message);
            if (context.Failure?.InnerException != null)
            {
                Console.WriteLine("Inner exception: " + context.Failure.InnerException.Message);
            }

            // Redirect to a custom error page or show the error
            context.Response.Redirect("/Account/Error");
            context.HandleResponse();
            return Task.CompletedTask;
        },

        OnTokenValidated = context =>
        {
            // Log claims received
            var claims = context.Principal?.Claims;
            Console.WriteLine("User claims:");
            foreach (var claim in claims ?? Enumerable.Empty<System.Security.Claims.Claim>())
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }

            return Task.CompletedTask;
        },

        OnRedirectToIdentityProvider = context =>
        {
            // Log the details of the redirect request
            Console.WriteLine("Redirecting to Identity Provider");
            Console.WriteLine($"Redirect URI: {context.ProtocolMessage.RedirectUri}");
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthorization(options => { options.DefaultPolicy = SetupAuthorizationPolicyBuilder().Build(); });

AuthorizationPolicyBuilder SetupAuthorizationPolicyBuilder()
{
    AuthorizationPolicyBuilder policyBuilder = new();
    policyBuilder.RequireAuthenticatedUser();

    string allowedRoles = config.GetSection("AzureAd")["AllowedRoles"];
    if (string.IsNullOrWhiteSpace(allowedRoles) is false)
    {
        policyBuilder.RequireClaim(ClaimTypes.Role, allowedRoles.Split(','));
    }

    return policyBuilder;
}

// Enforce HTTPS in ASP.NET Core
// @link https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?
builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(365);
});

builder.Services.Configure<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme,
   options =>
   {
       options.AccessDeniedPath = "/access-denied";
       options.Cookie.Name = ".RISE.Login";
       options.Cookie.HttpOnly = true;
       options.Cookie.IsEssential = true;
       options.ExpireTimeSpan = _authenticationExpiration;
       options.SlidingExpiration = true;

       if (string.IsNullOrEmpty(config["CI"]))
           options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
   });

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ErrorService>();
builder.Services.AddScoped<IDfeHttpClientFactory, DfeHttpClientFactory>();
builder.Services.AddScoped<IGetEstablishment, EstablishmentService>();
builder.Services.Decorate<IGetEstablishment, GetEstablishmentItemCacheDecorator>();
builder.Services.AddScoped<ICorrelationContext, CorrelationContext>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IGraphClientFactory, GraphClientFactory>();
builder.Services.AddScoped<IGraphUserService, GraphUserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddSingleton<IAuthorizationHandler, HeaderRequirementHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

builder.Services.AddApplicationDependencyGroup(builder.Configuration);
builder.Services.AddInfrastructureDependencyGroup(builder.Configuration);

var app = builder.Build();

var forwardOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All,
    RequireHeaderSymmetry = false
};
forwardOptions.KnownNetworks.Clear();
forwardOptions.KnownProxies.Clear();
app.UseForwardedHeaders(forwardOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseSecurityHeaders(SecurityHeadersDefinitions.GetHeaderPolicyCollection(app.Environment.IsDevelopment()));
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseCookiePolicy(new CookiePolicyOptions { Secure = CookieSecurePolicy.Always, HttpOnly = HttpOnlyPolicy.Always });
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", context =>
    {
        context.Response.Redirect("schools-requiring-improvement", false);
        return Task.CompletedTask;
    });
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute("default", "{controller}/{action}/");
});

app.UseHealthChecks("/health");

app.Run();

public partial class Program { } // Make the Program class partial for testing
