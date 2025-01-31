using System.ComponentModel.DataAnnotations;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.TaskList.AddSupportingOrganisationContactDetails;

public class IndexModel(ISupportProjectQueryService supportProjectQueryService,ErrorService errorService, IMediator mediator) : BaseSupportProjectPageModel(supportProjectQueryService,errorService),IDateValidationMessageProvider
{
    [BindProperty(Name = "name")]
    public string? Name  { get; set; }
    
    [BindProperty(Name = "email-address")]
    
    public string? EmailAddress  { get; set; }
    
    public bool ShowError { get; set; }
    

    
    [BindProperty(Name = "date-supporting-organisation-details-added", BinderType = typeof(DateInputModelBinder))]
    [DateValidation(Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services.DateRangeValidationService.DateRange.PastOrToday)]
    
    public DateTime? DateSupportingOrganisationDetailsAdded  { get; set; }
    
    string IDateValidationMessageProvider.SomeMissing(string displayName, IEnumerable<string> missingParts)
    {
        return $"Date must include a {string.Join(" and ", missingParts)}";
    }

    string IDateValidationMessageProvider.AllMissing(string displayName)
    {
        return $"Enter the date the supporting organisation contact details were added";
    }
    
    public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
    {
        await base.GetSupportProject(id, cancellationToken);
        
        Name = SupportProject.SupportOrganisationName;
        EmailAddress = SupportProject.SupportOrganisationIdNumber;
        DateSupportingOrganisationDetailsAdded = SupportProject.DateSupportOrganisationChosen;

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
        
        //var request = new SetChoosePreferredSupportingOrganisationCommand(new SupportProjectId(id),  OrganisationName ,IdNumber,DateSupportOrganisationChosen );

        //var result = await mediator.Send(request, cancellationToken);
       
        if (true)
        {
            _errorService.AddApiError();
            return await base.GetSupportProject(id, cancellationToken);
        }
        
        return RedirectToPage(@Links.TaskList.Index.Page, new { id });
    }

}