using Dfe.RegionalImprovementForStandardsAndExcellence.Application.Common.Behaviours;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.MappingProfiles;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationDependencyGroup(
            this IServiceCollection services, IConfiguration config)
        {
            var performanceLoggingEnabled = config.GetValue<bool>("Features:PerformanceLoggingEnabled");

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

                if (performanceLoggingEnabled)
                {
                    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
                }
            });

            services.AddAutoMapper(typeof(RiseProfile));

            services.AddBackgroundService();

            services.AddScoped<ISupportProjectQueryService, SupportProjectQueryService>();

            return services;
        }
    }
}
