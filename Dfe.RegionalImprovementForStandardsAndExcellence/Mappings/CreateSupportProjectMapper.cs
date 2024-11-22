using Dfe.Academies.Contracts.V4.Establishments;
using Dfe.Academies.Contracts.V4.Trusts;
using Dfe.RegionalImprovementForStandardsAndExcellence.Data.Models;
using System;
using Dfe.RegionalImprovementForStandardsAndExcellence.Models;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Mappings;

public static class CreateSupportProjectMapper
{
   public static CreateNewSupportProject MapToDto(EstablishmentDto establishment)
   {
      CreateNewSupportProject NewSupportProject = new(
         establishment.Name,
         establishment.Urn
         );
      return NewSupportProject;
   }
}
