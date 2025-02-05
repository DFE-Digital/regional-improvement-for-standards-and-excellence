using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.TaskList;

public class IndexModel(ISupportProjectQueryService supportProjectQueryService, IGetEstablishment getEstablishment, ErrorService errorService) : BaseSupportProjectEstablishmentPageModel(supportProjectQueryService, getEstablishment, errorService)
{
    public string ReturnPage { get; set; }

    public TaskListStatus ContactTheSchoolTaskListStatus { get; set; }
    public TaskListStatus RecordTheSchoolResponseTaskListStatus { get; set; }
    public TaskListStatus CheckThePotentialAdviserConflictsOfInterestTaskListStatus { get; set; }
    public TaskListStatus SendIntroductoryEmailTaskListStatus { get; set; }
    public TaskListStatus AssignAdviserTaskListStatus { get; set; }

    public TaskListStatus AdviserVisitToSchoolTaskListStatus { get; set; }
    public TaskListStatus CompleteAndSaveAssessmentTemplateTaskListStatus { get; set; }
    public TaskListStatus NoteOfVisitTaskListStatus { get; set; }
    public TaskListStatus RecordVisitDateToVisitSchoolTaskListStatus { get; set; }

    public TaskListStatus ChosePreferredSupportingOrganisationTaskListStatus { get; set; }
    public TaskListStatus RecordSupportDecisionTaskListStatus { get; set; }
    public TaskListStatus DueDiligenceOnPreferredSupportingOrganisationTaskListStatus { get; set; }
    public TaskListStatus SetRecordSupportingOrganisationAppointment { get; set; }

    public TaskListStatus SupportingOrganisationContactDetailsTaskListStatus { get; set; }
    public TaskListStatus ShareTheImprovementPlanTemplateTaskListStatus { get; set; }

    public TaskListStatus RecordImprovementPlanDecisionTaskListStatus { get; set; }

    public TaskListStatus SendAgreedImprovementPlanForApprovalTaskListStatus { get; set; }
    
    public TaskListStatus ReviewTheImprovementPlanTaskListStatus { get; set; }
    public void SetErrorPage(string errorPage)
    {
        TempData["ErrorPage"] = errorPage;
    }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        ProjectListFilters.ClearFiltersFrom(TempData);

        ReturnPage = @Links.SchoolList.Index.Page;

        await base.GetSupportProject(id, cancellationToken);

        ContactTheSchoolTaskListStatus = TaskStatusViewModel.ContactedTheSchoolTaskStatus(SupportProject);
        RecordTheSchoolResponseTaskListStatus = TaskStatusViewModel.RecordTheSchoolResponseTaskStatus(SupportProject);
        CheckThePotentialAdviserConflictsOfInterestTaskListStatus = TaskStatusViewModel.CheckThePotentialAdviserConflictsOfInterestTaskListStatus(SupportProject);
        AssignAdviserTaskListStatus = TaskStatusViewModel.CheckAssignAdviserTaskListStatus(SupportProject);
        SendIntroductoryEmailTaskListStatus = TaskStatusViewModel.SendIntroductoryEmailTaskListStatus(SupportProject);
        AdviserVisitToSchoolTaskListStatus = TaskStatusViewModel.AdviserVisitToSchoolTaskListStatus(SupportProject);
        CompleteAndSaveAssessmentTemplateTaskListStatus = TaskStatusViewModel.CompleteAndSaveAssessmentTemplateTaskListStatus(SupportProject);
        NoteOfVisitTaskListStatus = TaskStatusViewModel.NoteOfVsistTaskListStatus(SupportProject);
        RecordVisitDateToVisitSchoolTaskListStatus = TaskStatusViewModel.RecordVisitDateToVisitSchoolTaskListStatus(SupportProject);
        ChosePreferredSupportingOrganisationTaskListStatus =
            TaskStatusViewModel.ChoosePreferredSupportingOrganisationTaskListStatus(SupportProject);
        RecordSupportDecisionTaskListStatus = TaskStatusViewModel.RecordSupportDecisionTaskListStatus(SupportProject);
        DueDiligenceOnPreferredSupportingOrganisationTaskListStatus = TaskStatusViewModel.DueDiligenceOnPreferredSupportingOrganisationTaskListStatus(SupportProject);
        SetRecordSupportingOrganisationAppointment = TaskStatusViewModel.SetRecordSupportingOrganisationAppointmentTaskListStatus(SupportProject);
        SupportingOrganisationContactDetailsTaskListStatus =
            TaskStatusViewModel.SupportingOrganisationContactDetailsTaskListStatus(SupportProject);
        ShareTheImprovementPlanTemplateTaskListStatus = TaskStatusViewModel.ShareTheImprovementPlanTemplateTaskListStatus(SupportProject);
        RecordImprovementPlanDecisionTaskListStatus = TaskStatusViewModel.RecordImprovementPlanDecisionTaskListStatus(SupportProject);
        SendAgreedImprovementPlanForApprovalTaskListStatus = TaskStatusViewModel.SendAgreedImprovementPlanForApprovalTaskListStatus(SupportProject);
        ReviewTheImprovementPlanTaskListStatus =
            TaskStatusViewModel.ReviewTheImprovementPlanTaskListStatus(SupportProject);
        return Page();
    }
}
