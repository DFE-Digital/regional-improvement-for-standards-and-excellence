
using DfE.ManageSchoolImprovement.Frontend.Services;
using static DfE.ManageSchoolImprovement.Frontend.Services.DateRangeValidationService;

namespace DfE.ManageSchoolImprovement.Frontend.Tests.Services
{
    public class DateRangeValidationServiceTests
    {
        public static IEnumerable<object[]> DateRangeTestCases()
        {
            yield return new object[] { new DateTime(2022, 1, 1), DateRange.Past, true, "" };
            yield return new object[] { DateTime.Now.AddYears(1), DateRange.Past, false, "You must enter a date in the past" };
            yield return new object[] { new DateTime(2022, 1, 1), DateRange.PastOrToday, true, "" };
            yield return new object[] { DateTime.Now.AddYears(1), DateRange.PastOrToday, false, "You must enter today's date or a date in the past" };
            yield return new object[] { new DateTime(2022, 1, 1), DateRange.Future, false, "You must enter a date in the future" };
            yield return new object[] { DateTime.Now.AddYears(1), DateRange.Future, true, "" };
            yield return new object[] { new DateTime(2022, 1, 1), DateRange.FutureOrToday, false, "You must enter today's date or a date in the future" };
            yield return new object[] { DateTime.Now.AddYears(1), DateRange.FutureOrToday, true, "" };
            yield return new object[] { new DateTime(2022, 1, 1), DateRange.PastOrFuture, true, "" };
        }

        // Use MemberData to inject the data into the test
        [Theory]
        [MemberData(nameof(DateRangeTestCases))]
        public void Validate_ShouldReturnCorrectResult(DateTime date, DateRange dateRange, bool expectedIsValid, string expectedMessage)
        {
            // Act
            var (isValid, message) = Validate(date, dateRange, "Test");

            // Assert
            Assert.Equal(expectedIsValid, isValid);
            Assert.Equal(expectedMessage, message);
        }

        [Fact]
        public void Validate_PastRange_ShouldReturnErrorForFutureDate()
        {
            // Arrange
            var futureDate = DateTime.Today.AddDays(1);

            // Act
            var (isValid, message) = Validate(futureDate, DateRange.Past, "Test");

            // Assert
            Assert.False(isValid);
            Assert.Equal("You must enter a date in the past", message);
        }

        [Fact]
        public void Validate_PastOrTodayRange_ShouldReturnErrorForFutureDate()
        {
            // Arrange
            var futureDate = DateTime.Today.AddDays(1);

            // Act
            var (isValid, message) = Validate(futureDate, DateRange.PastOrToday, "Test");

            // Assert
            Assert.False(isValid);
            Assert.Equal("You must enter today's date or a date in the past", message);
        }

        [Fact]
        public void Validate_FutureRange_ShouldReturnErrorForPastDate()
        {
            // Arrange
            var pastDate = DateTime.Today.AddDays(-1);

            // Act
            var (isValid, message) = Validate(pastDate, DateRange.Future, "Test");

            // Assert
            Assert.False(isValid);
            Assert.Equal("You must enter a date in the future", message);
        }

        [Fact]
        public void Validate_FutureOrTodayRange_ShouldReturnErrorForPastDate()
        {
            // Arrange
            var pastDate = DateTime.Today.AddDays(-1);

            // Act
            var (isValid, message) = Validate(pastDate, DateRange.FutureOrToday, "Test");

            // Assert
            Assert.False(isValid);
            Assert.Equal("You must enter today's date or a date in the future", message);
        }

        [Fact]
        public void Validate_PastOrFutureRange_ShouldAlwaysReturnValid()
        {
            // Arrange
            var anyDate = DateTime.Today;

            // Act
            var (isValid, message) = Validate(anyDate, DateRange.PastOrFuture, "Test");

            // Assert
            Assert.True(isValid);
            Assert.Equal("", message);
        }
    }
}
