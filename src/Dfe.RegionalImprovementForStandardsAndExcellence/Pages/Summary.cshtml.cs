using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages;

public class SummaryModel : PageModel
{
   private readonly IGetEstablishment _getEstablishment;
   private readonly ISupportProjectQueryService _supportProjectQueryService;

   public SummaryModel(IGetEstablishment getEstablishment,
                       ISupportProjectQueryService supportProjectQueryService
       )
   {
      _getEstablishment = getEstablishment;
      _supportProjectQueryService = supportProjectQueryService;
   }

   public DfE.CoreLibs.Contracts.Academies.V4.Establishments.EstablishmentDto Establishment { get; set; }
   
   public async Task<IActionResult> OnGetAsync(string urn)
   {
      Establishment = await _getEstablishment.GetEstablishmentByUrn(urn);
      
      return Page();
   }

   //public async Task<IActionResult> OnPostAsync(string urn)
   //{
   //   DfE.CoreLibs.Contracts.Academies.V4.Establishments.EstablishmentDto establishment = await _getEstablishment.GetEstablishmentByUrn(urn);
      

   //   //var result = await _supportProjectQueryService.CreateSupportProject(CreateSupportProjectMapper.MapToDto(establishment));

   //   //if (result.Success)
   //   //{
   //   //   return RedirectToPage(Links.ProjectList.Index.Page);
   //   //}

   //   //else
   //   //{
   //   //   return RedirectToPage(Links.NewProject.Summary.Page);
   //   //}
   //}
   
}
