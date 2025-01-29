
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.SupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.ViewModels;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Tests.ViewModels
{
    public class TaskStatusViewModelTests
    {
        public static readonly TheoryData<bool, bool, bool, DateTime?, TaskListStatus> ContactedTheSchoolTaskStatusCases = new()
        {
            {false, false, false, null, TaskListStatus.NotStarted },
            {false, true, false, null, TaskListStatus.InProgress},
            {true, true, true, DateTime.Now, TaskListStatus.Complete }
        };

        [Theory, MemberData(nameof(ContactedTheSchoolTaskStatusCases))]
        public void ContactedTheSchoolTaskStatusShouldReturnCorrectStatus(bool attachRiseInfoToEmail, bool findSchoolEmailAddress, bool useTheNotificationLetterToCreateEmail, DateTime? contactedTheSchoolDate, TaskListStatus expectedTaskListStatus)
        {
            // Arrange
            var supportProjectModel = SupportProjectViewModel.Create(new SupportProjectDto(1, DateTime.Now, "SchoolName", "23434", "LocalAuthority", "Region", null!, null!, findSchoolEmailAddress, useTheNotificationLetterToCreateEmail,
                attachRiseInfoToEmail!, contactedTheSchoolDate, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null!,null,null,null));

            //Action 
            var taskListStatus = TaskStatusViewModel.ContactedTheSchoolTaskStatus(supportProjectModel);

            //Assert
            Assert.Equal(expectedTaskListStatus, taskListStatus);
        }

        public static readonly TheoryData<bool?, bool?, DateTime?, TaskListStatus> RecordTheSchoolResponseTaskStatusCases = new()
        {
            {null, null, null, TaskListStatus.NotStarted },
            {false, false, null, TaskListStatus.InProgress },
            {false, true, null, TaskListStatus.InProgress},
            {true, true, DateTime.Now, TaskListStatus.Complete }
        };
          
        [Theory, MemberData(nameof(RecordTheSchoolResponseTaskStatusCases))]
        public void RecordTheSchoolResponseTaskStatusShouldReturnCorrectStatus(bool? hasSavedSchoolResponseinSharePoint, bool? hasAcceeptedTargetedSupport, DateTime? schoolResponseDate, TaskListStatus expectedTaskListStatus)
        {
            // Arrange
            var supportProjectModel = SupportProjectViewModel.Create(new SupportProjectDto(1, DateTime.Now, "SchoolName", "23434", "LocalAuthority", "Region", null!, null!, false, false,
                false, null, null, null, null, null, schoolResponseDate, hasAcceeptedTargetedSupport, hasSavedSchoolResponseinSharePoint, null, null, null, null, null, null, null, null, null, null, null, null, null, null!,null,null,null));

            //Action 
            var taskListStatus = TaskStatusViewModel.RecordTheSchoolResponseTaskStatus(supportProjectModel);

            //Assert
            Assert.Equal(expectedTaskListStatus, taskListStatus);
        }

        public static readonly TheoryData<bool?, bool?, DateTime?, TaskListStatus> SendIntroductoryEmailTaskListStatusCases = new()
        {
            {null, null, null, TaskListStatus.NotStarted },
            {false, false, null, TaskListStatus.InProgress },
            {false, true, null, TaskListStatus.InProgress},
            {true, true, DateTime.Now, TaskListStatus.Complete }
        };

        [Theory, MemberData(nameof(SendIntroductoryEmailTaskListStatusCases))]
        public void SendIntroductoryEmailTaskListStatusShouldReturnCorrectStatus(bool? hasShareEmailTemplateWithAdvisor, bool? remindAdvisorToCopyRiseTeamWhenSentEmail, DateTime? introductoryEmailSentDate, TaskListStatus expectedTaskListStatus)
        {
            // Arrange
            var supportProjectModel = SupportProjectViewModel.Create(new SupportProjectDto(1, DateTime.Now, "SchoolName", "23434", "LocalAuthority", "Region", null!, null!, false, false,
                false, null, null, null, null, null, null, null, null, null, null, introductoryEmailSentDate, hasShareEmailTemplateWithAdvisor, remindAdvisorToCopyRiseTeamWhenSentEmail, null, null, null, null, null, null, null, null, null!,null,null,null));

            //Action 
            var taskListStatus = TaskStatusViewModel.SendIntroductoryEmailTaskListStatus(supportProjectModel);

            //Assert
            Assert.Equal(expectedTaskListStatus, taskListStatus);
        }

        public static readonly TheoryData<bool?, bool?, DateTime?, TaskListStatus> CompleteAndSaveAssessmentTemplateTaskListStatusCases = new()
        {
            {null, null, null, TaskListStatus.NotStarted },
            {false, false, null, TaskListStatus.InProgress },
            {false, true, null, TaskListStatus.InProgress},
            {true, true, DateTime.Now, TaskListStatus.Complete }
        };

        [Theory, MemberData(nameof(CompleteAndSaveAssessmentTemplateTaskListStatusCases))]
        public void CompleteAndSaveAssessmentTemplateTaskListStatusShouldReturnCorrectStatus(bool? hasTalkToAdviserAboutFindings, bool? hasCompleteAssessmentTemplate, DateTime? savedAssessmentTemplateInSharePointDate, TaskListStatus expectedTaskListStatus)
        {
            // Arrange
            var supportProjectModel = SupportProjectViewModel.Create(new SupportProjectDto(1, DateTime.Now, "SchoolName", "23434", "LocalAuthority", "Region", null!, null!, false, false,
                false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, savedAssessmentTemplateInSharePointDate, hasTalkToAdviserAboutFindings, hasCompleteAssessmentTemplate, null, null, null, null, null!,null,null,null));

            //Action 
            var taskListStatus = TaskStatusViewModel.CompleteAndSaveAssessmentTemplateTaskListStatus(supportProjectModel);

            //Assert
            Assert.Equal(expectedTaskListStatus, taskListStatus);
        }

        public static readonly TheoryData<bool?, bool?, DateTime?, TaskListStatus> NoteOfVsistTaskListStatusCases = new()
        {
            {null, null, null, TaskListStatus.NotStarted },
            {false, false, null, TaskListStatus.InProgress },
            {false, true, null, TaskListStatus.InProgress},
            {true, true, DateTime.Now, TaskListStatus.Complete }
        };

        [Theory, MemberData(nameof(NoteOfVsistTaskListStatusCases))]
        public void NoteOfVsistTaskListStatusShouldReturnCorrectStatus(bool?askTheAdviserToSendYouTheirNotes, bool? giveTheAdviserTheNoteOfVisitTemplate, DateTime? dateNoteOfVisitSavedInSharePoint, TaskListStatus expectedTaskListStatus)
        {
            // Arrange
            var supportProjectModel = SupportProjectViewModel.Create(new SupportProjectDto(1, DateTime.Now, "SchoolName", "23434", "LocalAuthority", "Region", null!, null!, false, false,
                false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, giveTheAdviserTheNoteOfVisitTemplate, askTheAdviserToSendYouTheirNotes, dateNoteOfVisitSavedInSharePoint, null, null!,null,null,null));

            //Action 
            var taskListStatus = TaskStatusViewModel.NoteOfVsistTaskListStatus(supportProjectModel);

            //Assert
            Assert.Equal(expectedTaskListStatus, taskListStatus);
        }

        public static readonly TheoryData<bool?, bool?, bool?, DateTime?, TaskListStatus> CheckThePotentialAdviserConflictsOfInterestTaskListStatusCases = new()
        {
            {null, null, null, null, TaskListStatus.NotStarted },
            {false, false, false, null, TaskListStatus.InProgress },
            {false, true, true, null, TaskListStatus.InProgress},
            {true, true, true, DateTime.Now, TaskListStatus.Complete }
        };

        [Theory, MemberData(nameof(CheckThePotentialAdviserConflictsOfInterestTaskListStatusCases))]
        public void CheckThePotentialAdviserConflictsOfInterestTaskListStatusShouldReturnCorrectStatus(bool? sendConflictOfInterestFormToProposedAdviserAndTheSchool, bool? receiveCompletedConflictOfInterestForm, bool? saveCompletedConflictOfinterestFormInSharePoint, DateTime? dateConflictsOfInterestWereChecked, TaskListStatus expectedTaskListStatus)
        {
            // Arrange
            var supportProjectModel = SupportProjectViewModel.Create(new SupportProjectDto(1, DateTime.Now, "SchoolName", "23434", "LocalAuthority", "Region", null!, null!, false, false,
                false, null, sendConflictOfInterestFormToProposedAdviserAndTheSchool, receiveCompletedConflictOfInterestForm, saveCompletedConflictOfinterestFormInSharePoint, dateConflictsOfInterestWereChecked, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null!,null,null,null));

            //Action 
            var taskListStatus = TaskStatusViewModel.CheckThePotentialAdviserConflictsOfInterestTaskListStatus(supportProjectModel);

            //Assert
            Assert.Equal(expectedTaskListStatus, taskListStatus);
        }

        public static readonly TheoryData<string?, DateTime?, TaskListStatus> CheckAssignAdviserTaskListStatusCases = new()
        {
            {null, null, TaskListStatus.NotStarted },
            {"test@email.com", null, TaskListStatus.InProgress},
            {"test@email.com", DateTime.Now, TaskListStatus.Complete }
        };

        [Theory, MemberData(nameof(CheckAssignAdviserTaskListStatusCases))]
        public void CheckAssignAdviserTaskListStatusShouldReturnCorrectStatus(string? adviserEmailAddress, DateTime? dateAdviserAssigned, TaskListStatus expectedTaskListStatus)
        {
            // Arrange
            var supportProjectModel = SupportProjectViewModel.Create(new SupportProjectDto(1, DateTime.Now, "SchoolName", "23434", "LocalAuthority", "Region", null!, null!, false, false,
                false, null, null, null, null, null, null, null, null, dateAdviserAssigned, adviserEmailAddress, null, null, null, null, null, null, null, null, null, null, null, null!,null,null,null));

            //Action 
            var taskListStatus = TaskStatusViewModel.CheckAssignAdviserTaskListStatus(supportProjectModel);

            //Assert
            Assert.Equal(expectedTaskListStatus, taskListStatus);
        }

        public static readonly TheoryData<DateTime?, TaskListStatus> AdviserVisitToSchoolTaskListStatusCases = new()
        {
            {null, TaskListStatus.NotStarted }, 
            {DateTime.Now, TaskListStatus.Complete }
        };

        [Theory, MemberData(nameof(AdviserVisitToSchoolTaskListStatusCases))]
        public void AdviserVisitToSchoolTaskListStatusShouldReturnCorrectStatus(DateTime? adviserVisitDate, TaskListStatus expectedTaskListStatus)
        {
            // Arrange
            var supportProjectModel = SupportProjectViewModel.Create(new SupportProjectDto(1, DateTime.Now, "SchoolName", "23434", "LocalAuthority", "Region", null!, null!, false, false,
                false, null, null, null, null, null, null, null, null, null, null, null, null, null, adviserVisitDate, null, null, null, null, null, null, null, null!,null,null,null));

            //Action 
            var taskListStatus = TaskStatusViewModel.AdviserVisitToSchoolTaskListStatus(supportProjectModel);

            //Assert
            Assert.Equal(expectedTaskListStatus, taskListStatus);
        }

        public static readonly TheoryData<DateTime?, TaskListStatus> RecordVisitDateToVisitSchoolTaskListStatusCases = new()
        {
            {null, TaskListStatus.NotStarted },
            {DateTime.Now, TaskListStatus.Complete }
        };

        [Theory, MemberData(nameof(RecordVisitDateToVisitSchoolTaskListStatusCases))]
        public void RecordVisitDateToVisitSchoolTaskListStatusShouldReturnCorrectStatus(DateTime? schoolVisitDate, TaskListStatus expectedTaskListStatus)
        {
            // Arrange
            var supportProjectModel = SupportProjectViewModel.Create(new SupportProjectDto(1, DateTime.Now, "SchoolName", "23434", "LocalAuthority", "Region", null!, null!, false, false,
                false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, schoolVisitDate, null!,null,null,null));

            //Action 
            var taskListStatus = TaskStatusViewModel.RecordVisitDateToVisitSchoolTaskListStatus(supportProjectModel);

            //Assert
            Assert.Equal(expectedTaskListStatus, taskListStatus);
        }
        
        public static readonly TheoryData<DateTime?,string?,string?, TaskListStatus> ChoosePreferredSupportOrganisationTaskListStatusCases = new()
        {
            {null,null,null,TaskListStatus.NotStarted},
            {DateTime.Now,"name","Id",TaskListStatus.Complete },
            {DateTime.Now,"name",null,TaskListStatus.InProgress }
        };

        [Theory, MemberData(nameof(ChoosePreferredSupportOrganisationTaskListStatusCases))]
        public void ChoosePreferredSupportOrganisationShouldReturnCorrectStatus(DateTime? dateSupportOrganisationChosen,string? organisationName,string? organisationId, TaskListStatus expectedTaskListStatus)
        {
            // Arrange
            var supportProjectModel = SupportProjectViewModel.Create(new SupportProjectDto(1, DateTime.Now, "SchoolName", "23434", "LocalAuthority", "Region", null!, null!, false, false,
                false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, dateSupportOrganisationChosen,organisationName,organisationId,null));

            //Action 
            var taskListStatus = TaskStatusViewModel.ChoosePreferredSupportingOrganisationTaskListStatus(supportProjectModel);

            //Assert
            Assert.Equal(expectedTaskListStatus, taskListStatus);
        }
    }
}
