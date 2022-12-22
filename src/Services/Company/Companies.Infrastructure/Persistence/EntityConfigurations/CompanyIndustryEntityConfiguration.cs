using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Companies.Infrastructure.Persistence.EntityConfigurations
{
    internal class CompanyIndustryEntityConfiguration : IEntityTypeConfiguration<CompanyIndustry>
    {
        public void Configure(EntityTypeBuilder<CompanyIndustry> builder)
        {
            builder.ToTable("CompanyIndustries", Constants.DefaultSchema);

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => new { s.CompanyId, s.IndustryId }).IsUnique();

            builder.HasIndex(s => s.CompanyId);

            builder.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(s => s.IndustryId)
                .IsRequired();

            builder.Property(s => s.CompanyId)
                .IsRequired();

            builder.Property(s => s.Code)
                .HasMaxLength(EntityConfiguration.Clasificator)
                .IsRequired();

            builder.Property(s => s.CreatedAt)
                .IsRequired();
        }
    }
}
