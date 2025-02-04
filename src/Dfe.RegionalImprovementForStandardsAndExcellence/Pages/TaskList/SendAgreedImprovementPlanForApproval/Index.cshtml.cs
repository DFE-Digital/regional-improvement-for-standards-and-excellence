using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.TaskList.SendAgreedImprovementPlanForApproval
{
    public class IndexModel(ISupportProjectQueryService supportProjectQueryService, ErrorService errorService, IMediator mediator) : BaseSupportProjectPageModel(supportProjectQueryService, errorService)
    {
        [BindProperty(Name = "save-agreed-improvement-plan-in-sp")]
        public bool? HasSavedImprovementPlanInSharePoint { get; set; }

        [BindProperty(Name = "email-agreed-plan-to-rg")]
        public bool? HasEmailedAgreedPlanToRegionalDirectorForApproval { get; set; } 
         

        public async Task<IActionResult> OnPost(int id, CancellationToken cancellationToken)
        {
            var request = new SetSendAgreedImprovementPlanForApprovalCommand(new SupportProjectId(id), HasSavedImprovementPlanInSharePoint, HasEmailedAgreedPlanToRegionalDirectorForApproval);

            var result = await mediator.Send(request, cancellationToken);

            if (!result)
            {
                _errorService.AddApiError();
                return await base.GetSupportProject(id, cancellationToken); ;
            }

            return RedirectToPage(@Links.TaskList.Index.Page, new { id });
        }

        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            await base.GetSupportProject(id, cancellationToken);
            HasSavedImprovementPlanInSharePoint = SupportProject.HasSavedImprovementPlanInSharePoint;
            HasEmailedAgreedPlanToRegionalDirectorForApproval = SupportProject.HasEmailedAgreedPlanToRegionalDirectorForApproval;
            return Page();
        }
    }
}
