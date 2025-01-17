using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Common;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Database.Interceptors;
using Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Principal;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Database;

public class RegionalImprovementForStandardsAndExcellenceContext : DbContext
{
    private readonly IConfiguration? _configuration;
    const string DefaultSchema = "RISE";
    private readonly IMediator _mediator = null!;
    private readonly IUserContextService _userContextService;

    public RegionalImprovementForStandardsAndExcellenceContext(DbContextOptions<RegionalImprovementForStandardsAndExcellenceContext> options, IConfiguration configuration, IMediator mediator, IUserContextService userContextService)
    : base(options)
    {
        _configuration = configuration;
        _mediator = mediator;
        _userContextService = userContextService;
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

    public override int SaveChanges()
    {
        SetAuditFields();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditFields()
    {
        var currentUsername = _userContextService.GetCurrentUsername();

        // for new domain object mapped directly to the database
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is IAuditableEntity &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (IAuditableEntity)entry.Entity;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.LastModifiedBy = currentUsername;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedOn = DateTime.UtcNow;
                entity.CreatedBy = currentUsername;
            }
        }

    }
}
