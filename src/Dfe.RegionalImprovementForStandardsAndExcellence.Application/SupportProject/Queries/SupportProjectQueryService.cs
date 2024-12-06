using AutoMapper;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.Common.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using DfE.CoreLibs.Contracts.Academies.V4;
using System.Threading;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries
{
    public class SupportProjectQueryService(ISupportProjectRepository supportProjectRepository, IMapper mapper) : ISupportProjectQueryService
    {
        public async Task<Result<IEnumerable<SupportProjectDto>>> GetAllSupportProjects(CancellationToken cancellationToken)
        {
            var supportProjects = await supportProjectRepository.FetchAsync(sp => true, cancellationToken);

            var result = supportProjects.Select(x => mapper.Map<SupportProjectDto>(x)).ToList();

            return Result<IEnumerable<SupportProjectDto>>.Success(result);
        }

        public async Task<Result<PagedDataResponse<SupportProjectDto>?>> SearchForSupportProjects(
            string? title, 
            IEnumerable<string>? states,       
            IEnumerable<string>? advisors,      
            IEnumerable<string>? regions, 
            IEnumerable<string>? localAuthorities,
            int page, 
            int count, 
            CancellationToken cancellationToken)
        {
            var (projects, totalCount) = await supportProjectRepository.SearchForSupportProjects(title, states, advisors, regions, localAuthorities, page, count, cancellationToken);

            var pageResponse = PagingResponseFactory.Create("transfer-projects/projects", page, count, totalCount,
                new Dictionary<string, object?> {
                {"states", states},
                });

            return new PagedDataResponse<AcademyTransferProjectSummaryResponse>(data,
                pageResponse);

            return Result<IEnumerable<SupportProjectDto>>.Success(result);
        }

        public async Task<Result<SupportProjectDto?>> GetSupportProject(int id, CancellationToken cancellationToken)
        {
            var supportProjectId = new SupportProjectId(id);
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == supportProjectId, cancellationToken);

            var result = mapper.Map<SupportProjectDto?>(supportProject);

            return result == null ? Result<SupportProjectDto?>.Failure("") : Result<SupportProjectDto?>.Success(result);
        }
    }
}
