using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject.SetNoteOfVisitDetails;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.TaskList.NoteOfVisit;

public class NoteOfVisit(ISupportProjectQueryService supportProjectQueryService, ErrorService errorService, IMediator mediator) : BaseSupportProjectPageModel(supportProjectQueryService, errorService), IDateValidationMessageProvider
{
    [BindProperty(Name = "give-the-adviser-the-note-of-visit-template")]
    public bool? GiveTheAdviserTheNoteOfVisitTemplate { get; set; }

    [BindProperty(Name = "ask-the-adviser-to-send-you-their-notes")]
    public bool? AskTheAdviserToSendYouTheirNotes { get; set; }

    [BindProperty(Name = "enter-date-note-of-visit-saved-in-sharepoint", BinderType = typeof(DateInputModelBinder))]
    [DateValidation(DateRangeValidationService.DateRange.PastOrToday)]
    [Display(Name = "date Note of Visit was saved in SharePoint")]
    public DateTime? DateNoteOfVisitSavedInSharePoint { get; set; }

    public bool ShowError { get; set; }

    string IDateValidationMessageProvider.SomeMissing(string displayName, IEnumerable<string> missingParts)
    {
        return $"Date must include a {string.Join(" and ", missingParts)}";
    }

    string IDateValidationMessageProvider.AllMissing(string displayName)
    {
        return $"Enter the date Note of Visit was saved in SharePoint";
    }

    public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
    {
        await base.GetSupportProject(id, cancellationToken);

        GiveTheAdviserTheNoteOfVisitTemplate = SupportProject.GiveTheAdviserTheNoteOfVisitTemplate;
        AskTheAdviserToSendYouTheirNotes = SupportProject.AskTheAdviserToSendYouTheirNotes;
        DateNoteOfVisitSavedInSharePoint = SupportProject.DateNoteOfVisitSavedInSharePoint;

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

        var request = new SetNoteOfVisitDetailsCommand(new SupportProjectId(id), GiveTheAdviserTheNoteOfVisitTemplate, AskTheAdviserToSendYouTheirNotes, DateNoteOfVisitSavedInSharePoint);

        var result = await mediator.Send(request, cancellationToken);

        if (result != true)
        {
            _errorService.AddApiError();
            return await base.GetSupportProject(id, cancellationToken); ;
        }

        return RedirectToPage(@Links.TaskList.Index.Page, new { id });
    }

}