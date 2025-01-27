using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.SupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages;

public class BaseSupportProjectPageModel(ISupportProjectQueryService supportProjectQueryService, ErrorService errorService) : PageModel
{
    protected readonly ISupportProjectQueryService _supportProjectQueryService = supportProjectQueryService;
    protected readonly ErrorService _errorService = errorService;
    public SupportProjectViewModel SupportProject { get; set; }

    public virtual async Task<IActionResult> GetSupportProject(int id, CancellationToken cancellationToken)
    {
        return await GetProject(id, cancellationToken);
    }
    protected async Task<IActionResult> GetProject(int id, CancellationToken cancellationToken)
    {

        var result = await _supportProjectQueryService.GetSupportProject(id, cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound();
        }

        SupportProject = SupportProjectViewModel.Create(result.Value!);
        return Page();
    }
}