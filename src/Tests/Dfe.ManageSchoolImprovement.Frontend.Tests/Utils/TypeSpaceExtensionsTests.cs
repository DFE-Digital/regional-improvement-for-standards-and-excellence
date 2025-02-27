using Dfe.ManageSchoolImprovement.Utils;
using Microsoft.AspNetCore.Html;

namespace Dfe.ManageSchoolImprovement.Frontend.Tests.Utils
{
    public class TypeSpaceExtensionsTests
    {
        [Theory]
        [InlineData("Hello World!", "hello-world-")]
        [InlineData("Test_123", "test_123")]
        [InlineData("Space Test", "space-test")]
        [InlineData("!@#$%^&*()", "----------")]
        [InlineData("1234abcXYZ", "1234abcxyz")]
        public void Stub_ShouldReplaceNonAlphaNumericWithDash(string input, string expected)
        {
            // Act
            HtmlString result = input.Stub();

            // Assert
            Assert.Equal(expected, result.ToString());
        }

        [Fact]
        public void Stub_ShouldHandleEmptyString()
        {
            // Act
            HtmlString result = "".Stub();

            // Assert
            Assert.Equal("", result.ToString());
        }
    }
}
