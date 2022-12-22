using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Jobs.Infrastructure.Persistence.EntityConfigurations.JobAggregate
{
    internal class JobInterestedLinkedInEntityConfiguration : IEntityTypeConfiguration<JobInterestedLinkedIn>
    {
        public void Configure(EntityTypeBuilder<JobInterestedLinkedIn> builder)
        {
            builder.ToTable("JobInterestedLinkedIns", Constants.DefaultSchema);

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => new { s.JobId, s.Url }).IsUnique();

            builder.HasIndex(s => s.JobId);

            builder.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(s => s.JobId)
                .IsRequired();

            builder.Property(s => s.Url)
                .HasMaxLength(EntityConfiguration.LinkUrl)
                .IsRequired();

            builder.Property(s => s.CreatedAt)
                .IsRequired();
        }
    }
}
