using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Candidates.Infrastructure.Persistence.EntityConfigurations.CandidateAggregate
{
    internal class CandidateActivityStatusEntityConfiguration : IEntityTypeConfiguration<CandidateActivityStatus>
    {
        public void Configure(EntityTypeBuilder<CandidateActivityStatus> builder)
        {
            builder.ToTable("CandidateActivityStatuses", Constants.DefaultSchema);

            builder.HasKey(c => c.Id);

            builder.HasIndex(f => f.CandidateId);

            builder.Property(c => c.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(f => f.CandidateId)
                .IsRequired();

            builder.Property(c => c.ActivityStatus)
                .HasMaxLength(EntityConfiguration.Clasificator)
                .HasConversion<string>();

            builder.Property(f => f.CreatedAt)
                .IsRequired();
        }
    }
}
