using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Jobs.Infrastructure.Persistence.EntityConfigurations.JobAggregate
{
    internal class JobLanguageEntityConfiguration : IEntityTypeConfiguration<JobLanguage>
    {
        public void Configure(EntityTypeBuilder<JobLanguage> builder)
        {
            builder.ToTable("JobLanguages", Constants.DefaultSchema);

            builder.HasKey(l => l.Id);

            builder.HasIndex(l => l.JobId);

            builder.Property(l => l.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(l => l.JobId)
                .IsRequired();

            builder.Property(l => l.CreatedAt)
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
