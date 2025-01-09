using System.Security.Claims;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.CreateSupportProjectNote;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.EditSupportProjectNote;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.Notes;


public class EditNoteModel(ISupportProjectQueryService supportProjectQueryService,ErrorService errorService, IMediator mediator) : BaseSupportProjectPageModel(supportProjectQueryService,errorService)
{
    public string ReturnPage { get; set; }
    
    [BindProperty(Name = "project-note-body")]
    public string ProjectNoteBody { get; set; }
    
    public Guid ProjectNoteId { get; set; }
    public bool ShowError => _errorService.HasErrors();
    
    public async Task<IActionResult> OnGetAsync(int id,Guid noteid, CancellationToken cancellationToken)
    {
        ProjectListFilters.ClearFiltersFrom(TempData);

        ReturnPage = @Links.Notes.Index.Page;
        
        await base.GetSupportProject(id, cancellationToken);

        var note = SupportProject.Notes.FirstOrDefault(a => a.Id.Value == noteid);

        ProjectNoteBody = note.Note;
        ProjectNoteId = note.Id.Value;

        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync(int id,DateTime projectNoteDate, Guid projectNoteId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(ProjectNoteBody))
        {
            _errorService.AddError("project-note-body", "Enter a note");

            await base.GetSupportProject(id, cancellationToken);
            return Page();
        }
        
        var supportProjectId = new SupportProjectId(id);
        
        var supportProjectNoteId = new SupportProjectNoteId(projectNoteId);

        var request = new EditSupportProjectNote.EditSupportProjectNoteCommand(supportProjectId,ProjectNoteBody,supportProjectNoteId);
        
        var result = await mediator.Send(request, cancellationToken);

        if (result == null)
        {
            _errorService.AddApiError();
            await base.GetSupportProject(id, cancellationToken);
            return Page();
        }
        
        TempData["editNote"] = true;
        return RedirectToPage(@Links.Notes.Index.Page, new { id });
    }
}