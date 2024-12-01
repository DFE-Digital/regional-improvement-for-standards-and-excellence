using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.SupportProject;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.TaskList;

public class IndexModel(
    //ISupportProjectRepository _supportProjectRepository,
                  //ErrorService errorService
    ) : PageModel
{
   //protected readonly ISession _session = session;
   public SupportProjectViewModel SupportProject { get; set; }
   public string ReturnPage { get; set; }

   public void SetErrorPage(string errorPage)
   {
      TempData["ErrorPage"] = errorPage;
   }

   public async Task<IActionResult> OnGetAsync(string id)
   {
      ProjectListFilters.ClearFiltersFrom(TempData);

     // ApiResponse<SupportProject> result = await _supportProjectRepository.GetSupportProject(id);

      //SupportProject = SupportProjectViewModel.Build(result.Body);
      
      ReturnPage = @Links.ProjectList.Index.Page;
      
      //if (result.StatusCode != HttpStatusCode.OK)
      //{
      //   return NotFound();
      //}
      
      return Page();
   }
}
