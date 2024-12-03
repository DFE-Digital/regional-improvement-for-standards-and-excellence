﻿using AutoMapper;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.Common.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries
{
    public class SupportProjectQueryService(ISupportProjectRepository supportProjectRepository, IMapper mapper) : ISupportProjectQueryService
    {
        public async Task<Result<IEnumerable<SupportProjectDto>>> GetAllSupportProjects(CancellationToken cancellationToken)
        {
            var supportProjects = await supportProjectRepository.FetchAsync(sp => sp.Id.Value > 0, cancellationToken);

            var result = supportProjects.Select(x => mapper.Map<SupportProjectDto?>(x));

            return Result<IEnumerable<SupportProjectDto>>.Success(result);
        }

        public async Task<Result<SupportProjectDto?>> GetSupportProject(string id, CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.GetAsync(id, cancellationToken);

            var result = mapper.Map<SupportProjectDto?>(supportProject);

            return result == null ? Result<SupportProjectDto?>.Failure("") : Result<SupportProjectDto?>.Success(result);
        }
    }
}