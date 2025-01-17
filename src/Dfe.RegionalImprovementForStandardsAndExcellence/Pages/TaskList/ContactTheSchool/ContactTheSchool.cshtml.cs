using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.SupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.TaskList.ContactTheSchool;

public class ContactTheSchoolModel(ISupportProjectQueryService supportProjectQueryService,ErrorService errorService, IMediator mediator) : BaseSupportProjectPageModel(supportProjectQueryService,errorService),IDateValidationMessageProvider
{
    
    [BindProperty(Name = "school-contacted-date", BinderType = typeof(DateInputModelBinder))]
    [DateValidation(Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services.DateRangeValidationService.DateRange.PastOrToday)]
    [Display(Name = "Contacted the school")]
    public DateTime? SchoolContactedDate  { get; set; }
    
    [BindProperty(Name = "school-email-address-found")]
    public bool? SchoolEmailAddressFound { get; set; }
    
    [BindProperty(Name = "notification-letter-to-create-email")]
    
    public bool? UseTheNotificationLetterToCreateEmail { get; set; }
    
    [BindProperty(Name = "attach-rise-info-to-email")]
    
    public bool? AttachRiseInfoToEmail { get; set; }
    
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
        
        SchoolContactedDate = SupportProject.ContactedTheSchoolDate ?? null;

        SchoolEmailAddressFound = SupportProject.FindSchoolEmailAddress;

        UseTheNotificationLetterToCreateEmail = SupportProject.UseTheNotificationLetterToCreateEmail;

        AttachRiseInfoToEmail = SupportProject.AttachRiseInfoToEmail;
        
        return Page(); 
    }

    public async Task<IActionResult> OnPost(int id,CancellationToken cancellationToken)
    {
      
        
        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(Request.Form.Keys, ModelState);
            ShowError = true;
            return await base.GetSupportProject(id, cancellationToken);
        }
        
        var request = new SetContactTheSchoolDetailsCommand(new SupportProjectId(id),  SchoolEmailAddressFound ,UseTheNotificationLetterToCreateEmail,AttachRiseInfoToEmail,SchoolContactedDate );

        var result = await mediator.Send(request, cancellationToken);
       
        if (result != true)
        {
            _errorService.AddApiError();
            return await base.GetSupportProject(id, cancellationToken);;
        }
        
        return RedirectToPage(@Links.TaskList.Index.Page, new { id });
    }

}