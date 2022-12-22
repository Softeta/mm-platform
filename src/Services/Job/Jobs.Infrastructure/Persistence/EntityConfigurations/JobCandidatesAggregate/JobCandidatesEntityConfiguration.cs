using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Jobs.Infrastructure.Persistence.EntityConfigurations.JobCandidatesAggregate
{
    internal class JobCandidatesEntityConfiguration : IEntityTypeConfiguration<JobCandidates>
    {
        public void Configure(EntityTypeBuilder<JobCandidates> builder)
        {
            builder.ToTable("JobCandidates", Constants.DefaultSchema);

            builder.HasKey(j => j.Id);

            builder.Property(j => j.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(j => j.Stage)
                .HasConversion<string>()
                .HasMaxLength(EntityConfiguration.Clasificator)
                .IsRequired();

            builder.Property(j => j.CreatedAt)
                .IsRequired();

            builder.Property(j => j.Freelance)
                .HasConversion<string>()
                .HasMaxLength(EntityConfiguration.Clasificator);

            builder.Property(j => j.Permanent)
                .HasConversion<string>()
                .HasMaxLength(EntityConfiguration.Clasificator);

            builder.Property(j => j.StartDate);

            builder.Property(j => j.DeadlineDate);

            builder.Property(j => j.ShortListActivatedAt);

            builder.OwnsOne(job => job.Position, position =>
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
            }).Navigation(j => j.Position).IsRequired();

            builder.OwnsOne(job => job.Company, company =>
            {
                company.Property(c => c.Id)
                    .IsRequired();

                company.Property(c => c.Name)
                    .HasMaxLength(EntityConfiguration.Alias)
                    .IsRequired();

                company.Property(c => c.LogoUri)
                    .HasMaxLength(EntityConfiguration.LinkUrl);

            }).Navigation(j => j.Company).IsRequired();


            builder.HasMany(j => j.SelectedCandidates)
                .WithOne()
                .HasForeignKey(s => s.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(j => j.ArchivedCandidates)
                .WithOne()
                .HasForeignKey(s => s.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(j => j.DomainEvents);
        }
    }
}
