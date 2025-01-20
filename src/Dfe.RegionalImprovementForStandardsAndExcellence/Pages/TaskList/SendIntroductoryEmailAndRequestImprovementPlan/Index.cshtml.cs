using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.SetSendIntroEmailAndRequestImprovementPlan;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.TaskList.SendIntroductoryEmailAndRequestImprovementPlan
{
    public class IndexModel(ISupportProjectQueryService supportProjectQueryService, ErrorService errorService, IMediator mediator) : BaseSupportProjectPageModel(supportProjectQueryService, errorService), IDateValidationMessageProvider
    {
        [BindProperty(Name = "introductory-email-sent-date", BinderType = typeof(DateInputModelBinder))]
        [DateValidation(DateRangeValidationService.DateRange.PastOrToday)]
        public DateTime? IntroductoryEmailSentDate { get; set; }

        [BindProperty(Name = "share-email-template-with-advisor")]
        public bool? HasShareEmailTemplateWithAdvisor { get; set; }

        [BindProperty(Name = "remind-advisor-to-copy-in-rise-team-on-email-sent")]
        public bool? RemindAdvisorToCopyRiseTeamWhenSentEmail { get; set; }

        public bool ShowError { get; set; }

        string IDateValidationMessageProvider.SomeMissing(string displayName, IEnumerable<string> missingParts)
        {
            return $"Date must include a {string.Join(" and ", missingParts)}";
        }

        string IDateValidationMessageProvider.AllMissing(string displayName)
        {
            return $"Enter the saved school's resonpse date in SharePoint";
        }

        public async Task<IActionResult> OnPost(int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            { 
                _errorService.AddErrors(Request.Form.Keys, ModelState);
                ShowError = true;
                return await base.GetSupportProject(id, cancellationToken);
            }

            var request = new SetSendIntroEmailAndRequestImprovementPlanCommand(new SupportProjectId(id), IntroductoryEmailSentDate, HasShareEmailTemplateWithAdvisor, RemindAdvisorToCopyRiseTeamWhenSentEmail);

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
            HasShareEmailTemplateWithAdvisor = SupportProject.HasShareEmailTemplateWithAdvisor;
            RemindAdvisorToCopyRiseTeamWhenSentEmail = SupportProject.RemindAdvisorToCopyRiseTeamWhenSentEmail;
            IntroductoryEmailSentDate = SupportProject.IntroductoryEmailSentDate; 
            return Page();
        }
    }
}
