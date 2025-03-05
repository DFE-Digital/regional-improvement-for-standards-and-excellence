using Dfe.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject;
using Dfe.ManageSchoolImprovement.Application.SupportProject.Queries;
using Dfe.ManageSchoolImprovement.Domain.ValueObjects;
using Dfe.ManageSchoolImprovement.Frontend.Models;
using Dfe.ManageSchoolImprovement.Frontend.Services;
using Dfe.ManageSchoolImprovement.Frontend.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc; 
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageSchoolImprovement.Frontend.Pages.TaskList.RecordMatchingDecision
{
    public class IndexModel(ISupportProjectQueryService supportProjectQueryService, ErrorService errorService, IMediator mediator) : BaseSupportProjectPageModel(supportProjectQueryService, errorService), IDateValidationMessageProvider
    {
        [BindProperty(Name = "decision-date", BinderType = typeof(DateInputModelBinder))]
        [DateValidation(DateRangeValidationService.DateRange.PastOrToday)]
        [Display(Name = "record matching decision")]
        public DateTime? RegionalDirectorDecisionDate { get; set; }

        [BindProperty(Name = "HasSchoolMatchedWithHighQualityOrganisation")]
        public bool? HasSchoolMatchedWithHighQualityOrganisation { get; set; }

        [BindProperty(Name = "NotMatchingSchoolWithHighQualityOrgNotes")]
        public string? NotMatchingSchoolWithHighQualityOrgNotes { get; set; }

        public required IList<RadioButtonsLabelViewModel> RadioButtoons { get; set; }

        public bool ShowError { get; set; }

        string IDateValidationMessageProvider.SomeMissing(string displayName, IEnumerable<string> missingParts)
        {
            return $"Date must include a {string.Join(" and ", missingParts)}";
        }

        string IDateValidationMessageProvider.AllMissing(string displayName)
        {
            return $"Enter the record matching decision date";
        }

        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            await base.GetSupportProject(id, cancellationToken);
            HasSchoolMatchedWithHighQualityOrganisation = SupportProject.HasSchoolMatchedWithHighQualityOrganisation;
            RegionalDirectorDecisionDate = SupportProject.RegionalDirectorDecisionDate;
            NotMatchingSchoolWithHighQualityOrgNotes = SupportProject.NotMatchingSchoolWithHighQualityOrgNotes;
            RadioButtoons = RadioButtons;
            return Page();
        }
        public async Task<IActionResult> OnPost(int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid || !IsNotMatchingSchoolWithHighQualityOrgNotesValid())
            {
                RadioButtoons = RadioButtons;
                _errorService.AddErrors(Request.Form.Keys, ModelState);
                ShowError = true;
                return await base.GetSupportProject(id, cancellationToken);
            }

            var request = new SetRecordMatchingDecisionCommand(new SupportProjectId(id), RegionalDirectorDecisionDate, HasSchoolMatchedWithHighQualityOrganisation, NotMatchingSchoolWithHighQualityOrgNotes);

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
                        Name = "Yes, school to be matched",
                        Value = "True"
                    },
                    new() {
                        Id = "no",
                        Name = "No, school will not be matched",
                        Value = "False",
                        Input = new TextAreaInputViewModel
                        {
                            Id = nameof(NotMatchingSchoolWithHighQualityOrgNotes),
                            ValidationMessage = "You must add a note",
                            Paragraph = "Provide some details about why approval was not given.",
                            Value = NotMatchingSchoolWithHighQualityOrgNotes,
                            IsValid = IsNotMatchingSchoolWithHighQualityOrgNotesValid()
                        }
                    }
                };

                return list;
            }
        }
        private bool IsNotMatchingSchoolWithHighQualityOrgNotesValid()
        { 
            if (HasSchoolMatchedWithHighQualityOrganisation == false && string.IsNullOrWhiteSpace(NotMatchingSchoolWithHighQualityOrgNotes))
            {
                return false;
            }
            return true;
        }
    }
}
