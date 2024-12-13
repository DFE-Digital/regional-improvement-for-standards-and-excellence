using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.SupportProject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages;

public class IndexModel : PageModel
{

    private readonly ISupportProjectQueryService _supportProjectQueryService;

    public IndexModel(ISupportProjectQueryService supportProjectQueryService)
    {
        _supportProjectQueryService = supportProjectQueryService;
    }

    public IEnumerable<SupportProjectViewModel> SupportProjects = new List<SupportProjectViewModel>();
    
    [BindProperty]
    public ProjectListFilters Filters { get; set; } = new();

    public PaginationViewModel Pagination { get; set; } = new();
    public int ProjectCount => SupportProjects.Count();


    [BindProperty(SupportsGet = true)]
    public int TotalProjects { get; set; }
    public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
    {
        Filters.PersistUsing(TempData).PopulateFrom(Request.Query);

        Pagination.PagePath = "/schools-requiring-improvement";

        var result =
           await _supportProjectQueryService.SearchForSupportProjects(
               Filters.Title, Filters.SelectedStatuses, Filters.SelectedOfficers, Filters.SelectedRegions,
               Filters.SelectedLocalAuthorities, Pagination.PagePath, Pagination.CurrentPage, Pagination.PageSize,
               cancellationToken);

        //var result = await _supportProjectQueryService.GetAllSupportProjects(cancellationToken);

        if(result.IsSuccess && result.Value != null) {
            Pagination.Paging = result.Value.Paging;
            TotalProjects = result.Value?.Paging?.RecordCount ?? 0;
            SupportProjects = result.Value?.Data.Select(SupportProjectViewModel.Create);
        }

        return Page();
    }
}