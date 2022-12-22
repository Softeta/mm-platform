using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Jobs.Infrastructure.Persistence.EntityConfigurations.JobAggregate
{
    internal class JobIndustryEntityConfiguration : IEntityTypeConfiguration<JobIndustry>
    {
        public void Configure(EntityTypeBuilder<JobIndustry> builder)
        {
            builder.ToTable("JobIndustries", Constants.DefaultSchema);

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => new { s.JobId, s.IndustryId }).IsUnique();

            builder.HasIndex(s => s.JobId);

            builder.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(s => s.IndustryId)
                .IsRequired();

            builder.Property(s => s.JobId)
                .IsRequired();

            builder.Property(s => s.Code)
                .HasMaxLength(EntityConfiguration.Clasificator)
                .IsRequired();

            builder.Property(s => s.CreatedAt)
                .IsRequired();
        }
    }
}
