using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Candidates.Infrastructure.Persistence.EntityConfigurations.CandidateAggregate
{
    internal class CandidateEducationEntityConfiguration : IEntityTypeConfiguration<CandidateEducation>
    {
        public void Configure(EntityTypeBuilder<CandidateEducation> builder)
        {
            builder.ToTable("CandidateEducations", Constants.DefaultSchema);

            builder.HasKey(c => c.Id);

            builder.HasIndex(f => f.CandidateId);

            builder.Property(c => c.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(f => f.CandidateId)
                .IsRequired();

            builder.Property(c => c.SchoolName)
                .HasMaxLength(EntityConfiguration.Alias);

            builder.Property(c => c.Degree)
                .HasMaxLength(EntityConfiguration.Title);

            builder.Property(c => c.FieldOfStudy)
                .HasMaxLength(EntityConfiguration.Title);

            builder.OwnsOne(c => c.Period, range =>
            {
                range.Property(a => a.From)
                    .IsRequired();

                range.Property(a => a.To);
            });

            builder.Property(c => c.StudiesDescription)
                .HasMaxLength(EntityConfiguration.Title);

            builder.Property(c => c.IsStillStudying)
                .IsRequired();

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
