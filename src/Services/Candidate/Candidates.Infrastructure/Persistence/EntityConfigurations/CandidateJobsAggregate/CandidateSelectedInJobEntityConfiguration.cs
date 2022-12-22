using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Candidates.Infrastructure.Persistence.EntityConfigurations.CandidateJobsAggregate
{
    internal class CandidateSelectedInJobEntityConfiguration : IEntityTypeConfiguration<CandidateSelectedInJob>
    {
        public void Configure(EntityTypeBuilder<CandidateSelectedInJob> builder)
        {
            builder.ToTable("CandidateSelectedInJobs", Constants.DefaultSchema);

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.CandidateId);

            builder.HasIndex(x => new { x.CandidateId, x.JobId }).IsUnique();

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(x => x.CandidateId)
                .IsRequired();

            builder.Property(x => x.JobId)
                .IsRequired();

            builder.Property(x => x.JobStage)
                .HasConversion<string>()
                .HasMaxLength(EntityConfiguration.Clasificator)
                .IsRequired();

            builder.Property(x => x.Stage)
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

            builder.Property(x => x.CoverLetter)
                .HasMaxLength(EntityConfiguration.LongDescription);

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

            builder.OwnsOne(c => c.MotivationVideo, motivationVideo =>
            {
                motivationVideo.Property(c => c.Uri)
                    .HasMaxLength(EntityConfiguration.LinkUrl);

                motivationVideo.Property(c => c.FileName)
                    .HasMaxLength(EntityConfiguration.Title);
            });

            builder.Property(s => s.InvitedAt);

            builder.Property(s => s.HasApplied)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();
        }
    }
}
