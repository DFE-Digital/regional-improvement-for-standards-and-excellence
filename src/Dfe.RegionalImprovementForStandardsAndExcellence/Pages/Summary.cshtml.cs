using Dfe.Academies.Contracts.V4.Trusts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using Dfe.RegionalImprovementForStandardsAndExcellence.Data.Services;
using Dfe.RegionalImprovementForStandardsAndExcellence.Mappings;
using Dfe.RegionalImprovementForStandardsAndExcellence.Models;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Pages;

public class SummaryModel : PageModel
{
   private readonly IGetEstablishment _getEstablishment;
   private readonly ISupportProjectRepository _supportProjectRepository;

   public SummaryModel(IGetEstablishment getEstablishment,
                       ISupportProjectRepository supportProjectRepository)
   {
      _getEstablishment = getEstablishment;
      _supportProjectRepository = supportProjectRepository;
   }

   public Academies.Contracts.V4.Establishments.EstablishmentDto Establishment { get; set; }
   
   public async Task<IActionResult> OnGetAsync(string urn)
   {
      Establishment = await _getEstablishment.GetEstablishmentByUrn(urn);
      
      return Page();
   }

   public async Task<IActionResult> OnPostAsync(string urn)
   {
      Academies.Contracts.V4.Establishments.EstablishmentDto establishment = await _getEstablishment.GetEstablishmentByUrn(urn);
      

      var result = await _supportProjectRepository.CreateSupportProject(CreateSupportProjectMapper.MapToDto(establishment));

      if (result.Success)
      {
         return RedirectToPage(Links.ProjectList.Index.Page);
      }

      else
      {
         return RedirectToPage(Links.NewProject.Summary.Page);
      }
   }
   
}
