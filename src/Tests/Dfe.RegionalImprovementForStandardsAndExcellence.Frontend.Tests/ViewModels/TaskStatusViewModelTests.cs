using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;
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
            var supportProjectModel = CreateSupportProjectViewModel(findSchoolEmailAddress: findSchoolEmailAddress, useTheNotificationLetterToCreateEmail: useTheNotificationLetterToCreateEmail,
                attachRiseInfoToEmail: attachRiseInfoToEmail!, contactedTheSchoolDate: contactedTheSchoolDate);

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
            var supportProjectModel = CreateSupportProjectViewModel(schoolResponseDate: schoolResponseDate, hasAcceeptedTargetedSupport: hasAcceeptedTargetedSupport, hasSavedSchoolResponseinSharePoint: hasSavedSchoolResponseinSharePoint);

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
        public void SendIntroductoryEmailTaskListStatusShouldReturnCorrectStatus(bool? hasShareEmailTemplateWithAdvisor, bool? remindAdvisorToCopyRiseTeamWhenSentEmail,
            DateTime? introductoryEmailSentDate, TaskListStatus expectedTaskListStatus)
        {
            // Arrange
            var supportProjectModel = CreateSupportProjectViewModel(introductoryEmailSentDate: introductoryEmailSentDate, hasShareEmailTemplateWithAdvisor: hasShareEmailTemplateWithAdvisor,
                remindAdvisorToCopyRiseTeamWhenSentEmail: remindAdvisorToCopyRiseTeamWhenSentEmail);

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
        public void CompleteAndSaveAssessmentTemplateTaskListStatusShouldReturnCorrectStatus(bool? hasTalkToAdviserAboutFindings, bool? hasCompleteAssessmentTemplate,
            DateTime? savedAssessmentTemplateInSharePointDate, TaskListStatus expectedTaskListStatus)
        {
            // Arrange
            var supportProjectModel = CreateSupportProjectViewModel(savedAssessmentTemplateInSharePointDate: savedAssessmentTemplateInSharePointDate, hasTalkToAdviserAboutFindings: hasTalkToAdviserAboutFindings,
                hasCompleteAssessmentTemplate: hasCompleteAssessmentTemplate);

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
        public void NoteOfVsistTaskListStatusShouldReturnCorrectStatus(bool? askTheAdviserToSendYouTheirNotes, bool? giveTheAdviserTheNoteOfVisitTemplate, DateTime? dateNoteOfVisitSavedInSharePoint, TaskListStatus expectedTaskListStatus)
        {
            // Arrange
            var supportProjectModel = CreateSupportProjectViewModel(giveTheAdviserTheNoteOfVisitTemplate: giveTheAdviserTheNoteOfVisitTemplate, askTheAdviserToSendYouTheirNotes: askTheAdviserToSendYouTheirNotes,
                dateNoteOfVisitSavedInSharePoint: dateNoteOfVisitSavedInSharePoint);

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
            var supportProjectModel = CreateSupportProjectViewModel(sendConflictOfInterestFormToProposedAdviserAndTheSchool: sendConflictOfInterestFormToProposedAdviserAndTheSchool,
                receiveCompletedConflictOfInterestForm: receiveCompletedConflictOfInterestForm, saveCompletedConflictOfinterestFormInSharePoint: saveCompletedConflictOfinterestFormInSharePoint,
                dateConflictsOfInterestWereChecked: dateConflictsOfInterestWereChecked);

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
            var supportProjectModel = CreateSupportProjectViewModel(dateAdviserAssigned: dateAdviserAssigned, adviserEmailAddress: adviserEmailAddress);

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
            var supportProjectModel = CreateSupportProjectViewModel(adviserVisitDate: adviserVisitDate);

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
            var supportProjectModel = CreateSupportProjectViewModel(schoolVisitDate: schoolVisitDate);

            //Action 
            var taskListStatus = TaskStatusViewModel.RecordVisitDateToVisitSchoolTaskListStatus(supportProjectModel);

            //Assert
            Assert.Equal(expectedTaskListStatus, taskListStatus);
        }

        public static readonly TheoryData<DateTime?, bool?, string?, TaskListStatus> RecordSupportDecisionTaskListStatusCases = new()
        {
            { null, null, null, TaskListStatus.NotStarted },
            { DateTime.Now, true, null, TaskListStatus.Complete },
            { DateTime.Now, false, "Notes", TaskListStatus.InProgress }
        };

        [Theory, MemberData(nameof(RecordSupportDecisionTaskListStatusCases))]
        public void RecordSupportDecisionTaskListStatusShouldReturnCorrectStatus(DateTime? regionalDirectorDecisionDate, bool? hasConfirmedSchoolGetTargetSupport, string? disapprovingTargetedSupportNotes, TaskListStatus expectedTaskListStatus)
        {
            // Arrange
            var supportProjectModel = CreateSupportProjectViewModel(regionalDirectorDecisionDate: regionalDirectorDecisionDate, hasConfirmedSchoolGetTargetSupport: hasConfirmedSchoolGetTargetSupport,
                disapprovingTargetedSupportNotes: disapprovingTargetedSupportNotes);

            //Action 
            var taskListStatus = TaskStatusViewModel.RecordSupportDecisionTaskListStatus(supportProjectModel);

            //Assert
            Assert.Equal(expectedTaskListStatus, taskListStatus);
        }

        public static readonly TheoryData<DateTime?, string?, string?, TaskListStatus> ChoosePreferredSupportingOrganisationTaskListStatusCases = new()
        {
            { null, null, null, TaskListStatus.NotStarted },
            { DateTime.Now, "name", "12344f", TaskListStatus.Complete },
            { DateTime.Now, null, "12345f", TaskListStatus.InProgress }
        };

        [Theory, MemberData(nameof(ChoosePreferredSupportingOrganisationTaskListStatusCases))]
        public void ChoosePreferredSupportingOrganisationShouldReturnCorrectStatus(DateTime? datePreferredSupportOrganisationChosen, string? supportOrganisationName, string? supportOrganisationId, TaskListStatus expectedTaskListStatus)
        {
            // Arrange
            var supportProjectModel = CreateSupportProjectViewModel(dateSupportOrganisationChosen: datePreferredSupportOrganisationChosen, supportOrganisationName: supportOrganisationName,
                supportOrganisationId: supportOrganisationId);

            //Action 
            var taskListStatus = TaskStatusViewModel.ChoosePreferredSupportingOrganisationTaskListStatus(supportProjectModel);

            //Assert
            Assert.Equal(expectedTaskListStatus, taskListStatus);
        }

        private static SupportProjectViewModel CreateSupportProjectViewModel(string assignedAdviserFullName = "", string assignedAdviserEmailAddress = "", bool findSchoolEmailAddress = false, bool useTheNotificationLetterToCreateEmail = false,
            bool attachRiseInfoToEmail = false, DateTime? contactedTheSchoolDate = null, bool? sendConflictOfInterestFormToProposedAdviserAndTheSchool = null, bool? receiveCompletedConflictOfInterestForm = null,
            bool? saveCompletedConflictOfinterestFormInSharePoint = null, DateTime? dateConflictsOfInterestWereChecked = null, DateTime? schoolResponseDate = null, bool? hasAcceeptedTargetedSupport = null,
            bool? hasSavedSchoolResponseinSharePoint = null, DateTime? dateAdviserAssigned = null, string? adviserEmailAddress = null, DateTime? introductoryEmailSentDate = null, bool? hasShareEmailTemplateWithAdvisor = null,
            bool? remindAdvisorToCopyRiseTeamWhenSentEmail = null, DateTime? adviserVisitDate = null, DateTime? savedAssessmentTemplateInSharePointDate = null, bool? hasTalkToAdviserAboutFindings = null,
            bool? hasCompleteAssessmentTemplate = null, bool? giveTheAdviserTheNoteOfVisitTemplate = null, bool? askTheAdviserToSendYouTheirNotes = null, DateTime? dateNoteOfVisitSavedInSharePoint = null, DateTime? schoolVisitDate = null,
            DateTime? dateSupportOrganisationChosen = null, string? supportOrganisationName = null, string? supportOrganisationId = null, DateTime? regionalDirectorDecisionDate = null, bool? hasConfirmedSchoolGetTargetSupport = null, string? disapprovingTargetedSupportNotes = null,
            bool? CheckOrganisationHasCapacityAndWillingToProvideSupport = null,
            bool? CheckChoiceWithTrustRelationshipManagerOrLaLead = null,
            bool? DiscussChoiceWithSfso = null,
            bool? CheckFinancialConcernsAtSupportingOrganisation = null,
            bool? CheckTheOrganisationHasAVendorAccount = null,
            DateTime? DateDueDiligenceCompleted = null,
            IEnumerable<SupportProjectNote> notes = null!)
        {
            return SupportProjectViewModel.Create(new SupportProjectDto(1, DateTime.Now, "SchoolName", "23434", "LocalAuthority", "Region", assignedAdviserFullName, assignedAdviserEmailAddress, findSchoolEmailAddress,
                useTheNotificationLetterToCreateEmail, attachRiseInfoToEmail, contactedTheSchoolDate, sendConflictOfInterestFormToProposedAdviserAndTheSchool, receiveCompletedConflictOfInterestForm,
                saveCompletedConflictOfinterestFormInSharePoint, dateConflictsOfInterestWereChecked, schoolResponseDate, hasAcceeptedTargetedSupport, hasSavedSchoolResponseinSharePoint, dateAdviserAssigned, adviserEmailAddress,
                introductoryEmailSentDate, hasShareEmailTemplateWithAdvisor, remindAdvisorToCopyRiseTeamWhenSentEmail, adviserVisitDate, savedAssessmentTemplateInSharePointDate, hasTalkToAdviserAboutFindings, hasCompleteAssessmentTemplate,
                giveTheAdviserTheNoteOfVisitTemplate, askTheAdviserToSendYouTheirNotes, dateNoteOfVisitSavedInSharePoint, schoolVisitDate, dateSupportOrganisationChosen, supportOrganisationName, supportOrganisationId, regionalDirectorDecisionDate, hasConfirmedSchoolGetTargetSupport, disapprovingTargetedSupportNotes,
                CheckOrganisationHasCapacityAndWillingToProvideSupport,
                CheckChoiceWithTrustRelationshipManagerOrLaLead,
                DiscussChoiceWithSfso,
                CheckFinancialConcernsAtSupportingOrganisation,
                CheckTheOrganisationHasAVendorAccount,
                DateDueDiligenceCompleted,
                notes));
        }
    }
}
