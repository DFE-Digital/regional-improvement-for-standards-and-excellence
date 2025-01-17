using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Models
{
    public record SupportProjectDto(int id,
        string schoolName,
        string schoolUrn,
        string localAuthority,
        string region,
        string assignedAdviserFullName,
        string assignedAdviserEmailAddress,
        bool FindSchoolEmailAddress,
        bool UseTheNotificationLetterToCreateEmail,
        bool AttachRiseInfoToEmail, 
        DateTime? ContactedTheSchoolDate,
        DateTime? SchoolResponseDate,
        bool? HasAcceeptedTargetedSupport,
        bool? HasSavedSchoolResponseinSharePoint,
        IEnumerable<SupportProjectNote> notes
        );

}
