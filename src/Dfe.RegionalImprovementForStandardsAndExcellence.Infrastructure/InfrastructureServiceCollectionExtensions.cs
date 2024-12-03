using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Database;
using Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Security.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureDependencyGroup(
            this IServiceCollection services, IConfiguration config)
        {
            //Repos
            services.AddScoped<ISupportProjectRepository, SupportProjectRepository>();

            //Cache service
            services.AddServiceCaching(config);

            //Db
            var connectionString = config.GetConnectionString("DefaultConnection");

            services.AddDbContext<RegionalImprovementForStandardsAndExcellenceContext>(options =>
                options.UseSqlServer(connectionString));

            // Authentication
            services.AddCustomAuthorization(config);

            return services;
        }
    }
}