using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Models
{
    public record SupportProjectDto(int id,
        DateTime createdOn,
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
        bool? SendConflictOfInterestFormToProposedAdviserAndTheSchool,
        bool? RecieveCompletedConflictOfInteresetForm,
        bool? SaveCompletedConflictOfinterestFormInSharePoint,
        DateTime? DateConflictsOfInterestWereChecked,
        IEnumerable<SupportProjectNote> notes);

}
