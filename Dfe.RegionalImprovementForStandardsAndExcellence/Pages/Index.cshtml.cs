using Dfe.RegionalImprovementForStandardsAndExcellence.Data.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Data.Services;
using Dfe.RegionalImprovementForStandardsAndExcellence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Pages;

public class IndexModel : PageModel
{
    
    private readonly ISupportProjectRepository _repository;

    public IndexModel(ISupportProjectRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<SupportProject> SupportProjects;
    
    [BindProperty]
    public ProjectListFilters Filters { get; set; } = new();
    
    public void OnGet()
    {
        SupportProjects = _repository.GetAllSupportProjects().Result.Body;
    }
}