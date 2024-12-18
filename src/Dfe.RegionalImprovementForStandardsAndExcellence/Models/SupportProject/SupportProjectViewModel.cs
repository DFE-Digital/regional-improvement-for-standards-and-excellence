using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using System.Runtime.InteropServices;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.SupportProject
{
    public class SupportProjectViewModel
    {
        public int Id { get; set; }

        public string SchoolName { get; set; }

        public string SchoolUrn { get; set; }
        
        public string LocalAuthority { get; set; }

        public string Region { get; set; }

        public string AssignedAdviserFullName { get; set; }
        
        public string AssignedAdviserEmailAddress { get; set; }
        public static SupportProjectViewModel Create(SupportProjectDto supportProjectDto)
        {
            return new SupportProjectViewModel()
            {
                Id = supportProjectDto.id,
                AssignedAdviserFullName = supportProjectDto.assignedAdviserFullName,
                AssignedAdviserEmailAddress = supportProjectDto.assignedAdviserEmailAddress,
                LocalAuthority = supportProjectDto.localAuthority,
                Region = supportProjectDto.region,
                SchoolName = supportProjectDto.schoolName,
                SchoolUrn = supportProjectDto.schoolUrn
            };
        }
    }
}
