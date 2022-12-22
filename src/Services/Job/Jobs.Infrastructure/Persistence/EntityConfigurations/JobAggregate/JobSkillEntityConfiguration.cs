using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Jobs.Infrastructure.Persistence.EntityConfigurations.JobAggregate
{
    internal class JobSkillEntityConfiguration : IEntityTypeConfiguration<JobSkill>
    {
        public void Configure(EntityTypeBuilder<JobSkill> builder)
        {
            builder.ToTable("JobSkills", Constants.DefaultSchema);

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => new { s.JobId, s.SkillId }).IsUnique();

            builder.HasIndex(s => s.JobId);

            builder.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(s => s.SkillId)
                .IsRequired();

            builder.Property(s => s.JobId)
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
