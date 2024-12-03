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
        string region,
        string assignedUser)
    {
        Id = id;
        SchoolName = schoolName;
        SchoolUrn = schoolUrn;
        Region = region;
        AssignedUser = assignedUser;
    }

    public SupportProjectId Id { get; set; }

    public string SchoolName { get; set; }

    public string SchoolUrn { get; set; }

    public string Region { get; set; }

    public string AssignedUser { get; set; }

    public static SupportProject Create(
        string schoolName,
        string schoolUrn,
        string region,
        string assignedUser)
    {

        return new SupportProject() { SchoolName = schoolName, SchoolUrn = schoolUrn, Region = region, AssignedUser = assignedUser};
    }
}