using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Jobs.Infrastructure.Persistence.EntityConfigurations.JobAggregate
{
    internal class JobInterestedCandidateEntityConfiguration : IEntityTypeConfiguration<JobInterestedCandidate>
    {
        public void Configure(EntityTypeBuilder<JobInterestedCandidate> builder)
        {
            builder.ToTable("JobInterestedCandidates", Constants.DefaultSchema);

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => new { s.JobId, s.CandidateId }).IsUnique();

            builder.HasIndex(s => s.JobId);

            builder.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(s => s.CandidateId)
                .IsRequired();

            builder.Property(s => s.JobId)
                .IsRequired();

            builder.Property(s => s.FirstName)
                .HasMaxLength(EntityConfiguration.Alias)
                .IsRequired();

            builder.Property(s => s.LastName)
                .HasMaxLength(EntityConfiguration.Alias)
                .IsRequired();

            builder.Property(s => s.Position)
                .HasMaxLength(EntityConfiguration.Title);

            builder.Property(s => s.PictureUri)
                .HasMaxLength(EntityConfiguration.LinkUrl);

            builder.Property(s => s.CreatedAt)
                .IsRequired();
        }
    }
}
