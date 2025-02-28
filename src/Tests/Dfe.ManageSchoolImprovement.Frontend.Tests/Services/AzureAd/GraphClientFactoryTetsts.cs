using Dfe.ManageSchoolImprovement.Frontend.Models; 
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Moq;
using System.Net.Http.Headers;
using GraphClientFactory = Dfe.ManageSchoolImprovement.Frontend.Services.AzureAd.GraphClientFactory;

namespace Dfe.ManageSchoolImprovement.Frontend.Tests.Services.AzureAd
{
    public class GraphClientFactoryTests
    {
        private readonly Mock<IOptions<AzureAdOptions>> _mockAzureAdOptions;
        private readonly AzureAdOptions _azureAdOptions;
        private readonly GraphClientFactory _graphClientFactory;

        public GraphClientFactoryTests()
        {
            _azureAdOptions = new AzureAdOptions
            {
                ClientId = Guid.NewGuid(),
                ClientSecret = "test-client-secret",
                TenantId = Guid.NewGuid(),
                GroupId = Guid.NewGuid(),
                ApiUrl = "https://graph.microsoft.com"
            };

            _mockAzureAdOptions = new Mock<IOptions<AzureAdOptions>>();
            _mockAzureAdOptions.Setup(opt => opt.Value).Returns(_azureAdOptions);

            _graphClientFactory = new GraphClientFactory(_mockAzureAdOptions.Object);
        }

        [Fact]
        public void Create_ShouldReturnGraphServiceClient()
        {
            // Act
            GraphServiceClient client = _graphClientFactory.Create();

            // Assert
            Assert.NotNull(client);
            Assert.Equal($"{_azureAdOptions.ApiUrl}/V1.0", client.BaseUrl);
        }

        [Fact(Skip ="Required to fix access token issue")]
        public async Task Create_ShouldAuthenticateRequest()
        {
            // Arrange
            GraphServiceClient client = _graphClientFactory.Create();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me");

            // Simulate the authentication process
            var app = ConfidentialClientApplicationBuilder.Create(_azureAdOptions.ClientId.ToString())
                .WithClientSecret(_azureAdOptions.ClientSecret)
                .WithAuthority(new Uri(_azureAdOptions.Authority))
                .Build();

            var result = await app.AcquireTokenForClient(_azureAdOptions.Scopes).ExecuteAsync();

            // Act
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            await client.AuthenticationProvider.AuthenticateRequestAsync(requestMessage);

            // Assert
            Assert.NotNull(requestMessage.Headers.Authorization);
            Assert.Equal("Bearer", requestMessage.Headers.Authorization.Scheme);
            Assert.False(string.IsNullOrWhiteSpace(requestMessage.Headers.Authorization.Parameter));
        }
    }
}
