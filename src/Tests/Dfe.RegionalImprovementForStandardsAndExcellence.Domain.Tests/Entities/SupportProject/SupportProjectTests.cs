using FluentAssertions;
using Moq;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Tests.Entities.SupportProject
{
    public class SupportProjectTests
    {
        private readonly MockRepository mockRepository;

        public SupportProjectTests()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [Fact]
        public void Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange Act
            var supportProject = CreateSupportProject();

            // Assert
            supportProject.Should().NotBeNull();
            mockRepository.VerifyAll();
        }

        [Fact]
        public void SetAdviser_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var supportProject = CreateSupportProject();
            string assignedAdviserFullName = "testName";
            string assignedAdviserEmailAddress = "Test@Email.com";

            // Act
            supportProject.SetAdviser(
                assignedAdviserFullName,
                assignedAdviserEmailAddress);

            // Assert
            supportProject.AssignedAdviserFullName.Should().Be(assignedAdviserFullName);
            supportProject.AssignedAdviserEmailAddress.Should().Be(assignedAdviserEmailAddress);
            mockRepository.VerifyAll();
        }

        [Fact]
        public void SetSchoolResponse_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var supportProject = CreateSupportProject();
            DateTime? schoolResponseDate = DateTime.Now;
            bool? hasAcceeptedTargetedSupport = true;
            bool? hasSavedSchoolResponseinSharePoint = true;

            // Act
            supportProject.SetSchoolResponse(
                schoolResponseDate,
                hasAcceeptedTargetedSupport,
                hasSavedSchoolResponseinSharePoint);

            // Assert
            supportProject.SchoolResponseDate.Should().Be(schoolResponseDate);
            supportProject.HasAcceeptedTargetedSupport.Should().Be(hasAcceeptedTargetedSupport);
            supportProject.HasSavedSchoolResponseinSharePoint.Should().Be(hasSavedSchoolResponseinSharePoint);
            mockRepository.VerifyAll();
        }

        [Fact]
        public void SetSendIntroductoryEmail_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var supportProject = CreateSupportProject();
            var introductoryEmailSentDate = DateTime.UtcNow;
            var hasShareEmailTemplateWithAdvisor = true;
            var remindAdvisorToCopyRiseTeamWhenSentEmail = true;

            // Act
            supportProject.SetSendIntroductoryEmail(
                introductoryEmailSentDate,
                hasShareEmailTemplateWithAdvisor,
                remindAdvisorToCopyRiseTeamWhenSentEmail);

            // Assert
            supportProject.IntroductoryEmailSentDate.Should().Be(introductoryEmailSentDate);
            supportProject.HasShareEmailTemplateWithAdvisor.Should().Be(hasShareEmailTemplateWithAdvisor);
            supportProject.RemindAdvisorToCopyRiseTeamWhenSentEmail.Should().Be(remindAdvisorToCopyRiseTeamWhenSentEmail);
            mockRepository.VerifyAll();
        }

        [Fact]
        public void SetCompleteAndSaveAssessmentTemplate_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var supportProject = CreateSupportProject();
            var savedAssessmentTemplateInSharePointDate = DateTime.UtcNow;
            var hasTalkToAdvisor = true;
            var hasCompleteAssessmentTemplate = true;

            // Act
            supportProject.SetCompleteAndSaveAssessmentTemplate(
                savedAssessmentTemplateInSharePointDate,
                hasTalkToAdvisor,
                hasCompleteAssessmentTemplate);

            // Assert
            supportProject.SavedAssessmentTemplateInSharePointDate.Should().Be(savedAssessmentTemplateInSharePointDate);
            supportProject.HasTalkToAdviserAboutFindings.Should().Be(hasTalkToAdvisor);
            supportProject.HasCompleteAssessmentTemplate.Should().Be(hasCompleteAssessmentTemplate);
            mockRepository.VerifyAll();
        }

        [Fact]
        public void SetSchoolVisitDate_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var supportProject = CreateSupportProject();
            var schoolVisitDate = DateTime.UtcNow;

            // Act
            supportProject.SetSchoolVisitDate(schoolVisitDate);

            // Assert
            supportProject.SchoolVisitDate.Should().Be(schoolVisitDate);
            mockRepository.VerifyAll();
        }

        [Fact]
        public void SetAdviserConflictOfInterestDetails_WithValidDetails_SetsTheCorrectProperties()
        {
            // Arrange
            var supportProject = CreateSupportProject();

            bool? sendConflictOfInterestFormToProposedAdviserAndTheSchool = true;
            bool? receiveCompletedConflictOfInterestForm = true;
            bool? saveCompletedConflictOfinterestFormInSharePoint = true;
            DateTime? dateConflictsOfInterestWereChecked = DateTime.UtcNow;

            // Act
            supportProject.SetAdviserConflictOfInterestDetails(
                sendConflictOfInterestFormToProposedAdviserAndTheSchool,
                receiveCompletedConflictOfInterestForm,
                saveCompletedConflictOfinterestFormInSharePoint,
                dateConflictsOfInterestWereChecked);

            // Assert
            supportProject.SendConflictOfInterestFormToProposedAdviserAndTheSchool.Should().Be(sendConflictOfInterestFormToProposedAdviserAndTheSchool);
            supportProject.ReceiveCompletedConflictOfInterestForm.Should().Be(receiveCompletedConflictOfInterestForm);
            supportProject.SaveCompletedConflictOfinterestFormInSharePoint.Should().Be(saveCompletedConflictOfinterestFormInSharePoint);
            supportProject.DateConflictsOfInterestWereChecked.Should().Be(dateConflictsOfInterestWereChecked);
            mockRepository.VerifyAll();
        }

        [Fact]
        public void SetAdviserDetails_WithValidDetails_SetsTheCorrectProperties()
        {
            // Arrange
            var supportProject = CreateSupportProject();

            string? adviserEmailAddress = "test";
            DateTime? dateAssigned = DateTime.UtcNow;

            // Act
            supportProject.SetAdviserDetails(
                adviserEmailAddress,
                dateAssigned);

            // Assert
            supportProject.AdviserEmailAddress.Should().Be(adviserEmailAddress);
            supportProject.DateAdviserAssigned.Should().Be(dateAssigned);
            mockRepository.VerifyAll();
        }

        private static Domain.Entities.SupportProject.SupportProject CreateSupportProject(
            string schoolName = "Default School",
            string schoolUrn = "DefaultURN",
            string localAuthority = "Default Authority",
            string region = "Default Region")
        {
            return Domain.Entities.SupportProject.SupportProject.Create(
                 schoolName,
                 schoolUrn,
                 localAuthority,
                 region);
        }

        [Fact]
        public void SetContactTheSchool_WithValidDetails_SetsTheCorrectProperties()
        {
            // Arrange
            var supportProject = CreateSupportProject();

            bool? schoolEmailAddressFound = true;
            bool? useTheNotificationLetterToCreateEmail = true;
            bool? attachRiseInfoToEmail = true;
            DateTime? schoolContactedDate = DateTime.UtcNow;

            // Act
            supportProject.SetContactTheSchoolDetails(
                schoolEmailAddressFound,
                useTheNotificationLetterToCreateEmail,
                attachRiseInfoToEmail,
                schoolContactedDate);

            // Assert
            supportProject.FindSchoolEmailAddress.Should().Be(schoolEmailAddressFound);
            supportProject.UseTheNotificationLetterToCreateEmail.Should().Be(useTheNotificationLetterToCreateEmail);
            supportProject.AttachRiseInfoToEmail.Should().Be(attachRiseInfoToEmail);
            supportProject.ContactedTheSchoolDate.Should().Be(schoolContactedDate);
            mockRepository.VerifyAll();
        }

        [Fact]

        public void SetAdviserVisitDate_WithValidDetails_SetsTheCorrectProperties()
        {
            // Arrange
            var supportProject = CreateSupportProject();

            DateTime? adviserVisitDate = DateTime.UtcNow;

            // Act
            supportProject.SetAdviserVisitDate(
                adviserVisitDate);

            // Assert
            supportProject.AdviserVisitDate.Should().Be(adviserVisitDate);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void SetNoteOfVisit_WithValidDetails_SetsTheCorrectProperties()
        {
            // Arrange
            var supportProject = CreateSupportProject();

            bool? giveTheAdviserTheNoteOfVisitTemplate = false;
            bool? askTheAdviserToSendYouTheirNotes = false;
            DateTime? dateNoteOfVisitSavedInSharePoint = DateTime.UtcNow;

            // Act
            supportProject.SetNoteOfVisitDetails(
                giveTheAdviserTheNoteOfVisitTemplate,
                askTheAdviserToSendYouTheirNotes,
                dateNoteOfVisitSavedInSharePoint);

            // Assert
            supportProject.GiveTheAdviserTheNoteOfVisitTemplate.Should().Be(giveTheAdviserTheNoteOfVisitTemplate);
            supportProject.AskTheAdviserToSendYouTheirNotes.Should().Be(askTheAdviserToSendYouTheirNotes);
            supportProject.DateNoteOfVisitSavedInSharePoint.Should().Be(dateNoteOfVisitSavedInSharePoint);
            mockRepository.VerifyAll();
        }

        [Fact]
        public void SetRecordSupportDecision_SetsTheCorrectProperties()
        {
            // Arrange
            var supportProject = CreateSupportProject();

            bool? hasConfirmedSchoolGetTargetSupport = false;
            DateTime? regionalDirectorDecisionDate = DateTime.UtcNow;
            string? disapprovingTargetedSupportNotes = "Notes only if choose no";

            // Act
            supportProject.SetRecordSupportDecision(
                regionalDirectorDecisionDate,
                hasConfirmedSchoolGetTargetSupport,
                disapprovingTargetedSupportNotes);

            // Assert
            supportProject.HasConfirmedSchoolGetTargetSupport.Should().Be(hasConfirmedSchoolGetTargetSupport);
            supportProject.RegionalDirectorDecisionDate.Should().Be(regionalDirectorDecisionDate);
            supportProject.DisapprovingTargetedSupportNotes.Should().Be(disapprovingTargetedSupportNotes);
            mockRepository.VerifyAll();
        }

        [Fact]
        public void SetRecordSupportDecision_RemovesNotes_OnConfirmingTargetSupport()
        {
            // Arrange
            var supportProject = CreateSupportProject();

            bool? hasConfirmedSchoolGetTargetSupport = true;
            DateTime? regionalDirectorDecisionDate = DateTime.UtcNow;
            string? disapprovingTargetedSupportNotes = "Notes only if choose no";

            // Act
            supportProject.SetRecordSupportDecision(
                regionalDirectorDecisionDate,
                hasConfirmedSchoolGetTargetSupport,
                disapprovingTargetedSupportNotes);

            // Assert
            supportProject.HasConfirmedSchoolGetTargetSupport.Should().Be(hasConfirmedSchoolGetTargetSupport);
            supportProject.RegionalDirectorDecisionDate.Should().Be(regionalDirectorDecisionDate);
            supportProject.DisapprovingTargetedSupportNotes.Should().BeNull();
            mockRepository.VerifyAll();
        }

        [Fact]
        public void SetChoosePreferredSupportOrganisation_WithValidDetails_SetsTheCorrectProperties()
        {
            // Arrange
            var supportProject = CreateSupportProject();

            DateTime? dateSupportOrganisationChosen = DateTime.UtcNow;
            string? supportOrgansiationName = "name";
            string? supportOrganisationId = "1234a";

            // Act
            supportProject.SetChoosePreferredSupportOrganisation(
                dateSupportOrganisationChosen,
                supportOrgansiationName,
                supportOrganisationId);

            // Assert
            supportProject.DateSupportOrganisationChosen.Should().Be(dateSupportOrganisationChosen);
            supportProject.SupportOrganisationName.Should().Be(supportOrgansiationName);
            supportProject.SupportOrganisationIdNumber.Should().Be(supportOrganisationId);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void SetDueDiligenceOnPreferredSupportingOrganisationDetails_WithValidDetails_SetsTheCorrectProperties()
        {
            // Arrange
            var supportProject = CreateSupportProject();

            bool? checkOrganisationHasCapacityAndWillingToProvideSupport = true;
            bool? checkChoiceWithTrustRelationshipManagerOrLaLead = false;
            bool? discussChoiceWithSfso = true;
            bool? checkFinancialConcernsAtSupportingOrganisation = null;
            bool? checkTheOrganisationHasAVendorAccount = true;
            DateTime? dateDueDiligenceCompleted = DateTime.UtcNow;

            // Act
            supportProject.SetDueDiligenceOnPreferredSupportingOrganisationDetails(
                checkOrganisationHasCapacityAndWillingToProvideSupport,
                checkChoiceWithTrustRelationshipManagerOrLaLead,
                discussChoiceWithSfso,
                checkFinancialConcernsAtSupportingOrganisation,
                checkTheOrganisationHasAVendorAccount, dateDueDiligenceCompleted);

            // Assert
            supportProject.CheckOrganisationHasCapacityAndWillingToProvideSupport.Should().Be(checkOrganisationHasCapacityAndWillingToProvideSupport);
            supportProject.CheckChoiceWithTrustRelationshipManagerOrLaLead.Should().Be(checkChoiceWithTrustRelationshipManagerOrLaLead);
            supportProject.DiscussChoiceWithSfso.Should().Be(discussChoiceWithSfso);
            supportProject.CheckFinancialConcernsAtSupportingOrganisation.Should().Be(checkFinancialConcernsAtSupportingOrganisation);
            supportProject.CheckTheOrganisationHasAVendorAccount.Should().Be(checkTheOrganisationHasAVendorAccount);
            supportProject.DateDueDiligenceCompleted.Should().Be(dateDueDiligenceCompleted);
            this.mockRepository.VerifyAll();
        }


        [Fact]
        public void SetRecordSupportOrganisationAppointment_SetsTheCorrectProperties()
        {
            // Arrange
            var supportProject = CreateSupportProject();

            bool? hasConfirmedSupportingOrgnaisationAppointment = false;
            DateTime? regionalDirectorAppointmentDate = DateTime.UtcNow;
            string? disapprovingSupportingOrgnaisationAppointmentNotes = "Notes only if choose no";

            // Act
            supportProject.SetRecordSupportingOrganisationAppointment(
                regionalDirectorAppointmentDate,
                hasConfirmedSupportingOrgnaisationAppointment,
                disapprovingSupportingOrgnaisationAppointmentNotes);

            // Assert
            supportProject.HasConfirmedSupportingOrgnaisationAppointment.Should().Be(hasConfirmedSupportingOrgnaisationAppointment);
            supportProject.RegionalDirectorAppointmentDate.Should().Be(regionalDirectorAppointmentDate);
            supportProject.DisapprovingSupportingOrgnaisationAppointmentNotes.Should().Be(disapprovingSupportingOrgnaisationAppointmentNotes);
            mockRepository.VerifyAll();
        }

        [Fact]
        public void SetRecordSupportOrganisationAppointment_RemovesNotes_OnConfirmingTargetSupport()
        {
            // Arrange
            var supportProject = CreateSupportProject();

            bool? hasConfirmedSupportingOrgnaisationAppointment = true;
            DateTime? regionalDirectorAppointmentDate = DateTime.UtcNow;
            string? disapprovingTargetedSupportNotes = "Notes only if choose no";

            // Act
            supportProject.SetRecordSupportingOrganisationAppointment(
                regionalDirectorAppointmentDate,
                hasConfirmedSupportingOrgnaisationAppointment,
                disapprovingTargetedSupportNotes);

            // Assert
            supportProject.HasConfirmedSupportingOrgnaisationAppointment.Should().Be(hasConfirmedSupportingOrgnaisationAppointment);
            supportProject.RegionalDirectorAppointmentDate.Should().Be(regionalDirectorAppointmentDate);
            supportProject.DisapprovingSupportingOrgnaisationAppointmentNotes.Should().BeNull();
            mockRepository.VerifyAll();
        }

        [Fact]
        public void SetSupportingOrganisationContactDetails_WithValidDetails_SetsTheCorrectProperties()
        {
            // Arrange
            var supportProject = CreateSupportProject();

            DateTime? dateSupportingOrganisationContactDetailAdded = DateTime.UtcNow;
            string? supportOrgansiationContactName = "name";
            string? supportOrganisationContactEmailAddress = "1234a";

            // Act
            supportProject.SetSupportingOrganisationContactDetails(
                dateSupportingOrganisationContactDetailAdded,
                supportOrgansiationContactName,
                supportOrganisationContactEmailAddress);

            // Assert
            supportProject.DateSupportingOrganisationContactDetailsAdded.Should().Be(dateSupportingOrganisationContactDetailAdded);
            supportProject.SupportingOrganisationContactName.Should().Be(supportOrgansiationContactName);
            supportProject.SupportingOrganisationContactEmailAddress.Should().Be(supportOrganisationContactEmailAddress);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void SetShareImproveMentPlanDetails_WithValidDetails_SetsTheCorrectProperties()
        {
            // Arrange
            var supportProject = CreateSupportProject();

            DateTime? dateTemplatesSent = DateTime.UtcNow;
            bool? sendTheTemplateToTheSupportingOrganisation = true;
            bool? sendTheTemplateToTheSchoolsResponsibleBody = true;

            // Act
            supportProject.SetImprovementPlanTemplateDetails(
                sendTheTemplateToTheSupportingOrganisation,
                sendTheTemplateToTheSchoolsResponsibleBody,
                dateTemplatesSent);

            // Assert
            supportProject.DateTemplatesSent.Should().Be(dateTemplatesSent);
            supportProject.SendTheTemplateToTheSupportingOrganisation.Should().Be(sendTheTemplateToTheSupportingOrganisation);
            supportProject.SendTheTemplateToTheSchoolsResponsibleBody.Should().Be(sendTheTemplateToTheSchoolsResponsibleBody);
            this.mockRepository.VerifyAll();
        }
    }
}
