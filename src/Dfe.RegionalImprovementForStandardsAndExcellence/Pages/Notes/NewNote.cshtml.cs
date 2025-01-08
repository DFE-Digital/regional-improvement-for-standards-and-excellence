using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.CreateSupportProjectNote;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.Notes;


public class NewNoteModel(ISupportProjectQueryService supportProjectQueryService, IGetEstablishment getEstablishment,ErrorService errorService, IMediator mediator) : BaseSupportProjectPageModel(supportProjectQueryService, getEstablishment,errorService)
{
    public string ReturnPage { get; set; }
    
    [BindProperty(Name = "project-note-body")]
    public string ProjectNoteBody { get; set; }
    
    public bool ShowError => _errorService.HasErrors();
    
    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        ProjectListFilters.ClearFiltersFrom(TempData);

        ReturnPage = @Links.Notes.Index.Page;
        
        await base.GetSupportProject(id, cancellationToken);

        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(ProjectNoteBody))
        {
            _errorService.AddError("project-note-body", "Enter a note");

            await base.GetSupportProject(id, cancellationToken);
            return Page();
        }

        var supportProjectId = new SupportProjectId(id);

        var request = new CreateSupportProjectNote.CreateSupportProjectNoteCommand(supportProjectId,ProjectNoteBody,"test user");

        var result = await mediator.Send(request, cancellationToken);

        if (result == null)
        {
            _errorService.AddApiError();
            await base.GetSupportProject(id, cancellationToken);
            return Page();
        }
        

        return RedirectToPage(@Links.Notes.Index.Page, new { id });
    }
}