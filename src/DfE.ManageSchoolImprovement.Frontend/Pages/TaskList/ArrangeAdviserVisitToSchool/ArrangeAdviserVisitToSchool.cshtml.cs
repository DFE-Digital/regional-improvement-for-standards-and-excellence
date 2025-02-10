using System.ComponentModel.DataAnnotations;
using DfE.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject;
using DfE.ManageSchoolImprovement.Application.SupportProject.Queries;
using DfE.ManageSchoolImprovement.Domain.ValueObjects;
using DfE.ManageSchoolImprovement.Frontend.Models;
using DfE.ManageSchoolImprovement.Frontend.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DfE.ManageSchoolImprovement.Frontend.Pages.TaskList.ArrangeAdviserVisitToSchool;

public class ArrangeAdviserVisitToSchoolModel(ISupportProjectQueryService supportProjectQueryService,ErrorService errorService, IMediator mediator) : BaseSupportProjectPageModel(supportProjectQueryService,errorService),IDateValidationMessageProvider
{
    
    [BindProperty(Name = "adviser-visit-date", BinderType = typeof(DateInputModelBinder))]
    [DateValidation(DfE.ManageSchoolImprovement.Frontend.Services.DateRangeValidationService.DateRange.FutureOrToday)]
    [Display(Name = "Adviser visit date")]
    
    public DateTime? AdviserVisitDate  { get; set; }
    
    public bool ShowError { get; set; }
    string IDateValidationMessageProvider.SomeMissing(string displayName, IEnumerable<string> missingParts)
    {
        return $"Date must include a {string.Join(" and ", missingParts)}";
    }

    string IDateValidationMessageProvider.AllMissing(string displayName)
    {
        return $"Enter the adviser visit school date";
    }
    
    public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
    {
        
        await base.GetSupportProject(id, cancellationToken);
        
        AdviserVisitDate = SupportProject.AdviserVisitDate ?? null;
        
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
        
        var request = new SetAdviserVisitDateCommand(new SupportProjectId(id),AdviserVisitDate);

        var result = await mediator.Send(request, cancellationToken);
       
        if (result != true)
        {
            _errorService.AddApiError();
            return await base.GetSupportProject(id, cancellationToken);
        }
        
        return RedirectToPage(@Links.TaskList.Index.Page, new { id });
    }
}
