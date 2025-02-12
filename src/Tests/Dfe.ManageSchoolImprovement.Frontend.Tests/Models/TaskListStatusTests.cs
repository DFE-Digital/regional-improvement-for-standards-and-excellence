using Dfe.ManageSchoolImprovement.Frontend.Models;

namespace Dfe.ManageSchoolImprovement.Frontend.Tests.Models
{
    public class TaskListStatusTests
    {
        [Fact]
        public void TaskListStatus_ShouldHaveCorrectValues()
        {
            // Assert
            Assert.Equal(0, (int)TaskListStatus.NotStarted);
            Assert.Equal(1, (int)TaskListStatus.InProgress);
            Assert.Equal(2, (int)TaskListStatus.Complete);
        }

        [Theory]
        [InlineData(TaskListStatus.NotStarted, "NotStarted")]
        [InlineData(TaskListStatus.InProgress, "InProgress")]
        [InlineData(TaskListStatus.Complete, "Complete")]
        public void TaskListStatus_ShouldHaveExpectedNames(TaskListStatus status, string expectedName)
        {
            // Assert
            Assert.Equal(expectedName, status.ToString());
        }
    }
}
