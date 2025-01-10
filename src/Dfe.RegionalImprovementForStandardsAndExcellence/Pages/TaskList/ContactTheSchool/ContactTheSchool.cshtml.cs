using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.SupportProject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.TaskList.ContactTheSchool;

public class ContactTheSchoolModel(ISupportProjectQueryService supportProjectQueryService) : PageModel
{
    public SupportProjectViewModel SupportProject { get; set;}
    public string ReturnPage { get; set; }
    
    public DateTime SchoolContactedDate  { get; set; }
    
    public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
    {
        var result = await supportProjectQueryService.GetSupportProject(id, cancellationToken);
        
        
        SupportProject = SupportProjectViewModel.Create(result.Value);
        
        SchoolContactedDate = DateTime.Now;
        
        ReturnPage = @Links.TaskList.Index.Page;
        
        return Page(); 
    }
    
}