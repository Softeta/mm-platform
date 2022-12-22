using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Candidates.Infrastructure.Persistence.EntityConfigurations.CandidateJobsAggregate
{
    internal class CandidateAppliedInJobEntityConfiguration : IEntityTypeConfiguration<CandidateAppliedInJob>
    {
        public void Configure(EntityTypeBuilder<CandidateAppliedInJob> builder)
        {
            builder.ToTable("CandidateAppliedInJobs", Constants.DefaultSchema);

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => s.CandidateId);

            builder.HasIndex(x => new { x.CandidateId, x.JobId }).IsUnique();

            builder.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(s => s.CandidateId)
                .IsRequired();

            builder.Property(s => s.JobId)
                .IsRequired();

            builder.Property(x => x.JobStage)
                .HasConversion<string>()
                .HasMaxLength(EntityConfiguration.Clasificator)
                .IsRequired();

            builder.Property(x => x.Freelance)
                .HasConversion<string>()
                .HasMaxLength(EntityConfiguration.Clasificator);

            builder.Property(x => x.Permanent)
                .HasConversion<string>()
                .HasMaxLength(EntityConfiguration.Clasificator);

            builder.Property(x => x.StartDate);

            builder.Property(x => x.DeadlineDate);

            builder.OwnsOne(x => x.Position, position =>
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

            builder.OwnsOne(c => c.Company, company =>
            {
                company.Property(a => a.Id)
                    .IsRequired();

                company.Property(a => a.Name)
                    .HasMaxLength(EntityConfiguration.Title)
                    .IsRequired();

                company.Property(a => a.LogoUri)
                    .HasMaxLength(EntityConfiguration.LinkUrl);
            });

            builder.Property(s => s.CreatedAt)
                .IsRequired();
        }
    }
}
