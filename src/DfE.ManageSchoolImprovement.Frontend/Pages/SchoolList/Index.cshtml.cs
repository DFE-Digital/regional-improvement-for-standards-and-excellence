using DfE.ManageSchoolImprovement.Application.SupportProject.Queries;
using DfE.ManageSchoolImprovement.Frontend.Models;
using DfE.ManageSchoolImprovement.Frontend.Models.SupportProject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DfE.ManageSchoolImprovement.Frontend.Pages.SchoolList;

public class IndexModel : PageModel
{

    private readonly ISupportProjectQueryService _supportProjectQueryService;

    public IndexModel(ISupportProjectQueryService supportProjectQueryService)
    {
        _supportProjectQueryService = supportProjectQueryService;
    }

    public IEnumerable<SupportProjectViewModel> SupportProjects = [];

    [BindProperty]
    public ProjectListFilters Filters { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public PaginationViewModel Pagination { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int TotalProjects { get; set; }
    public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
    {
        Filters.PersistUsing(TempData).PopulateFrom(Request.Query);

        Pagination.PagePath = "/SchoolList/Index";

        var result =
           await _supportProjectQueryService.SearchForSupportProjects(
               Filters.Title, Filters.SelectedStatuses, Filters.SelectedOfficers, Filters.SelectedRegions,
               Filters.SelectedLocalAuthorities, Pagination.PagePath, Pagination.CurrentPage, Pagination.PageSize,
               cancellationToken);

        if (result.IsSuccess && result.Value != null)
        {
            Pagination.Paging = result.Value.Paging;
            TotalProjects = result.Value?.Paging?.RecordCount ?? 0;
            SupportProjects = result.Value?.Data.Select(SupportProjectViewModel.Create)!;
        }

        var regionsResult = await _supportProjectQueryService.GetAllProjectRegions(cancellationToken);

        if (regionsResult.IsSuccess && regionsResult.Value != null)
        {
            Filters.AvailableRegions = regionsResult.Value.ToList();
        }

        var localAuthoritiesResult = await _supportProjectQueryService.GetAllProjectLocalAuthorities(cancellationToken);

        if (localAuthoritiesResult.IsSuccess && localAuthoritiesResult.Value != null)
        {
            Filters.AvailableLocalAuthorities = localAuthoritiesResult.Value.ToList();
        }

        return Page();
    }
}
