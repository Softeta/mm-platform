using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Jobs.Infrastructure.Persistence.EntityConfigurations.JobAggregate
{
    internal class JobSeniorityEntityConfiguration : IEntityTypeConfiguration<JobSeniority>
    {
        public void Configure(EntityTypeBuilder<JobSeniority> builder)
        {
            builder.ToTable("JobSeniorities", Constants.DefaultSchema);

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => s.JobId);

            builder.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(s => s.JobId)
                .IsRequired();

            builder.Property(s => s.Seniority)
                .HasConversion<string>()
                .HasMaxLength(EntityConfiguration.Clasificator)
                .IsRequired();

            builder.Property(s => s.CreatedAt)
                .IsRequired();
        }
    }
}
