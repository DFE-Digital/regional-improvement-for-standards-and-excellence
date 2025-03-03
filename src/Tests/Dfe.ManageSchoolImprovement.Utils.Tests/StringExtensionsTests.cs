namespace Dfe.ManageSchoolImprovement.Utils.Tests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("", true)]
        [InlineData(" ", true)]
        [InlineData("test", false)]
        public void IsEmpty_Should_Return_CorrectValue(string input, bool expected)
        {
            Assert.Equal(expected, input.IsEmpty());
        }

        [Theory]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("test", true)]
        public void IsPresent_Should_Return_CorrectValue(string input, bool expected)
        {
            Assert.Equal(expected, input.IsPresent());
        }

        [Theory]
        [InlineData("john.doe@EDUCATION.GOV.UK", "john Doe")]
        [InlineData("user@example.com", "user@example.com")]
        public void FullNameFromEmail_Should_Return_CorrectName(string email, string expected)
        {
            Assert.Equal(expected, email.FullNameFromEmail());
        }

        [Theory]
        [InlineData("property.name", "property_name")]
        [InlineData("[property]", "_property_")]
        public void ToHtmlName_Should_Convert_Correctly(string input, string expected)
        {
            Assert.Equal(expected, input.ToHtmlName());
        }
    }
}
