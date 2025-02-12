using Dfe.ManageSchoolImprovement.Frontend.Models;

namespace Dfe.ManageSchoolImprovement.Frontend.Tests.Models
{
    public class AzureAdOptionsTests
    {
        [Fact]
        public void AzureAdOptions_ShouldSetAndGetPropertiesCorrectly()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var clientSecret = "TestSecret";
            var tenantId = Guid.NewGuid();
            var groupId = Guid.NewGuid();
            var apiUrl = "https://custom-api.com/";

            // Act
            var options = new AzureAdOptions
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                TenantId = tenantId,
                GroupId = groupId,
                ApiUrl = apiUrl
            };

            // Assert
            Assert.Equal(clientId, options.ClientId);
            Assert.Equal(clientSecret, options.ClientSecret);
            Assert.Equal(tenantId, options.TenantId);
            Assert.Equal(groupId, options.GroupId);
            Assert.Equal(apiUrl, options.ApiUrl);
        }

        [Fact]
        public void AzureAdOptions_Authority_ShouldReturnCorrectFormat()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var expectedAuthority = $"https://login.microsoftonline.com/{tenantId}";

            var options = new AzureAdOptions { TenantId = tenantId };

            // Act
            var authority = options.Authority;

            // Assert
            Assert.Equal(expectedAuthority, authority);
        }

        [Fact]
        public void AzureAdOptions_DefaultApiUrl_ShouldBeGraphMicrosoft()
        {
            // Arrange & Act
            var options = new AzureAdOptions();

            // Assert
            Assert.Equal("https://graph.microsoft.com/", options.ApiUrl);
        }

        [Fact]
        public void AzureAdOptions_Scopes_ShouldContainApiUrlDefault()
        {
            // Arrange
            var options = new AzureAdOptions();

            // Act
            var scopes = options.Scopes;

            // Assert
            Assert.Contains("https://graph.microsoft.com/.default", scopes);
        }

        [Fact]
        public void AzureAdOptions_Scopes_ShouldAdaptToCustomApiUrl()
        {
            // Arrange
            var customApiUrl = "https://custom-api.com/";
            var options = new AzureAdOptions { ApiUrl = customApiUrl };

            // Act
            var scopes = options.Scopes;

            // Assert
            Assert.Contains($"{customApiUrl}.default", scopes);
        }
    }
}
