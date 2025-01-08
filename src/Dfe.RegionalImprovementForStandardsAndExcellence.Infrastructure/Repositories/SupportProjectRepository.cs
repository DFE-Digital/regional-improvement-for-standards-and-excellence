using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    internal class SupportProjectRepository(RegionalImprovementForStandardsAndExcellenceContext dbContext)
        : Repository<SupportProject, RegionalImprovementForStandardsAndExcellenceContext>(dbContext), ISupportProjectRepository
    {
        public async Task<(IEnumerable<SupportProject> projects, int totalCount)> SearchForSupportProjects(
            string? title,
            IEnumerable<string>? states,
            IEnumerable<string>? advisors,
            IEnumerable<string>? regions,
            IEnumerable<string>? localAuthorities,
            int page,
            int count,
            CancellationToken cancellationToken)
        {
            IQueryable<SupportProject> queryable = DbSet();

            queryable = FilterByRegion(regions, queryable);
            queryable = FilterByStatus(states, queryable);
            queryable = FilterByKeyword(title, queryable);
            //queryable = FilterByAdvisors(advisors, queryable);
            queryable = FilterByLocalAuthority(localAuthorities, queryable);

            var totalProjects = queryable.Count();
            var projects = await queryable
                .OrderByDescending(acp => acp.CreatedOn)
                .Skip((page - 1) * count)
                .Take(count).ToListAsync();

            return (projects, totalProjects);
        }

        private static IQueryable<SupportProject> FilterByRegion(IEnumerable<string>? regions, IQueryable<SupportProject> queryable)
        {

            if (regions != null && regions.Any())
            {
                var lowerCaseRegions = regions.Select(region => region.ToLower());
                queryable = queryable.Where(p =>
                    !string.IsNullOrEmpty(p.Region) && lowerCaseRegions.Contains(p.Region.ToLower()));
            }

            return queryable;
        }

        private static IQueryable<SupportProject> FilterByStatus(IEnumerable<string>? states, IQueryable<SupportProject> queryable)
        {
            //if (states != null && states!.Any())
            //{
            //    queryable = queryable.Where(p => states.Contains(p.ProjectStatus!.ToLower()));
            //}

            return queryable;
        }

        private static IQueryable<SupportProject> FilterByKeyword(string? title, IQueryable<SupportProject> queryable)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {

                queryable = queryable.Where(p => p.SchoolName!.ToLower().Contains(title!.ToLower()) ||
                p.SchoolUrn.ToString().ToLower().Contains(title!.ToLower())
                );
            }

            return queryable;
        }

        private static IQueryable<SupportProject> FilterByLocalAuthority(IEnumerable<string>? localAuthorities, IQueryable<SupportProject> queryable)
        {
            if (localAuthorities != null && localAuthorities.Any())
            {
                var lowerCaseRegions = localAuthorities.Select(la => la.ToLower());
                queryable = queryable.Where(p =>
                    !string.IsNullOrEmpty(p.LocalAuthority) && lowerCaseRegions.Contains(p.LocalAuthority.ToLower()));
            }

            return queryable;
        }

        public async Task<IEnumerable<string>> GetAllProjectRegions(CancellationToken cancellationToken)
        {
            return await DbSet().OrderByDescending(p => p.Region)
                    .AsNoTracking()
                    .Select(p => p.Region)
                    .Where(p => !string.IsNullOrEmpty(p))
                    .Distinct()
                    .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllProjectLocalAuthorities(CancellationToken cancellationToken)
        {
            return await DbSet().OrderByDescending(p => p.LocalAuthority)
                    .AsNoTracking()
                    .Select(p => p.LocalAuthority)
                    .Where(p => !string.IsNullOrEmpty(p))
                    .Distinct()
                    .ToListAsync();

        }
        
        public async Task<SupportProject> GetSuportProjectById(SupportProjectId id, CancellationToken cancellationToken)
        {
            return await DefaultIncludes().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        
        private IQueryable<SupportProject> DefaultIncludes() 
        {
            return DbSet().Include(x => x.Notes).AsQueryable();
        }
    }
}
