﻿using Dfe.RegionalImprovementForStandardsAndExcellence.Data.Models.Establishment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Data.Services;

public interface IGetEstablishment
{
   Task<Academies.Contracts.V4.Establishments.EstablishmentDto> GetEstablishmentByUrn(string urn);
   Task<IEnumerable<EstablishmentSearchResponse>> SearchEstablishments(string searchQuery);
}
