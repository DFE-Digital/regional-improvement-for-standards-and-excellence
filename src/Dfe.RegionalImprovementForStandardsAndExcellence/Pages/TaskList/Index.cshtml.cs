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
    public TaskListStatus RecordSupportDecisionTaskListStatus {  get; set; }

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
        return Page();
    }
}
