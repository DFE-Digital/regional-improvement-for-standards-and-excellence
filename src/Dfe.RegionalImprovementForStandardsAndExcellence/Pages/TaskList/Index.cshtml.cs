using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.SupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using System.Threading;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.TaskList;

public class IndexModel(
    ISupportProjectQueryService _supportProjectQueryService,
                  ErrorService errorService
    ) : PageModel
{
   //protected readonly ISession _session = session;
   public SupportProjectViewModel SupportProject { get; set; }
   public string ReturnPage { get; set; }

   public void SetErrorPage(string errorPage)
   {
      TempData["ErrorPage"] = errorPage;
   }

   public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        ProjectListFilters.ClearFiltersFrom(TempData);

        var result = await _supportProjectQueryService.GetSupportProject(id, cancellationToken);

        if (result.IsSuccess && result.Value != null)
        {
            SupportProject = SupportProjectViewModel.Create(result.Value);
        }

        ReturnPage = @Links.ProjectList.Index.Page;

        if (!result.IsSuccess)
        {
            return NotFound();
        }

        return Page();
   }
}
