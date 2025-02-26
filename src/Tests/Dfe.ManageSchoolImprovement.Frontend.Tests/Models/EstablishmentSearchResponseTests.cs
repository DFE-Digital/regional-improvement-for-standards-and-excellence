using Dfe.ManageSchoolImprovement.Frontend.Models;

namespace Dfe.ManageSchoolImprovement.Frontend.Tests.Models
{
    public class EstablishmentSearchResponseTests
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly_WhenValidParametersProvided()
        {
            // Arrange
            var name = "Test School";
            var urn = "123456";
            var ukprn = "654321";

            // Act
            var establishmentSearchResponse = new EstablishmentSearchResponse
            {
                Name = name,
                Urn = urn,
                Ukprn = ukprn
            };

            // Assert
            Assert.Equal(name, establishmentSearchResponse.Name);
            Assert.Equal(urn, establishmentSearchResponse.Urn);
            Assert.Equal(ukprn, establishmentSearchResponse.Ukprn);
        }
    }
}
