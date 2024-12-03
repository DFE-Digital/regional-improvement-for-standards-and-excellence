
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

    public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
    {
        var result = await _supportProjectQueryService.GetAllSupportProjects(cancellationToken);

        if(result.IsSuccess && result.Value != null) {
            SupportProjects = result.Value.Select(SupportProjectViewModel.Create);
        }

        return Page();
    }
}