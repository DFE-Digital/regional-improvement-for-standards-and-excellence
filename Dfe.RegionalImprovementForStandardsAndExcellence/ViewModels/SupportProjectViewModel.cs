using Dfe.RegionalImprovementForStandardsAndExcellence.Data.Models;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.ViewModels;

public class SupportProjectViewModel
{
   public int Id { get; set; }
    
   public string SchoolName { get; set; }
    
   public string SchoolUrn { get; set; }
    
   public string Region { get; set; }

   public string AssignedUser { get; set; }
   public static SupportProjectViewModel Build(SupportProject project)
   {
      return new SupportProjectViewModel
      {
            Id = project.Id,
            SchoolName = project.SchoolName,
            SchoolUrn = project.SchoolUrn,
            Region = project.Region,
            AssignedUser = project.AssignedUser
      };
   }
}
