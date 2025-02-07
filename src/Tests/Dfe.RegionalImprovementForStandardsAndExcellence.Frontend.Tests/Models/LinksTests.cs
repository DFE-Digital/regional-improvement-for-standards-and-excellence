using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using System.Reflection;
using static Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.Links;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Tests.Models
{
    public class LinksTests
    {
        public LinksTests()
        {
            var fieldInfo = typeof(Links).GetField("_links", BindingFlags.NonPublic | BindingFlags.Static); 
            var links = (List<LinkItem>)fieldInfo?.GetValue(null)!; 
            links.Clear();
        }

        [Fact]
        public void AddLinkItem_ShouldAddNewLinkItem()
        {
            // Act
            var link = AddLinkItem("/newpage", "Back");

            // Assert
            Assert.IsType<LinkItem>(link);
            Assert.Equal("/newpage", link.Page);
            Assert.Equal("Back", link.BackText);
        }

        [Fact]
        public void AddLinkItem_WithoutBackText_ShouldAddNewLinkItem()
        {
            // Act
            var link = AddLinkItem("/newpage");

            // Assert
            Assert.IsType<LinkItem>(link);
            Assert.Equal("/newpage", link.Page);
            Assert.Equal("Back", link.BackText);
        }

        [Fact]
        public void ByPage_ShouldReturnCorrectLinkItem()
        {
            // Arrange
            AddLinkItem("/testpage", "Back");

            // Act
            var link = ByPage("/testpage");

            // Assert
            Assert.NotNull(link);
            Assert.Equal("/testpage", link.Page);
            Assert.Equal("Back", link.BackText);
        }

        [Fact]
        public void ByPage_WithoutBackText_ShouldReturnCorrectLinkItem()
        {
            // Arrange
            AddLinkItem("/testpage");

            // Act
            var link = ByPage("/testpage");

            // Assert
            Assert.NotNull(link);
            Assert.Equal("/testpage", link.Page);
            Assert.Equal("Back", link.BackText);
        }

        [Fact]
        public void ByPage_ShouldReturnNullIfPageDoesNotExist()
        {
            // Arrange
            AddLinkItem("/existingpage", "Back");

            // Act
            var link = ByPage("/nonexistentpage");

            // Assert
            Assert.Null(link);
        }

        [Fact]
        public void InializeProjectDocumentsEnabled_ShouldSetIsApplicationDocumentsEnabledFlag()
        {
            // Act
            InializeProjectDocumentsEnabled(true);

            // Assert
            Assert.True(IsApplicationDocumentsEnabled);

            // Act
            InializeProjectDocumentsEnabled(false);

            // Assert
            Assert.False(IsApplicationDocumentsEnabled);
        }

        [Fact]
        public void AddLinkItem_ShouldAddLinkItemsForPublicLinks()
        {
            // Act
            var accessibilityLink = Public.Accessibility;
            var cookiePreferencesLink = Public.CookiePreferences;
            var cookiePreferencesURLLink = Public.CookiePreferencesURL;

            // Assert
            Assert.Equal("/Public/AccessibilityStatement", accessibilityLink.Page);
            Assert.Equal("/Public/CookiePreferences", cookiePreferencesLink.Page);
            Assert.Equal("/public/cookie-Preferences", cookiePreferencesURLLink.Page);
        }

        [Theory]
        [InlineData("/TaskList/Index", "Back")]
        [InlineData("/TaskList/ContactTheSchool/ContactTheSchool", "Back")]
        [InlineData("/TaskList/RecordTheSchoolResponse/Index", "Back")]
        [InlineData("/TaskList/AdviserConflictOfInterest/AdviserConflictOfInterest", "Back")]
        [InlineData("/TaskList/AssignAdviser/AssignAdviser", "Back")]
        [InlineData("/TaskList/SendIntroductoryEmail/Index", "Back")]
        [InlineData("/TaskList/ArrangeAdviserVisitToSchool/ArrangeAdviserVisitToSchool", "Back")]
        [InlineData("/TaskList/CompleteAndSaveAssessmentTemplate/Index", "Back")]
        [InlineData("/TaskList/NoteOfVisit/NoteOfVisit", "Back")]
        [InlineData("/TaskList/RecordVisitDateToVisitSchool/Index", "Back")]
        [InlineData("/TaskList/ChoosePreferredSupportingOrganisation/Index", "Back")]
        [InlineData("/TaskList/RecordSupportDecision/Index", "Back")]
        [InlineData("/TaskList/AddSupportingOrganisationContactDetails/Index", "Back")]
        [InlineData("/TaskList/DueDiligenceOnPreferredSupportingOrganisation/Index", "Back")]
        [InlineData("/TaskList/RecordSupportingOrganisationAppointment/Index", "Back")]
        [InlineData("/TaskList/ShareTheImprovementPlanTemplate/Index", "Back")]
        [InlineData("/TaskList/RecordImprovementPlanDecision/Index", "Back")]
        [InlineData("/TaskList/SendAgreedImprovementPlanForApproval/Index", "Back")]
        [InlineData("/TaskList/RequestPlanningGrantOfferLetter/Index", "Back")]
        [InlineData("/TaskList/ReviewTheImprovementPlan/Index", "Back")]
        [InlineData("/TaskList/RequestImprovementGrantOfferLetter/Index", "Back")]
        public void TaskList_Items_ShouldHaveCorrectValues(string expectedPage, string expectedBackText)
        {
            // Arrange & Act
            var taskListType = typeof(TaskList);
            var properties = taskListType.GetFields();

            LinkItem? foundItem = null;

            foreach (var prop in properties)
            {
                if (prop.GetValue(null) is LinkItem item && item.Page == expectedPage)
                {
                    foundItem = item;
                    break;
                }
            }

            // Assert
            Assert.NotNull(foundItem);
            Assert.Equal(expectedPage, foundItem.Page);
            Assert.Equal(expectedBackText, foundItem.BackText);
        }

        [Fact]
        public void AddSchool_WhichSchoolNeedsHelp_ShouldHaveCorrectValues()
        {
            // Arrange & Act
            var linkItem = AddSchool.WhichSchoolNeedsHelp;

            // Assert
            Assert.NotNull(linkItem);
            Assert.Equal("/AddSchool/WhichSchoolNeedsHelp", linkItem.Page);
            Assert.Equal("Back", linkItem.BackText);
        }

        [Fact]
        public void AddSchool_Summary_ShouldHaveCorrectValues()
        {
            // Arrange & Act
            var linkItem = AddSchool.Summary;

            // Assert
            Assert.NotNull(linkItem);
            Assert.Equal("/AddSchool/Summary", linkItem.Page);
            Assert.Equal("Back", linkItem.BackText);
        }

        [Fact]
        public void SchoolList_Index_ShouldHaveCorrectValues()
        {
            // Arrange & Act
            var linkItem = SchoolList.Index;

            // Assert
            Assert.NotNull(linkItem);
            Assert.Equal("/SchoolList/Index", linkItem.Page);
            Assert.Equal("Back", linkItem.BackText);
        }

        [Fact]
        public void AboutTheSchool_Index_ShouldHaveCorrectValues()
        {
            // Arrange & Act
            var linkItem = AboutTheSchool.Index;

            // Assert
            Assert.NotNull(linkItem);
            Assert.Equal("/AboutTheSchool/Index", linkItem.Page);
            Assert.Equal("Back", linkItem.BackText);
        }

        [Fact]
        public void Notes_Index_ShouldHaveCorrectValues()
        {
            // Arrange & Act
            var linkItem = Notes.Index;

            // Assert
            Assert.NotNull(linkItem);
            Assert.Equal("/Notes/Index", linkItem.Page);
            Assert.Equal("Back", linkItem.BackText);
        }

        [Fact]
        public void Notes_NewNote_ShouldHaveCorrectValues()
        {
            // Arrange & Act
            var linkItem = Notes.NewNote;

            // Assert
            Assert.NotNull(linkItem);
            Assert.Equal("/Notes/NewNote", linkItem.Page);
            Assert.Equal("Back", linkItem.BackText);
        }

        [Fact]
        public void Notes_EditNote_ShouldHaveCorrectValues()
        {
            // Arrange & Act
            var linkItem = Notes.EditNote;

            // Assert
            Assert.NotNull(linkItem);
            Assert.Equal("/Notes/EditNote", linkItem.Page);
            Assert.Equal("Back", linkItem.BackText);
        }

        [Fact]
        public void ProjectAssignment_Index_ShouldHaveCorrectValues()
        {
            // Arrange & Act
            var linkItem = ProjectAssignment.Index;

            // Assert
            Assert.NotNull(linkItem);
            Assert.Equal("/ProjectAssignment/Index", linkItem.Page);
            Assert.Equal("Back", linkItem.BackText);
        }
    }
}
