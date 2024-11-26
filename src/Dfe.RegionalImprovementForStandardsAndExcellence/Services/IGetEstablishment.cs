using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;

public interface IGetEstablishment
{
    Task<DfE.CoreLibs.Contracts.Academies.V4.Establishments.EstablishmentDto> GetEstablishmentByUrn(string urn);
    Task<IEnumerable<EstablishmentSearchResponse>> SearchEstablishments(string searchQuery);
}
