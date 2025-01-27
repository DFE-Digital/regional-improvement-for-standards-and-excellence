﻿using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Models
{
    public record SupportProjectDto(int id,
        DateTime createdOn,
        string schoolName,
        string schoolUrn,
        string localAuthority,
        string region,
        string assignedAdviserFullName,
        string assignedAdviserEmailAddress,
        bool FindSchoolEmailAddress,
        bool UseTheNotificationLetterToCreateEmail,
        bool AttachRiseInfoToEmail,
        DateTime? ContactedTheSchoolDate,
        bool? SendConflictOfInterestFormToProposedAdviserAndTheSchool,
        bool? ReceiveCompletedConflictOfInterestForm,
        bool? SaveCompletedConflictOfinterestFormInSharePoint,
        DateTime? DateConflictsOfInterestWereChecked,
        DateTime? SchoolResponseDate,
        bool? HasAcceeptedTargetedSupport,
        bool? HasSavedSchoolResponseinSharePoint,
        DateTime? DateAdviserAssigned,
        string? AdviserEmailAddress,
        DateTime? IntroductoryEmailSentDate,
        bool? HasShareEmailTemplateWithAdvisor,
        bool? RemindAdvisorToCopyRiseTeamWhenSentEmail,
        DateTime? AdviserVisitDate,
        DateTime? SavedAssessmentTemplateInSharePointDate,
        bool? HasTalkToAdviserAboutFindings,
        bool? HasCompleteAssessmentTemplate,
        bool? GiveTheAdviserTheNoteOfVisitTemplate,
        bool? AskTheAdviserToSendYouTheirNotes,
        DateTime? DateNoteOfVisitSavedInSharePoint,
        DateTime? SchoolVisitDate,
        IEnumerable<SupportProjectNote> notes
    );
}