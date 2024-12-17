using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using MediatR;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.CreateSupportProject;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages;

public class SummaryModel : PageModel
{
    private readonly IGetEstablishment _getEstablishment;
    private readonly ISupportProjectQueryService _supportProjectQueryService;
    private readonly IMediator _mediator;

    public SummaryModel(IGetEstablishment getEstablishment,
                        ISupportProjectQueryService supportProjectQueryService, 
                        IMediator mediator
        )
    {
        _getEstablishment = getEstablishment;
        _supportProjectQueryService = supportProjectQueryService;
        _mediator = mediator;
    }

    public DfE.CoreLibs.Contracts.Academies.V4.Establishments.EstablishmentDto Establishment { get; set; }

    public async Task<IActionResult> OnGetAsync(string urn)
    {
        Establishment = await _getEstablishment.GetEstablishmentByUrn(urn);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string urn)
    {
        DfE.CoreLibs.Contracts.Academies.V4.Establishments.EstablishmentDto establishment = await _getEstablishment.GetEstablishmentByUrn(urn);

        var request = new CreateSupportProjectCommand(establishment.Name, establishment.Urn, establishment.LocalAuthorityName,establishment.Gor.Name, "Test", "user@test.com");

        var id = await _mediator.Send(request);

        if (id != null)
        {
            return RedirectToPage(Links.ProjectList.Index.Page);
        }

        else
        {
            return RedirectToPage(Links.NewProject.Summary.Page);
        }
    }

}
