using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Contexts;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ClinicalTrial> ClinicalTrials { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        modelBuilder.Entity<ClinicalTrial>()
            .Property(ct => ct.Status)
            .HasConversion(
                v => v.ToString(), // Convert enum to string for database
                v => (TrialStatus)Enum.Parse(typeof(TrialStatus), v));
    }
}
