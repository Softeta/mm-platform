using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Candidates.Infrastructure.Persistence.EntityConfigurations.CandidateAggregate
{
    internal class CandidateCourseEntityConfiguration : IEntityTypeConfiguration<CandidateCourse>
    {
        public void Configure(EntityTypeBuilder<CandidateCourse> builder)
        {
            builder.ToTable("CandidateCourses", Constants.DefaultSchema);

            builder.HasKey(c => c.Id);

            builder.HasIndex(f => f.CandidateId);

            builder.Property(c => c.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(f => f.CandidateId)
                .IsRequired();

            builder.Property(c => c.Name)
                .HasMaxLength(EntityConfiguration.Alias);

            builder.Property(c => c.IssuingOrganization)
                .HasMaxLength(EntityConfiguration.Alias);

            builder.Property(c => c.Description)
                .HasMaxLength(EntityConfiguration.Title);

            builder.OwnsOne(c => c.Certificate, certificate =>
            {
                certificate.Property(a => a.Uri)
                    .HasMaxLength(EntityConfiguration.LinkUrl);

                certificate.Property(a => a.FileName)
                    .HasMaxLength(EntityConfiguration.Title);
            });

            builder.Property(f => f.CreatedAt)
                .IsRequired();
        }
    }
}
