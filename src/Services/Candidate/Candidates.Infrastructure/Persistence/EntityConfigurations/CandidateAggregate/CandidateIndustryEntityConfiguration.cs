using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Candidates.Infrastructure.Persistence.EntityConfigurations.CandidateAggregate
{
    internal class CandidateIndustryEntityConfiguration : IEntityTypeConfiguration<CandidateIndustry>
    {
        public void Configure(EntityTypeBuilder<CandidateIndustry> builder)
        {
            builder.ToTable("CandidateIndustries", Constants.DefaultSchema);

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => new { s.CandidateId, s.IndustryId }).IsUnique();

            builder.HasIndex(s => s.CandidateId);

            builder.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(s => s.IndustryId)
                .IsRequired();

            builder.Property(s => s.CandidateId)
                .IsRequired();

            builder.Property(s => s.Code)
                .HasMaxLength(EntityConfiguration.Clasificator)
                .IsRequired();

            builder.Property(s => s.CreatedAt)
                .IsRequired();
        }
    }
}
