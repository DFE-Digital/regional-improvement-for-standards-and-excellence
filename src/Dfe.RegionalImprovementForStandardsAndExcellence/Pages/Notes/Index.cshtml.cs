using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.Notes;

public class IndexModel(ISupportProjectQueryService supportProjectQueryService, IGetEstablishment getEstablishment,ErrorService errorService) : BaseSupportProjectEstablishmentPageModel(supportProjectQueryService, getEstablishment,errorService)
{
    public string ReturnPage { get; set; }
    
    public bool NewNote { get; set; }
    public bool EditNote { get; set; }
    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        ProjectListFilters.ClearFiltersFrom(TempData);
        
        NewNote = (bool)(TempData["newNote"] ?? false);
        
        EditNote = (bool)(TempData["editNote"] ?? false);

        ReturnPage = @Links.ProjectList.Index.Page;
        
        await base.GetSupportProject(id, cancellationToken);
        
        return Page();
    }
    
}