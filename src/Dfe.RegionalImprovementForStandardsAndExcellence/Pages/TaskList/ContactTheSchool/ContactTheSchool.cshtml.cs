using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.SupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.TaskList.ContactTheSchool;

public class ContactTheSchoolModel(ISupportProjectQueryService supportProjectQueryService,ErrorService errorService) : BaseSupportProjectPageModel(supportProjectQueryService,errorService),IDateValidationMessageProvider
{
    
    [BindProperty(Name = "school-contacted-date", BinderType = typeof(DateInputModelBinder))]
    public DateTime? SchoolContactedDate  { get; set; }
    
    [BindProperty(Name = "school-email-address-found")]
    public bool? SchoolEmailAddressFound { get; set; }
    
    [BindProperty(Name = "text")]
    
    public string? Text { get; set; }
    
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
        
        SchoolContactedDate = DateTime.Now;

        SchoolEmailAddressFound = true;

        Text = "text here";
        
        return Page(); 
    }

    public async Task<IActionResult> OnPost(int id,CancellationToken cancellationToken)
    {
        var schoolEmail = SchoolEmailAddressFound; 
        var schoolContacted = SchoolContactedDate;
        var text = Text;

        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(Request.Form.Keys, ModelState);
            ShowError = true;
            return await base.GetSupportProject(id, cancellationToken);
        }
        
        return Page();
    }

}