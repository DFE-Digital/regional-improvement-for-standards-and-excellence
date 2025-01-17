using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject.SetAdviserConflictOfInterestDetails;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.TaskList.AdviserConflictOfIntereset;

public class AdviserConflictOfIntereset(ISupportProjectQueryService supportProjectQueryService, ErrorService errorService, IMediator mediator) : BaseSupportProjectPageModel(supportProjectQueryService, errorService), IDateValidationMessageProvider
{
    [BindProperty(Name = "send-conflict-of-interest-form-to-proposed-adviser-and-the-school")]
    public bool? SendConflictOfInterestFormToProposedAdviserAndTheSchool { get; set; }

    [BindProperty(Name = "recieve-completed-conflict-of-interest-form")]
    public bool? RecieveCompletedConflictOfInteresetForm { get; set; }

    [BindProperty(Name = "save-completed-conflict-of-interest-form-in-sharepoint")]
    public bool? SaveCompletedConflictOfinterestFormInSharePoint { get; set; }

    [BindProperty(Name = "date-conflicts-of-interest-were-checked", BinderType = typeof(DateInputModelBinder))]
    [DateValidation(DateRangeValidationService.DateRange.PastOrToday)]
    [Display(Name = "date conflicts of interest were checked")]
    public DateTime? DateConflictsOfInterestWereChecked { get; set; }

    public bool ShowError { get; set; }

    string IDateValidationMessageProvider.SomeMissing(string displayName, IEnumerable<string> missingParts)
    {
        return $"Date must include a {string.Join(" and ", missingParts)}";
    }

    string IDateValidationMessageProvider.AllMissing(string displayName)
    {
        return $"Enter the school contacted date";
    }

    public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
    {
        await base.GetSupportProject(id, cancellationToken);

        SendConflictOfInterestFormToProposedAdviserAndTheSchool = SupportProject.SendConflictOfInterestFormToProposedAdviserAndTheSchool;

        RecieveCompletedConflictOfInteresetForm = SupportProject.RecieveCompletedConflictOfInteresetForm;

        SaveCompletedConflictOfinterestFormInSharePoint = SupportProject.SaveCompletedConflictOfinterestFormInSharePoint;

        DateConflictsOfInterestWereChecked = SupportProject.DateConflictsOfInterestWereChecked;

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

        var request = new SetAdviserConflictOfInterestDetailsCommand(new SupportProjectId(id), SendConflictOfInterestFormToProposedAdviserAndTheSchool, RecieveCompletedConflictOfInteresetForm, SaveCompletedConflictOfinterestFormInSharePoint, DateConflictsOfInterestWereChecked);

        var result = await mediator.Send(request, cancellationToken);

        if (result != true)
        {
            _errorService.AddApiError();
            return await base.GetSupportProject(id, cancellationToken); ;
        }

        return RedirectToPage(@Links.TaskList.Index.Page, new { id });
    }

}