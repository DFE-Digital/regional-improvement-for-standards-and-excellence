using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject.SetRequestPlanningGrantOfferLetterDetails;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.TaskList.RequestPlanningGrantOfferLetter;

public class IndexModel(ISupportProjectQueryService supportProjectQueryService, ErrorService errorService, IMediator mediator) : BaseSupportProjectPageModel(supportProjectQueryService, errorService), IDateValidationMessageProvider
{
    [BindProperty(Name = "date-grant-team-contacted", BinderType = typeof(DateInputModelBinder))]
    [DateValidation(DateRangeValidationService.DateRange.PastOrToday)]
    [Display(Name = "date grant team contacted")]
    public DateTime? DateGrantTeamContacted { get; set; }

    public bool ShowError { get; set; }

    string IDateValidationMessageProvider.SomeMissing(string displayName, IEnumerable<string> missingParts)
    {
        return $"Date must include a {string.Join(" and ", missingParts)}";
    }

    string IDateValidationMessageProvider.AllMissing(string displayName)
    {
        return $"Enter the date grant team contacted";
    }

    public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
    {
        await base.GetSupportProject(id, cancellationToken);

        DateGrantTeamContacted = SupportProject.DateTeamContactedForRequestingPlanningGrantOfferLetter;

        return Page();
    }

    public async Task<IActionResult> OnPost(int id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(Request.Form.Keys, ModelState);
            ShowError = true;
            return await base.GetSupportProject(id, cancellationToken);
        }

        var request = new SetRequestPlanningGrantOfferLetterDetailsCommand(new SupportProjectId(id), DateGrantTeamContacted);

        var result = await mediator.Send(request, cancellationToken);

        if (result != true)
        {
            _errorService.AddApiError();
            return await base.GetSupportProject(id, cancellationToken); ;
        }

        return RedirectToPage(@Links.TaskList.Index.Page, new { id });
    }

}