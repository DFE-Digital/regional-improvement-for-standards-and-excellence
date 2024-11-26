
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.SupportProject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages;

public class IndexModel : PageModel
{
    
    //private readonly ISupportProjectRepository _repository;

    //public IndexModel(ISupportProjectRepository repository)
    //{
    //    _repository = repository;
    //}

    public IEnumerable<SupportProjectViewModel> SupportProjects;
    
    [BindProperty]
    public ProjectListFilters Filters { get; set; } = new();
    
    public void OnGet()
    {
        //SupportProjects = _repository.GetAllSupportProjects().Result.Body;
    }
}