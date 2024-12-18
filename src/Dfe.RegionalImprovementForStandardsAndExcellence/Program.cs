
using System.Collections.Specialized;
using Microsoft.AspNetCore.CookiePolicy;

using Dfe.Academisation.CorrelationIdMiddleware;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services.AzureAd;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph.ExternalConnectors;


var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

builder.Services.Configure<AzureAdOptions>(builder.Configuration.GetSection("AzureAd"));



builder.Services.AddHttpClient(DfeHttpClientFactory.AcademiesClientName, (sp, client) =>
{
    client.BaseAddress = new Uri(config["AcademiesApi:Url"]);
    client.DefaultRequestHeaders.Add("ApiKey", config["AcademiesApi:ApiKey"]);
    client.DefaultRequestHeaders.Add("User-Agent", "PrepareConversions/1.0");

});


builder.Services.AddSession(options =>
{
    options.IdleTimeout = new TimeSpan(10000);
    options.Cookie.Name = ".ManageAnAcademyConversion.Session";
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;

    //if (string.IsNullOrWhiteSpace(Configuration["CI"]))
      //  options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddScoped(sp => sp.GetService<IHttpContextAccessor>()?.HttpContext?.Session);
// Add services to the container.
builder.Services.AddRazorPages();
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

builder.Services.AddApplicationDependencyGroup(builder.Configuration);
builder.Services.AddInfrastructureDependencyGroup(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

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

app.Run();