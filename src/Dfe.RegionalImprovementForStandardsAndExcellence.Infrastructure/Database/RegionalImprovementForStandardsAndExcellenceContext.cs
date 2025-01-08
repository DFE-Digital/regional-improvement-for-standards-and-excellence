using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Database.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Database;

public class RegionalImprovementForStandardsAndExcellenceContext : DbContext
{
    private readonly IConfiguration? _configuration;
    const string DefaultSchema = "RISE";
    private readonly IMediator _mediator = null!;

    public RegionalImprovementForStandardsAndExcellenceContext(DbContextOptions<RegionalImprovementForStandardsAndExcellenceContext> options, IConfiguration configuration, IMediator mediator)
    : base(options)
    {
        _configuration = configuration;
        _mediator = mediator;
    }
    public DbSet<SupportProject> SupportProjects { get; set; } = null!;
    
    public DbSet<SupportProjectNote> ProjectNotes { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration!.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        optionsBuilder.AddInterceptors(new DomainEventDispatcherInterceptor(_mediator));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupportProject>(ConfigureSupportProject);
        modelBuilder.Entity<SupportProjectNote>(ConfigureSupportProjectNotes);

        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigureSupportProject(EntityTypeBuilder<SupportProject> supportProjectConfiguration)
    {
        supportProjectConfiguration.HasKey(s => s.Id);
        supportProjectConfiguration.ToTable("SupportProject", DefaultSchema, b => b.IsTemporal());
        supportProjectConfiguration.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                v => v!.Value,
                v => new SupportProjectId(v));
        
        supportProjectConfiguration
            .HasMany(a => a.Notes)
            .WithOne()
            .HasForeignKey("SupportProjectId")
            .IsRequired();
    }
    
    private static void ConfigureSupportProjectNotes(EntityTypeBuilder<SupportProjectNote> supportProjectNoteConfiguration)
    {
        supportProjectNoteConfiguration.ToTable("SupportProjectNotes", DefaultSchema, b => b.IsTemporal());
        supportProjectNoteConfiguration.HasKey(a => a.Id);
        supportProjectNoteConfiguration.Property(e => e.Id)
            .HasConversion(
                v => v!.Value,
                v => new SupportProjectNoteId(v));
    }

}
