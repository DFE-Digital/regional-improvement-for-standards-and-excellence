//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Database
//{
//    public class GenericDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
//    {
//        public TContext CreateDbContext(string[] args)
//        {
//            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Dfe.RegionalImprovementForStandardsAndExcellence.Frontend");

//            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

//            IConfigurationRoot configuration = new ConfigurationBuilder()
//                .SetBasePath(basePath)
//                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
//                .AddEnvironmentVariables()
//                .Build();

//            var connectionString = configuration.GetConnectionString("DefaultConnection");

//            var services = new ServiceCollection();

//            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
//            optionsBuilder.UseSqlServer(connectionString);

//            //services.AddMediatR(cfg =>
//            //{
//            //    cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceCollectionExtensions).Assembly);
//            //});
//            var serviceProvider = services.BuildServiceProvider();

//            var mediator = serviceProvider.GetRequiredService<IMediator>();



//            return new RegionalImprovementForStandardsAndExcellenceContext(optionsBuilder.Options);

//        }
//    }
//}
