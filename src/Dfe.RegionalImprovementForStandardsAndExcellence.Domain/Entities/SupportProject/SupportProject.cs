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
        string assignedUser)
    {
        Id = id;
        SchoolName = schoolName;
        SchoolUrn = schoolUrn;
        LocalAuthority = localAuthority;
        Region = region;
        AssignedUser = assignedUser;
    }

    public SupportProjectId Id { get; private set; }

    public string SchoolName { get; private set; }

    public string SchoolUrn { get; private set; }

    public string Region { get; private set; }

    public string AssignedUser { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public string CreatedBy { get; private set; }

    public string LocalAuthority { get; private set; }
    public DateTime? LastModifiedOn { get; private set; }

    public string? LastModifiedBy { get; private set; }

    public static SupportProject Create(
        string schoolName,
        string schoolUrn,
        string localAuthority,
        string region,
        string assignedUser)
    {

        return new SupportProject() { SchoolName = schoolName, SchoolUrn = schoolUrn,LocalAuthority = localAuthority,Region = region, AssignedUser = assignedUser};
    }
}