using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;
public class ClinicalTrialConfiguration : IEntityTypeConfiguration<ClinicalTrial>
{
    public void Configure(EntityTypeBuilder<ClinicalTrial> builder)
    {
        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.TrialId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ct => ct.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(ct => ct.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ct => ct.DurationInDays)
            .IsRequired()
            .HasDefaultValue(0);
    }
}
