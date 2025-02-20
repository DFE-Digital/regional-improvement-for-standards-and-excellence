using Dfe.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject;
using Dfe.ManageSchoolImprovement.Application.SupportProject.Queries;
using Dfe.ManageSchoolImprovement.Domain.ValueObjects;
using Dfe.ManageSchoolImprovement.Frontend.Models;
using Dfe.ManageSchoolImprovement.Frontend.Services;
using Dfe.ManageSchoolImprovement.Frontend.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc; 
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageSchoolImprovement.Frontend.Pages.TaskList.RecordSupportDecision
{
    public class IndexModel(ISupportProjectQueryService supportProjectQueryService, ErrorService errorService, IMediator mediator) : BaseSupportProjectPageModel(supportProjectQueryService, errorService), IDateValidationMessageProvider
    {
        [BindProperty(Name = "decision-date", BinderType = typeof(DateInputModelBinder))]
        [DateValidation(DateRangeValidationService.DateRange.PastOrToday)]
        [Display(Name = "record support decision")]
        public DateTime? RegionalDirectorDecisionDate { get; set; }

        [BindProperty(Name = "HasConfirmedSchoolGetTargetSupport")]
        public bool? HasConfirmedSchoolGetTargetSupport { get; set; }

        [BindProperty(Name = "DisapprovingTargetedSupportNotes")]
        public string? DisapprovingTargetedSupportNotes { get; set; }

        public required IList<RadioButtonsLabelViewModel> RadioButtoons { get; set; }

        public bool ShowError { get; set; }

        string IDateValidationMessageProvider.SomeMissing(string displayName, IEnumerable<string> missingParts)
        {
            return $"Date must include a {string.Join(" and ", missingParts)}";
        }

        string IDateValidationMessageProvider.AllMissing(string displayName)
        {
            return $"Enter the record support decision date";
        }

        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            await base.GetSupportProject(id, cancellationToken);
            HasConfirmedSchoolGetTargetSupport = SupportProject.HasConfirmedSchoolGetTargetSupport;
            RegionalDirectorDecisionDate = SupportProject.RegionalDirectorDecisionDate;
            DisapprovingTargetedSupportNotes = SupportProject.DisapprovingTargetedSupportNotes;
            RadioButtoons = RadioButtons;
            return Page();
        }
        public async Task<IActionResult> OnPost(int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid || !IsDisapprovingTargetedSupportNotesValid())
            {
                RadioButtoons = RadioButtons;
                _errorService.AddErrors(Request.Form.Keys, ModelState);
                ShowError = true;
                return await base.GetSupportProject(id, cancellationToken);
            }

            var request = new SetRecordSupportDecisionCommand(new SupportProjectId(id), RegionalDirectorDecisionDate, HasConfirmedSchoolGetTargetSupport, DisapprovingTargetedSupportNotes);

            var result = await mediator.Send(request, cancellationToken);

            if (!result)
            {
                _errorService.AddApiError();
                return await base.GetSupportProject(id, cancellationToken); ;
            }

            return RedirectToPage(@Links.TaskList.Index.Page, new { id });
        }


        private IList<RadioButtonsLabelViewModel> RadioButtons
        {
            get
            {
                var list = new List<RadioButtonsLabelViewModel>
                {
                    new() {
                        Id = "yes",
                        Name = "Yes, school to get targeted support",
                        Value = "True"
                    },
                    new() {
                        Id = "no",
                        Name = "No, school to be monitored",
                        Value = "False",
                        Input = new TextAreaInputViewModel
                        {
                            Id = nameof(DisapprovingTargetedSupportNotes),
                            ValidationMessage = "You must add a note",
                            Paragraph = "Provide some details about why approval was not given.",
                            Value = DisapprovingTargetedSupportNotes,
                            IsValid = IsDisapprovingTargetedSupportNotesValid()
                        }
                    }
                };

                return list;
            }
        }
        private bool IsDisapprovingTargetedSupportNotesValid()
        { 
            if (HasConfirmedSchoolGetTargetSupport == false && string.IsNullOrWhiteSpace(DisapprovingTargetedSupportNotes))
            {
                return false;
            }
            return true;
        }
    }
}
