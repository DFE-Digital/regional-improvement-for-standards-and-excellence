using Dfe.RegionalImprovementForStandardsAndExcellence.Application.Common.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Models;
using DfE.CoreLibs.Contracts.Academies.V4;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries
{
    public interface ISupportProjectQueryService
    {
        Task<Result<IEnumerable<SupportProjectDto>>> GetAllSupportProjects(CancellationToken cancellationToken);
        Task<Result<SupportProjectDto?>> GetSupportProject(int id, CancellationToken cancellationToken);
        Task<Result<PagedDataResponse<SupportProjectDto>?>> SearchForSupportProjects(
            string? title,
            IEnumerable<string>? states,
            IEnumerable<string>? advisors,
            IEnumerable<string>? regions,
            IEnumerable<string>? localAuthorities,
            string pagePath,
            int page,
            int count,
            CancellationToken cancellationToken);

        Task<Result<IEnumerable<string>>> GetAllProjectLocalAuthorities(CancellationToken cancellationToken);
        Task<Result<IEnumerable<string>>> GetAllProjectRegions(CancellationToken cancellationToken);
    }
}
