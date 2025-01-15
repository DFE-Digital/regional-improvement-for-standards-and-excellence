using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.SupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using System.Threading;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.AboutTheSchool;

public class IndexModel : BaseSupportProjectEstablishmentPageModel
{
   public string ReturnPage { get; set; }

   public void SetErrorPage(string errorPage)
   {
      TempData["ErrorPage"] = errorPage;
   }

   public IndexModel(ISupportProjectQueryService supportProjectQueryService, IGetEstablishment getEstablishment,ErrorService errorService,IMediator mediator) : base(supportProjectQueryService,getEstablishment,errorService)
   {
       
   }

   public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        ProjectListFilters.ClearFiltersFrom(TempData);

        ReturnPage = @Links.SchoolList.Index.Page;
        
        await base.GetSupportProject(id, cancellationToken);

        return Page();
    }
}
