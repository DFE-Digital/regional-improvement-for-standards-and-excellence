using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Models
{
    public record SupportProjectDto(SupportProjectId id,
        string schoolName,
        string schoolUrn,
        string region,
        string assignedUser);

}
