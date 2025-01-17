using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Common;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;

public class SupportProject : BaseAggregateRoot, IEntity<SupportProjectId>
{
    private SupportProject() { }
    public SupportProject(
        SupportProjectId id,
        string schoolName,
        string schoolUrn,
        string localAuthority,
        string region,
        string assignedAdviserFullName,
        string assignedAdviserEmailAddress)
    {
        Id = id;
        SchoolName = schoolName;
        SchoolUrn = schoolUrn;
        LocalAuthority = localAuthority;
        Region = region;
        AssignedAdviserFullName = assignedAdviserFullName;
        AssignedAdviserEmailAddress = assignedAdviserEmailAddress;
    }

    public SupportProjectId Id { get; private set; }

    public string SchoolName { get; private set; }

    public string SchoolUrn { get; private set; }

    public string Region { get; private set; }


    public DateTime CreatedOn { get; private set; }

    public string CreatedBy { get; private set; }

    public string LocalAuthority { get; private set; }
    
    public DateTime? LastModifiedOn { get; private set; }

    public string? LastModifiedBy { get; private set; }

    public string? AssignedAdviserFullName { get; private set; }
    
    public string? AssignedAdviserEmailAddress { get; private set; }
    
    public bool? FindSchoolEmailAddress { get; private set; }
    
    public bool? UseTheNotificationLetterToCreateEmail { get; private set; }
    
    public bool? AttachRiseInfoToEmail { get; private set; }
    
    public DateTime? ContactedTheSchoolDate { get; private set; }
    public IEnumerable<SupportProjectNote> Notes => _notes.AsReadOnly();

    public DateTime? SchoolResponseDate { get; set; }

    public bool? HasAcceeptedTargetedSupport { get; set; }

    public bool? HasSavedSchoolResponseinSharePoint { get; set; }

    private readonly List<SupportProjectNote> _notes = new();
    public static SupportProject Create(
        string schoolName,
        string schoolUrn,
        string localAuthority,
        string region,
        string createdBy,
        DateTime createdOn)
    {

        return new SupportProject() { 
            SchoolName = schoolName, 
            SchoolUrn = schoolUrn,
            LocalAuthority = localAuthority,
            Region = region, 
            CreatedBy = createdBy,
            CreatedOn = createdOn
        };
    }

    public void SetSchoolResponse(DateTime? schoolResponseDate,
        bool? hasAcceeptedTargetedSupport, bool? hasSavedSchoolResponseinSharePoint)
    {
        SchoolResponseDate = schoolResponseDate;
        HasAcceeptedTargetedSupport = hasAcceeptedTargetedSupport;
        HasSavedSchoolResponseinSharePoint = hasSavedSchoolResponseinSharePoint;
    }

    public void SetAdviser(string assignedAdviserFullName, string assignedAdviserEmailAddress)
    {
        AssignedAdviserFullName = assignedAdviserFullName;
        AssignedAdviserEmailAddress = assignedAdviserEmailAddress;
    }
    
    public void AddNote(SupportProjectNoteId id, string note, string author, DateTime date, SupportProjectId supportProjectId)
    {
        _notes.Add(new SupportProjectNote(id, note, author, date,supportProjectId));
    }

    public void EditSupportProjectNote(SupportProjectNoteId id, string note, string author, DateTime date)
    {
        var noteToUpdate = _notes.SingleOrDefault(x => x.Id == id);
        if (noteToUpdate != null)
        {
            noteToUpdate.SetNote(note, author, date);
        }
    }
    public void SetContactTheSchoolDetails(bool? findSchoolEmailAddress,bool? useTheNotificationLetterToCreateEmail,bool? attachRiseInfoToEmail,DateTime? schoolContactedDate)
    {
        FindSchoolEmailAddress = findSchoolEmailAddress;
        UseTheNotificationLetterToCreateEmail = useTheNotificationLetterToCreateEmail;
        AttachRiseInfoToEmail = attachRiseInfoToEmail;
        ContactedTheSchoolDate = schoolContactedDate;
    }
}