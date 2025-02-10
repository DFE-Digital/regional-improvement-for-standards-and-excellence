using DfE.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using DfE.ManageSchoolImprovement.Infrastructure.Database;
using DfE.ManageSchoolImprovement.Infrastructure.Repositories;
using DfE.ManageSchoolImprovement.Infrastructure.Security;
using DfE.ManageSchoolImprovement.Infrastructure.Security.Authorization;
using DfE.ManageSchoolImprovement.Utils;
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
            //services.AddCustomAuthorization(config);

            // Utils
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUserContextService, UserContextService>();

            // Health check
            AddInfrastructureHealthCheck(services);

            return services;
        }

        public static void AddInfrastructureHealthCheck(this IServiceCollection services) {
            services.AddHealthChecks()
                .AddDbContextCheck<RegionalImprovementForStandardsAndExcellenceContext>("RISE Database");
        }
    }
}
