using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Jobs.Infrastructure.Persistence.EntityConfigurations.JobCandidateAggregate
{
    internal class JobArchivedCandidatesEntityConfiguration : IEntityTypeConfiguration<JobArchivedCandidate>
    {
        public void Configure(EntityTypeBuilder<JobArchivedCandidate> builder)
        {
            builder.ToTable("ArchivedCandidate", Constants.DefaultSchema);

            builder.HasKey(r => r.Id);

            builder.HasIndex(r => r.JobId);

            builder.Property(r => r.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(r => r.JobId)
                .IsRequired();

            builder.Property(r => r.CandidateId)
                .IsRequired();

            builder.Property(e => e.FirstName)
                .HasMaxLength(EntityConfiguration.Alias)
                .IsRequired();

            builder.Property(e => e.LastName)
                .HasMaxLength(EntityConfiguration.Alias)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasMaxLength(EntityConfiguration.Email);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(EntityConfiguration.PhoneNumber);

            builder.Property(e => e.PictureUri)
                .HasMaxLength(EntityConfiguration.LinkUrl);

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
                        .HasMaxLength(EntityConfiguration.Title)
                        .IsRequired();
                });
            });

            builder.Property(r => r.Stage)
                .HasConversion<string>()
                .HasMaxLength(EntityConfiguration.Clasificator)
                .IsRequired();

            builder.Property(r => r.Brief)
                .HasMaxLength(EntityConfiguration.LongDescription);

            builder.Property(r => r.InvitedAt);

            builder.Property(r => r.HasApplied)
                .IsRequired();

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.Property(r => r.SystemLanguage)
                .HasConversion<string>()
                .HasMaxLength(EntityConfiguration.Clasificator);
        }
    }
}
