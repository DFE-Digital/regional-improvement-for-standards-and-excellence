using Dfe.ManageSchoolImprovement.Frontend.Models;

namespace Dfe.ManageSchoolImprovement.Frontend.Services;

public interface IGetEstablishment
{
    Task<DfE.CoreLibs.Contracts.Academies.V4.Establishments.EstablishmentDto> GetEstablishmentByUrn(string urn);
    Task<IEnumerable<EstablishmentSearchResponse>> SearchEstablishments(string searchQuery);
}
