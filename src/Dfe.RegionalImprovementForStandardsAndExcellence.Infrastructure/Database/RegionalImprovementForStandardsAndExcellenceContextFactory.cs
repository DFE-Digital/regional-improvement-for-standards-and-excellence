using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Security;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Database
{
    public class RegionalImprovementForStandardsAndExcellenceContextFactory : IDesignTimeDbContextFactory<RegionalImprovementForStandardsAndExcellenceContext>
    {
        public RegionalImprovementForStandardsAndExcellenceContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Dfe.RegionalImprovementForStandardsAndExcellence");

            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var services = new ServiceCollection();

            var optionsBuilder = new DbContextOptionsBuilder<RegionalImprovementForStandardsAndExcellenceContext>();
            optionsBuilder.UseSqlServer(connectionString);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceCollectionExtensions).Assembly);
            });

            var serviceProvider = services.BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var userContext = serviceProvider.GetRequiredService<IUserContextService>();

            return new RegionalImprovementForStandardsAndExcellenceContext(optionsBuilder.Options, configuration, mediator, userContext);
        }
    }
}
