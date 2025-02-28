using Dfe.ManageSchoolImprovement.Frontend.Services.AzureAd;
using Dfe.ManageSchoolImprovement.Frontend.Services.Http;
using Dfe.ManageSchoolImprovement.Frontend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Dfe.Academisation.CorrelationIdMiddleware;
using Dfe.ManageSchoolImprovement.Frontend.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Net;

namespace Dfe.ManageSchoolImprovement.Frontend.Tests
{
    public class ProgramTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        [Fact]
        public void Test_Service_Configuration()
        {
            using var scope = factory.Services.CreateScope();
            var services = scope.ServiceProvider;

            Assert.NotNull(services.GetService<IHttpClientFactory>());
            Assert.NotNull(services.GetService<IHttpContextAccessor>());
            Assert.NotNull(services.GetService<ErrorService>());
            Assert.NotNull(services.GetService<IDfeHttpClientFactory>());
            Assert.NotNull(services.GetService<IGetEstablishment>());
            Assert.NotNull(services.GetService<ICorrelationContext>());
            Assert.NotNull(services.GetService<IHttpClientService>());
            Assert.NotNull(services.GetService<IGraphClientFactory>());
            Assert.NotNull(services.GetService<IGraphUserService>());
            Assert.NotNull(services.GetService<IUserRepository>());
            Assert.NotNull(services.GetService<IAuthorizationHandler>()); 
        }

        [Fact]
        public async Task Test_Application_Startup()
        {
            var client = factory.CreateClient();
            var response = await client.GetAsync("/");
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }

        [Fact]
        public async Task Test_Authentication_Required_For_Secured_Pages()
        {
            var client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            var response = await client.GetAsync("/secured-page");
            Assert.Equal(System.Net.HttpStatusCode.Redirect, response.StatusCode);
            Assert.Contains("login.microsoftonline.com", response.Headers.Location?.ToString());
        }

        [Fact]
        public async Task Test_Health_Check()
        {
            var client = factory.CreateClient();
            var response = await client.GetAsync("/health");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/", HttpStatusCode.Found)]
        [InlineData("/public/maintenance", HttpStatusCode.OK)]
        [InlineData("/public/accessibilitystatement", HttpStatusCode.Found)]
        [InlineData("/public/cookiepreferences", HttpStatusCode.Found)]
        [InlineData("/AccessDenied", HttpStatusCode.Found)]
        [InlineData("/Privacy", HttpStatusCode.OK)]
        public async Task Test_RazorPages_Accessibility(string url, HttpStatusCode httpStatusCode)
        {
            var client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
            var response = await client.GetAsync(url);
            Assert.Equal(httpStatusCode, response.StatusCode);
        }
    }
}
