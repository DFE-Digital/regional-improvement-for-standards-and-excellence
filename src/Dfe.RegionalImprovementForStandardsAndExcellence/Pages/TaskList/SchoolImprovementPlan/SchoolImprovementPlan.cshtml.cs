using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Dfe.RegionalImprovementForStandardsAndExcellence.Data;
using Dfe.RegionalImprovementForStandardsAndExcellence.Data.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Data.Services;
using Dfe.RegionalImprovementForStandardsAndExcellence.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Services;
using Dfe.RegionalImprovementForStandardsAndExcellence.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Pages.TaskList.SchoolImprovementPlan
{
   public class SchoolImprovementPlan(ISupportProjectRepository _supportProjectRepository, ErrorService errorService) : PageModel
   {
      public string ReturnPage { get; set; }
      public string ReturnId { get; set; }
      
      public SupportProjectViewModel SupportProject { get; set; }

      public async  Task<IActionResult> OnGetAsync(string id)
      {
         
         ReturnPage = @Links.ProjectList.Index.Page;
         
         ApiResponse<SupportProject> result = await _supportProjectRepository.GetSupportProject(id);

         SupportProject = SupportProjectViewModel.Build(result.Body);
         
         return Page();
      }
   }
}


