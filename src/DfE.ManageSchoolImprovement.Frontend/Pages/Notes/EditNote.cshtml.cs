using DfE.ManageSchoolImprovement.Application.SupportProject.Commands.EditSupportProjectNote;
using DfE.ManageSchoolImprovement.Application.SupportProject.Queries;
using DfE.ManageSchoolImprovement.Domain.ValueObjects;
using DfE.ManageSchoolImprovement.Frontend.Models;
using DfE.ManageSchoolImprovement.Frontend.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace DfE.ManageSchoolImprovement.Frontend.Pages.Notes;


public class EditNoteModel(ISupportProjectQueryService supportProjectQueryService, ErrorService errorService, IMediator mediator) : BaseSupportProjectPageModel(supportProjectQueryService, errorService)
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

        if (note != null)
        {
            ProjectNoteBody = note.Note;
            ProjectNoteId = note.Id.Value;
            
        }
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

        var request = new EditSupportProjectNote.EditSupportProjectNoteCommand(supportProjectId,ProjectNoteBody,supportProjectNoteId,User.Identity.Name);
        
        var result = await mediator.Send(request, cancellationToken);

        if (result != supportProjectNoteId)
        {
            _errorService.AddApiError();
            await base.GetSupportProject(id, cancellationToken);
            return Page();
        }
        
        TempData["editNote"] = true;
        return RedirectToPage(@Links.Notes.Index.Page, new { id });
    }
}
