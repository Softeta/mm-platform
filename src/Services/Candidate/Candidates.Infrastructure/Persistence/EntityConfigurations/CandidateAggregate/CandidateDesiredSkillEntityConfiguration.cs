using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Candidates.Infrastructure.Persistence.EntityConfigurations.CandidateAggregate
{
    internal class CandidateDesiredSkillEntityConfiguration : IEntityTypeConfiguration<CandidateDesiredSkill>
    {
        public void Configure(EntityTypeBuilder<CandidateDesiredSkill> builder)
        {
            builder.ToTable("CandidateDesiredSkills", Constants.DefaultSchema);

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => new { s.CandidateId, s.SkillId }).IsUnique();

            builder.HasIndex(s => s.CandidateId);

            builder.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(s => s.SkillId)
                .IsRequired();

            builder.Property(s => s.CandidateId)
                .IsRequired();

            builder.Property(s => s.Code)
                .HasMaxLength(EntityConfiguration.Clasificator)
                .IsRequired();

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            builder.OwnsOne(s => s.AliasTo, aliasTo =>
            {
                aliasTo.Property(a => a.Id)
                .IsRequired();

                aliasTo.Property(a => a.Code)
                .HasMaxLength(EntityConfiguration.Clasificator)
                .IsRequired();
            });
        }
    }
}
