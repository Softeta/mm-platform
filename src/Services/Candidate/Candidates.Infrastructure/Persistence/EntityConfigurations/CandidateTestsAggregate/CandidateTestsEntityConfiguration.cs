using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Candidates.Infrastructure.Persistence.EntityConfigurations.CandidateJobsAggregate;

internal class CandidateTestsEntityConfiguration : IEntityTypeConfiguration<CandidateTests>
{
    public void Configure(EntityTypeBuilder<CandidateTests> builder)
    {
        builder.ToTable("CandidateTests", Constants.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.ExternalId);
        builder.HasIndex(c => c.CandidateOldPlatformId);

        builder.Property(c => c.Id)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(c => c.ExternalId)
            .IsRequired();

        builder.Property(c => c.CandidateOldPlatformId);

        builder.OwnsOne(tests => tests.LogicalAssessment, logical =>
        {
            logical.Property(c => c.PackageInstanceId)
                .HasMaxLength(EntityConfiguration.Title)
                .IsRequired();

            logical.Property(c => c.PackageTypeId)
                .HasMaxLength(EntityConfiguration.Title)
                .IsRequired();

            logical.Property(c => c.Status)
                .HasConversion<string>()
                .HasMaxLength(EntityConfiguration.Clasificator);

            logical.Property(c => c.Url)
                .HasMaxLength(EntityConfiguration.LinkUrl)
                .IsRequired();

            logical.Property(c => c.StartedAt);
            logical.Property(c => c.CompletedAt);

            logical.OwnsOne(d => d.Scores, score =>
            {
                score.Property(t => t.Total)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.Speed)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.Accuracy)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.Verbal)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.Numerical)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.Abstract)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();
            });
        });

        builder.OwnsOne(tests => tests.PersonalityAssessment, personality =>
        {
            personality.Property(c => c.PackageInstanceId)
                .HasMaxLength(EntityConfiguration.Title)
                .IsRequired();

            personality.Property(c => c.PackageTypeId)
                .HasMaxLength(EntityConfiguration.Title)
                .IsRequired();

            personality.Property(c => c.Status)
                .HasConversion<string>()
                .HasMaxLength(EntityConfiguration.Clasificator);

            personality.Property(c => c.Url)
                .HasMaxLength(EntityConfiguration.LinkUrl)
                .IsRequired();

            personality.Property(c => c.StartedAt);
            personality.Property(c => c.CompletedAt);

            personality.OwnsOne(d => d.Scores, score =>
            {
                score.Property(t => t.A1)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.A2)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.W1)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.W2)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.R1)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.R2)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.S1)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.S2)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.Y1)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.Y2)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.SD)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();

                score.Property(t => t.AQ)
                    .HasColumnType(EntityConfiguration.MoneySqlType)
                    .IsRequired();
            });
        });

        builder.OwnsOne(tests => tests.PapiDynamicWheel, papi =>
        {
            papi.Property(c => c.InstanceId)
                .HasMaxLength(EntityConfiguration.Title)
                .IsRequired();

            papi.Property(c => c.Url)
                .HasMaxLength(EntityConfiguration.LinkUrl)
                .IsRequired();
        });

        builder.OwnsOne(tests => tests.PapiGeneralFeedback, papi =>
        {
            papi.Property(c => c.InstanceId)
                .HasMaxLength(EntityConfiguration.Title)
                .IsRequired();

            papi.Property(c => c.Url)
                .HasMaxLength(EntityConfiguration.LinkUrl)
                .IsRequired();
        });

        builder.OwnsOne(tests => tests.LgiGeneralFeedback, papi =>
        {
            papi.Property(c => c.InstanceId)
                .HasMaxLength(EntityConfiguration.Title)
                .IsRequired();

            papi.Property(c => c.Url)
                .HasMaxLength(EntityConfiguration.LinkUrl)
                .IsRequired();
        });

        builder.Ignore(c => c.DomainEvents);
    }
}
