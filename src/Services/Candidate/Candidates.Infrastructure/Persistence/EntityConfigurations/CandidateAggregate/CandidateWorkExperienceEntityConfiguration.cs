using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Candidates.Infrastructure.Persistence.EntityConfigurations.CandidateAggregate
{
    internal class CandidateWorkExperienceEntityConfiguration : IEntityTypeConfiguration<CandidateWorkExperience>
    {
        public void Configure(EntityTypeBuilder<CandidateWorkExperience> builder)
        {
            builder.ToTable("CandidateWorkExperiences", Constants.DefaultSchema);

            builder.HasKey(c => c.Id);

            builder.HasIndex(f => f.CandidateId);

            builder.Property(c => c.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(f => f.CandidateId)
                .IsRequired();

            builder.Property(c => c.Type)
                .HasMaxLength(EntityConfiguration.Clasificator)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(c => c.CompanyName)
                .HasMaxLength(EntityConfiguration.Alias);

            builder.OwnsOne(job => job.Position, position =>
            {
                position.Property(p => p.Id)
                    .IsRequired();

                position.Property(p => p.Code)
                    .HasMaxLength(EntityConfiguration.Title)
                    .IsRequired();

                position.OwnsOne(p => p.AliasTo, aliasTo =>
                {
                    aliasTo.Property(a => a.Id)
                        .IsRequired();

                    aliasTo.Property(a => a.Code)
                        .HasMaxLength(EntityConfiguration.Clasificator)
                        .IsRequired();
                });
            }).Navigation(j => j.Position).IsRequired();

            builder.OwnsOne(c => c.Period, range =>
            {
                range.Property(a => a.From)
                    .IsRequired();

                range.Property(a => a.To);
            });

            builder.Property(c => c.JobDescription)
                .HasMaxLength(EntityConfiguration.Title);

            builder.Property(c => c.IsCurrentJob)
                .IsRequired();

            builder.HasMany(c => c.Skills)
                .WithOne()
                .HasForeignKey(f => f.CandidateWorkExperienceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(f => f.CreatedAt)
                .IsRequired();
        }
    }
}
