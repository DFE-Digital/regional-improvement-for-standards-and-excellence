using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Common;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;

public class SupportProjectNote : BaseAggregateRoot, IEntity<SupportProjectNoteId>
{
    private SupportProjectNote() { }
    
    public SupportProjectNote(SupportProjectNoteId id,
        string? note,
        string? author,
        DateTime date, SupportProjectId supportProjectId)
    {
        Id = id;
        Note = note;
        CreatedBy = author;
        CreatedOn = date;
        SupportProjectId = supportProjectId;
    }
    
    public SupportProjectId SupportProjectId { get; private set; }
    public SupportProjectNoteId Id { get; private set; }
    public string? Note { get; set; }
    
    public DateTime CreatedOn { get; private set; }
    public string CreatedBy { get; private set; }
    
    public DateTime? LastModifiedOn { get; private set; }

    public string? LastModifiedBy { get; private set; }
    
}