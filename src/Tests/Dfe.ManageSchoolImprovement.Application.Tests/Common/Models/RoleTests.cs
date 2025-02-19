using Dfe.ManageSchoolImprovement.Application.Common.Models;

namespace Dfe.ManageSchoolImprovement.Application.Tests.Common.Models
{
    public class RoleTests
    {
        [Fact]
        public void Support_ReturnsCorrectRole()
        {
            // Arrange & Act
            var result = Role.Support;

            // Assert
            Assert.Equal("msi.support", result);
        }
        [Fact]
        public void Default_ReturnsCorrectRole()
        {
            // Arrange & Act
            var result = Role.Default;

            // Assert
            Assert.Equal("msi.edit", result);
        }
    }
}
