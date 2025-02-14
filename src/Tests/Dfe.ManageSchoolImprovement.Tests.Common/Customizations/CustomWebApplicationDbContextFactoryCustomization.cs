using AutoFixture;
using DfE.CoreLibs.Testing.Mocks.Authentication;
using DfE.CoreLibs.Testing.Mocks.WebApplicationFactory;
using Dfe.ManageSchoolImprovement.Api;
using Dfe.ManageSchoolImprovement.Infrastructure.Database;
using Dfe.ManageSchoolImprovement.Tests.Common.Seeders;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Dfe.ManageSchoolImprovement.Tests.Common.Customizations
{
    public class CustomWebApplicationDbContextFactoryCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<CustomWebApplicationDbContextFactory<Program>>(composer => composer.FromFactory(() =>
            {

                var factory = new CustomWebApplicationDbContextFactory<Program>()
                {
                    SeedData = new Dictionary<Type, Action<DbContext>>
                    {
                        { typeof(RegionalImprovementForStandardsAndExcellenceContext), context => SclContextSeeder.Seed((RegionalImprovementForStandardsAndExcellenceContext)context) },
                    },
                    ExternalServicesConfiguration = services =>
                    {
                        services.PostConfigure<AuthenticationOptions>(options =>
                        {
                            options.DefaultAuthenticateScheme = "TestScheme";
                            options.DefaultChallengeScheme = "TestScheme";
                        });

                        services.AddAuthentication("TestScheme")
                            .AddScheme<AuthenticationSchemeOptions, MockJwtBearerHandler>("TestScheme", options => { });
                    },
                    ExternalHttpClientConfiguration = client =>
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "external-mock-token");
                    }
                };

                var client = factory.CreateClient();

                var config = new ConfigurationBuilder()
                    .AddInMemoryCollection(new Dictionary<string, string?>
                    {
                        { "ApiClient:BaseUrl", client.BaseAddress!.ToString() }
                    })
                    .Build();

                var services = new ServiceCollection();
                services.AddSingleton<IConfiguration>(config);
                //services.AddApiClient<ISchoolsClient, SchoolsClient>(config, client);

                var serviceProvider = services.BuildServiceProvider();

                fixture.Inject(factory);
                fixture.Inject(serviceProvider);
                fixture.Inject(client);
                //fixture.Inject(serviceProvider.GetRequiredService<ISchoolsClient>());
                fixture.Inject(new List<Claim>());

                return factory;
            }));
        }
    }
}
