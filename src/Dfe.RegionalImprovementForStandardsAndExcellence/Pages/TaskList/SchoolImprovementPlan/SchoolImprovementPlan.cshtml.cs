using Microsoft.AspNetCore.Mvc;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.SupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.TaskList.SchoolImprovementPlan
{
    public class SchoolImprovementPlan(ISupportProjectQueryService supportProjectQueryService) : PageModel
    {
        public string ReturnPage { get; set; }
        public string ReturnId { get; set; }

        public SupportProjectViewModel SupportProject { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {

            ReturnPage = @Links.ProjectList.Index.Page;

            var result = await supportProjectQueryService.GetSupportProject(id, cancellationToken);

            if (result.IsSuccess && result.Value != null)
            {
                SupportProject = SupportProjectViewModel.Create(result.Value);
            }

            return Page();
        }
    }
}


