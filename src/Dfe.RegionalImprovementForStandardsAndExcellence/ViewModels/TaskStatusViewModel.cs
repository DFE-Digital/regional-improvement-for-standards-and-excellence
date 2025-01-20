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

    public static TaskListStatus CheckThePotentialAdviserConflictsOfInterestTaskListStatus(SupportProjectViewModel supportProject)
    {
        if (supportProject.SendConflictOfInterestFormToProposedAdviserAndTheSchool.HasValue
            && supportProject.RecieveCompletedConflictOfInteresetForm.HasValue
            && supportProject.SaveCompletedConflictOfinterestFormInSharePoint.HasValue
            && supportProject.DateConflictsOfInterestWereChecked.HasValue)
        {
            return TaskListStatus.Complete;
        }

        if (!supportProject.SendConflictOfInterestFormToProposedAdviserAndTheSchool.HasValue
            && !supportProject.RecieveCompletedConflictOfInteresetForm.HasValue
            && !supportProject.SaveCompletedConflictOfinterestFormInSharePoint.HasValue
            && !supportProject.DateConflictsOfInterestWereChecked.HasValue)
        {
            return TaskListStatus.NotStarted;
        }

        return TaskListStatus.InProgress;
    }
}