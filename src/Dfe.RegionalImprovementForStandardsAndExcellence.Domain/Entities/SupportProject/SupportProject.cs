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

    public SupportProjectId Id { get; set; }

    public string SchoolName { get; set; }

    public string SchoolUrn { get; set; }

    public string LocalAuthority { get; set; }
    public string Region { get; set; }

    public string? AssignedAdviserFullName { get; set; }
    
    public string? AssignedAdviserEmailAddress { get; set; }

    public static SupportProject Create(
        string schoolName,
        string schoolUrn,
        string localAuthority,
        string region,
        string assignedAdviserFullName,
        string assignedAdviserEmailAddress)
    {

        return new SupportProject() { SchoolName = schoolName, SchoolUrn = schoolUrn,LocalAuthority = localAuthority,Region = region, AssignedAdviserFullName = assignedAdviserFullName,AssignedAdviserEmailAddress = assignedAdviserEmailAddress};
    }

    public void SetAdviser(string assignedAdviserFullName, string assignedAdviserEmailAddress)
    {
        AssignedAdviserFullName = assignedAdviserFullName;
        AssignedAdviserEmailAddress = assignedAdviserEmailAddress;

    }
}