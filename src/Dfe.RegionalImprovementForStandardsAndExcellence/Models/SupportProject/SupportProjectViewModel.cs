using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.SupportProject
{
    public class SupportProjectViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string SchoolName { get; set; }

        public string SchoolUrn { get; set; }

        public string LocalAuthority { get; set; }

        public string Region { get; set; }

        public string AssignedAdviserFullName { get; set; }

        public string AssignedAdviserEmailAddress { get; set; }

        public string QualityOfEducation { get; set; }

        public string BehaviourAndAttitudes { get; set; }

        public string PersonalDevelopment { get; set; }

        public string LeadershipAndManagement { get; set; }
        public string LastInspectionDate { get; set; }

        public bool? FindSchoolEmailAddress { get; private set; }

        public bool? UseTheNotificationLetterToCreateEmail { get; private set; }

        public bool? AttachRiseInfoToEmail { get; private set; }

        public DateTime? ContactedTheSchoolDate { get; private set; }

        public bool? SendConflictOfInterestFormToProposedAdviserAndTheSchool { get; private set; }

        public bool? ReceiveCompletedConflictOfInterestForm { get; private set; }

        public bool? SaveCompletedConflictOfinterestFormInSharePoint { get; private set; }

        public DateTime? DateConflictsOfInterestWereChecked { get; private set; }

        public IEnumerable<SupportProjectNote> Notes { get; set; }

        public DateTime? SchoolResponseDate { get; set; }

        public bool? HasAcceeptedTargetedSupport { get; set; }

        public bool? HasSavedSchoolResponseinSharePoint { get; set; }

        public DateTime? DateAdviserAssigned { get; private set; }
        public string? AdviserEmailAddress { get; private set; }

        public DateTime? IntroductoryEmailSentDate { get; set; }
        public bool? HasShareEmailTemplateWithAdvisor { get; set; }

        public bool? RemindAdvisorToCopyRiseTeamWhenSentEmail { get; set; }

        public DateTime? AdviserVisitDate { get; set; }
        public bool? GiveTheAdviserTheNoteOfVisitTemplate { get; private set; }
        public bool? AskTheAdviserToSendYouTheirNotes { get; private set; }
        public DateTime? DateNoteOfVisitSavedInSharePoint { get; private set; }

        public DateTime? SavedAssessmentTemplateInSharePointDate { get; set; }

        public bool? HasTalkToAdviserAboutFindings { get; set; }
        
        public bool? HasCompleteAssessmentTemplate { get; set; }

        public DateTime? SchoolVisitDate { get; set; }
        public bool? HasConfirmedSchoolGetTargetSupport { get;set; }
        public DateTime? RegionalDirectorDecisionDate { get; set; }
        public string? DisapprovingTargetedSupportNotes { get; set; }

        public static SupportProjectViewModel Create(SupportProjectDto supportProjectDto)
        {
            return new SupportProjectViewModel()
            {
                Id = supportProjectDto.id,
                CreatedOn = supportProjectDto.createdOn,
                // ToDo: we will repurpose these fields as the assigned delivery officer
                AssignedAdviserFullName = supportProjectDto.assignedAdviserFullName,
                AssignedAdviserEmailAddress = supportProjectDto.assignedAdviserEmailAddress,
                // ***
                LocalAuthority = supportProjectDto.localAuthority,
                Region = supportProjectDto.region,
                SchoolName = supportProjectDto.schoolName,
                SchoolUrn = supportProjectDto.schoolUrn,
                Notes = supportProjectDto.notes,
                FindSchoolEmailAddress = supportProjectDto.FindSchoolEmailAddress,
                UseTheNotificationLetterToCreateEmail = supportProjectDto.UseTheNotificationLetterToCreateEmail,
                AttachRiseInfoToEmail = supportProjectDto.AttachRiseInfoToEmail,
                ContactedTheSchoolDate = supportProjectDto.ContactedTheSchoolDate,
                SendConflictOfInterestFormToProposedAdviserAndTheSchool = supportProjectDto.SendConflictOfInterestFormToProposedAdviserAndTheSchool,
                ReceiveCompletedConflictOfInterestForm = supportProjectDto.ReceiveCompletedConflictOfInterestForm,
                SaveCompletedConflictOfinterestFormInSharePoint = supportProjectDto.SaveCompletedConflictOfinterestFormInSharePoint,
                DateConflictsOfInterestWereChecked = supportProjectDto.DateConflictsOfInterestWereChecked,
                SchoolResponseDate = supportProjectDto.SchoolResponseDate,
                HasAcceeptedTargetedSupport = supportProjectDto.HasAcceeptedTargetedSupport,
                HasSavedSchoolResponseinSharePoint = supportProjectDto.HasSavedSchoolResponseinSharePoint,
                HasShareEmailTemplateWithAdvisor = supportProjectDto.HasShareEmailTemplateWithAdvisor,
                RemindAdvisorToCopyRiseTeamWhenSentEmail = supportProjectDto.RemindAdvisorToCopyRiseTeamWhenSentEmail,
                IntroductoryEmailSentDate = supportProjectDto.IntroductoryEmailSentDate,
                AdviserEmailAddress = supportProjectDto.AdviserEmailAddress,
                DateAdviserAssigned = supportProjectDto.DateAdviserAssigned,
                SavedAssessmentTemplateInSharePointDate = supportProjectDto.SavedAssessmentTemplateInSharePointDate,
                HasTalkToAdviserAboutFindings = supportProjectDto.HasTalkToAdviserAboutFindings,
                HasCompleteAssessmentTemplate = supportProjectDto.HasCompleteAssessmentTemplate,
                AdviserVisitDate = supportProjectDto.AdviserVisitDate,
                GiveTheAdviserTheNoteOfVisitTemplate = supportProjectDto.GiveTheAdviserTheNoteOfVisitTemplate,
                AskTheAdviserToSendYouTheirNotes = supportProjectDto.AskTheAdviserToSendYouTheirNotes,
                DateNoteOfVisitSavedInSharePoint = supportProjectDto.DateNoteOfVisitSavedInSharePoint,
                SchoolVisitDate = supportProjectDto.SchoolVisitDate,
                RegionalDirectorDecisionDate = supportProjectDto.RegionalDirectorDecisionDate,
                HasConfirmedSchoolGetTargetSupport = supportProjectDto.HasConfirmedSchoolGetTargetSupport,
                DisapprovingTargetedSupportNotes = supportProjectDto.DisapprovingTargetedSupportNotes
            };
        }
    }
}
