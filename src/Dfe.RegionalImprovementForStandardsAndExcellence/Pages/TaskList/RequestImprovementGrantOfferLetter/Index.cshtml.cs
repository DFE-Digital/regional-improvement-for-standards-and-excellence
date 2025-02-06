using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.TaskList.RequestImprovementGrantOfferLetter
{
    public class IndexModel(ISupportProjectQueryService supportProjectQueryService, ErrorService errorService, IMediator mediator, IConfiguration configuration) : BaseSupportProjectPageModel(supportProjectQueryService, errorService), IDateValidationMessageProvider
    {
        [BindProperty(Name = "grant-team-contacted-date", BinderType = typeof(DateInputModelBinder))]
        [DateValidation(DateRangeValidationService.DateRange.PastOrToday)]
        [Display(Name = "grant team contacted date")]
        public DateTime? GrantTeamContactedDate { get; set; }

        public string? EmailAddress { get; set; }

        public bool ShowError { get; set; }

        string IDateValidationMessageProvider.SomeMissing(string displayName, IEnumerable<string> missingParts)
        {
            return $"Date must include a {string.Join(" and ", missingParts)}";
        }

        string IDateValidationMessageProvider.AllMissing(string displayName)
        {
            return $"Enter the introductory email's sent date.";
        }

        public async Task<IActionResult> OnPost(int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(Request.Form.Keys, ModelState);
                ShowError = true;
                return await base.GetSupportProject(id, cancellationToken);
            }

            var request = new SetRequestImprovementGrantOfferLetterCommand(new SupportProjectId(id), GrantTeamContactedDate);

            var result = await mediator.Send(request, cancellationToken);

            if (!result)
            {
                _errorService.AddApiError();
                return await base.GetSupportProject(id, cancellationToken);
            }

            return RedirectToPage(@Links.TaskList.Index.Page, new { id });
        }

        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            await base.GetSupportProject(id, cancellationToken);
            GrantTeamContactedDate = SupportProject.DateTeamContactedForRequestingImprovementGrantOfferLetter;
            EmailAddress = configuration.GetValue<string>("EmailForSendImprovementGrantOfferLetter") ?? string.Empty;
            return Page();
        }
    }
}
