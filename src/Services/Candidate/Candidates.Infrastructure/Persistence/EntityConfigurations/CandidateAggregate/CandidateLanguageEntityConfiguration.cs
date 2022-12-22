using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Candidates.Infrastructure.Persistence.EntityConfigurations.CandidateAggregate
{
    internal class CandidateLanguageEntityConfiguration : IEntityTypeConfiguration<CandidateLanguage>
    {
        public void Configure(EntityTypeBuilder<CandidateLanguage> builder)
        {
            builder.ToTable("CandidateLanguages", Constants.DefaultSchema);

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => s.CandidateId);

            builder.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(s => s.CandidateId)
                .IsRequired();

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            builder.OwnsOne(l => l.Language, language =>
            {
                language.Property(l => l.Id)
                    .IsRequired();

                language.Property(l => l.Code)
                    .HasMaxLength(EntityConfiguration.Clasificator)
                    .IsRequired();

                language.Property(l => l.Name)
                    .HasMaxLength(EntityConfiguration.Title)
                    .IsRequired();
            }).Navigation(j => j.Language).IsRequired();

        }
    }
}
