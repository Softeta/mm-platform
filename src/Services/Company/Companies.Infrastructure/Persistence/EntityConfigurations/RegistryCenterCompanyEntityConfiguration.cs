using Companies.Domain.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Companies.Infrastructure.Persistence.EntityConfigurations
{
    internal class RegistryCenterCompanyEntityConfiguration : IEntityTypeConfiguration<RegistryCenterCompany>
    {
        public void Configure(EntityTypeBuilder<RegistryCenterCompany> builder)
        {
            builder.ToTable("RegistryCenterCompanies", Constants.DefaultSchema);

            builder.HasKey(x => new { x.RegistrationNumber, x.Country });

            builder.HasIndex(x => new { x.Name });
            builder.HasIndex(x => new { x.RegistrationNumber });

            builder.Property(c => c.RegistrationNumber)
                .HasMaxLength(EntityConfiguration.Alias)
                .IsRequired();

            builder.Property(c => c.Name)
                .HasMaxLength(EntityConfiguration.Title)
                .IsRequired();

            builder.Property(c => c.Country)
                .HasMaxLength(EntityConfiguration.Country)
                .IsRequired();

            builder.Property(c => c.CountryCode)
                .HasMaxLength(EntityConfiguration.Country)
                .IsRequired();

            builder.Property(c => c.Region)
                .HasMaxLength(EntityConfiguration.Region);

            builder.Property(c => c.City)
                .HasMaxLength(EntityConfiguration.City);

            builder.Property(c => c.AddressLine)
                .HasMaxLength(EntityConfiguration.Address)
                .IsRequired();

            builder.Property(c => c.ZipCode)
                .HasMaxLength(EntityConfiguration.Alias);

            builder.HasMany(j => j.SearchIndexes)
                .WithOne()
                .HasForeignKey(s => new { s.RegistrationNumber, s.CountryCode })
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
