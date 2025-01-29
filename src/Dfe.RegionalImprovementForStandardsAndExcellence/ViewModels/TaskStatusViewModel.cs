using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.SupportProject;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.ViewModels;

public static class TaskStatusViewModel
{
    public static TaskListStatus ContactedTheSchoolTaskStatus(SupportProjectViewModel SupportProject)
    {
        if (SupportProject.AttachRiseInfoToEmail.Equals(true) &&
            SupportProject.FindSchoolEmailAddress.Equals(true) &&
            SupportProject.UseTheNotificationLetterToCreateEmail.Equals(true) &&
            SupportProject.ContactedTheSchoolDate.HasValue)
        {
            return TaskListStatus.Complete;
        }

        if (SupportProject.AttachRiseInfoToEmail.Equals(false) &&
            SupportProject.FindSchoolEmailAddress.Equals(false) &&
            SupportProject.UseTheNotificationLetterToCreateEmail.Equals(false) &&
            !SupportProject.ContactedTheSchoolDate.HasValue)
        {
            return TaskListStatus.NotStarted;
        }

        return TaskListStatus.InProgress;
    }

    public static TaskListStatus RecordTheSchoolResponseTaskStatus(SupportProjectViewModel SupportProject)
    {
        if (SupportProject.HasSavedSchoolResponseinSharePoint.Equals(true) &&
            SupportProject.HasAcceeptedTargetedSupport.Equals(true) &&
            SupportProject.SchoolResponseDate.HasValue)
        {
            return TaskListStatus.Complete;
        }

        if (!SupportProject.HasSavedSchoolResponseinSharePoint.HasValue &&
            !SupportProject.HasAcceeptedTargetedSupport.HasValue &&
            !SupportProject.SchoolResponseDate.HasValue)
        {
            return TaskListStatus.NotStarted;
        }

        return TaskListStatus.InProgress;
    }

    public static TaskListStatus CheckThePotentialAdviserConflictsOfInterestTaskListStatus(
        SupportProjectViewModel supportProject)
    {
        if (supportProject.SendConflictOfInterestFormToProposedAdviserAndTheSchool.HasValue
            && supportProject.ReceiveCompletedConflictOfInterestForm.HasValue
            && supportProject.SaveCompletedConflictOfinterestFormInSharePoint.HasValue
            && supportProject.DateConflictsOfInterestWereChecked.HasValue)
        {
            return TaskListStatus.Complete;
        }

        if (!supportProject.SendConflictOfInterestFormToProposedAdviserAndTheSchool.HasValue
            && !supportProject.ReceiveCompletedConflictOfInterestForm.HasValue
            && !supportProject.SaveCompletedConflictOfinterestFormInSharePoint.HasValue
            && !supportProject.DateConflictsOfInterestWereChecked.HasValue)
        {
            return TaskListStatus.NotStarted;
        }

        return TaskListStatus.InProgress;
    }

    public static TaskListStatus CheckAssignAdviserTaskListStatus(SupportProjectViewModel supportProject)
    {
        if (supportProject.AdviserEmailAddress != null
            && supportProject.DateAdviserAssigned.HasValue)
        {
            return TaskListStatus.Complete;
        }

        if (supportProject.AdviserEmailAddress == null
            && !supportProject.DateAdviserAssigned.HasValue)
        {
            return TaskListStatus.NotStarted;
        }

        return TaskListStatus.InProgress;
    }
    
    public static TaskListStatus SendIntroductoryEmailTaskListStatus(SupportProjectViewModel supportProject)
    {
        if (supportProject.HasShareEmailTemplateWithAdvisor.HasValue
            && supportProject.RemindAdvisorToCopyRiseTeamWhenSentEmail.HasValue
            && supportProject.IntroductoryEmailSentDate.HasValue)
        {
            return TaskListStatus.Complete;
        }

        if (!supportProject.HasShareEmailTemplateWithAdvisor.HasValue
            && !supportProject.RemindAdvisorToCopyRiseTeamWhenSentEmail.HasValue
            && !supportProject.IntroductoryEmailSentDate.HasValue)
        {
            return TaskListStatus.NotStarted;
        }

        return TaskListStatus.InProgress;
    }

    public static TaskListStatus AdviserVisitToSchoolTaskListStatus(SupportProjectViewModel supportProject)
    {
        if (supportProject.AdviserVisitDate.HasValue)
        {
            return TaskListStatus.Complete;
        }

        return TaskListStatus.NotStarted;
    }

    public static TaskListStatus CompleteAndSaveAssessmentTemplateTaskListStatus(SupportProjectViewModel supportProject)
    {
        if (supportProject.SavedAssessmentTemplateInSharePointDate.HasValue
            && supportProject.HasTalkToAdviserAboutFindings.HasValue
            && supportProject.HasCompleteAssessmentTemplate.HasValue)
        {
            return TaskListStatus.Complete;
        }

        if (!supportProject.SavedAssessmentTemplateInSharePointDate.HasValue
            && !supportProject.HasTalkToAdviserAboutFindings.HasValue
            && !supportProject.HasCompleteAssessmentTemplate.HasValue)
        {
            return TaskListStatus.NotStarted;
        }

        return TaskListStatus.InProgress;
    }

    public static TaskListStatus NoteOfVsistTaskListStatus(SupportProjectViewModel supportProject)
    {
        if (supportProject.AskTheAdviserToSendYouTheirNotes.HasValue
            && supportProject.GiveTheAdviserTheNoteOfVisitTemplate.HasValue
            && supportProject.DateNoteOfVisitSavedInSharePoint.HasValue)
        {
            return TaskListStatus.Complete;
        }

        if (!supportProject.AskTheAdviserToSendYouTheirNotes.HasValue
            && !supportProject.GiveTheAdviserTheNoteOfVisitTemplate.HasValue
            && !supportProject.DateNoteOfVisitSavedInSharePoint.HasValue)
        {
            return TaskListStatus.NotStarted;
        }

        return TaskListStatus.InProgress;
    }
    public static TaskListStatus RecordVisitDateToVisitSchoolTaskListStatus(SupportProjectViewModel supportProject)
    {
        if (supportProject.SchoolVisitDate.HasValue)
        {
            return TaskListStatus.Complete;
        }
         
        return TaskListStatus.NotStarted;
    }

    public static TaskListStatus ChoosePreferredSupportingOrganisationTaskListStatus(
        SupportProjectViewModel supportProject)
    {
        if (supportProject.DateSupportOrganisationChosen.HasValue
            && supportProject.SupportOrganisationName != null
            && supportProject.SupportOrganisationIdNumber != null)
        {
            return TaskListStatus.Complete;
        }
        
        if (!supportProject.DateSupportOrganisationChosen.HasValue
            && supportProject.SupportOrganisationName == null
            && supportProject.SupportOrganisationIdNumber == null)
        {
            return TaskListStatus.NotStarted;
        }
        
        return TaskListStatus.InProgress;
    }
}