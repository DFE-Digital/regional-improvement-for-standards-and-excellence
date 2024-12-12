using AutoMapper;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.Common.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;

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

        public async Task<Result<SupportProjectDto?>> GetSupportProject(int id, CancellationToken cancellationToken)
        {
            var supportProjectId = new SupportProjectId(id);
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == supportProjectId, cancellationToken);

            var result = mapper.Map<SupportProjectDto?>(supportProject);

            return result == null ? Result<SupportProjectDto?>.Failure("") : Result<SupportProjectDto?>.Success(result);
        }
        
    }
}
