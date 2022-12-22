using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Jobs.Infrastructure.Persistence.EntityConfigurations;

public class JobCandidatesFiltersEntityConfiguration : IEntityTypeConfiguration<JobCandidatesFilter>
{
    public void Configure(EntityTypeBuilder<JobCandidatesFilter> builder)
    {
        builder.ToTable("CandidatesFilter", Constants.DefaultSchema);

        builder.HasKey(r => new { r.UserId, r.JobId, r.Index });

        builder.Property(r => r.Index)
            .IsRequired();

        builder.Property(r => r.UserId)
            .IsRequired();

        builder.Property(r => r.JobId)
            .IsRequired();

        builder.Property(r => r.Title)
            .HasMaxLength(EntityConfiguration.Title)
            .IsRequired();

        builder.Property(r => r.FilterParams)
            .HasColumnType(EntityConfiguration.NVarcharMax)
            .IsRequired();
    }
}