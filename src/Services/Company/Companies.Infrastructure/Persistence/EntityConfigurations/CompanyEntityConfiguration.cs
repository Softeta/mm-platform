using Companies.Domain.Aggregates.CompanyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Companies.Infrastructure.Persistence.EntityConfigurations
{
    internal class CompanyEntityConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies", Constants.DefaultSchema);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(c => c.Name)
                .HasMaxLength(EntityConfiguration.Alias);

            builder.Property(c => c.RegistrationNumber)
                .HasMaxLength(EntityConfiguration.Alias);

            builder.Property(c => c.Status)
                .HasMaxLength(EntityConfiguration.Clasificator)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(c => c.WebsiteUrl)
                .HasMaxLength(EntityConfiguration.LinkUrl);

            builder.Property(c => c.LinkedInUrl)
                .HasMaxLength(EntityConfiguration.LinkUrl);

            builder.Property(c => c.GlassdoorUrl)
                .HasMaxLength(EntityConfiguration.LinkUrl);

            builder.OwnsOne(c => c.Logo, logo =>
            {
                logo.Property(l => l.OriginalUri)
                    .HasMaxLength(EntityConfiguration.LinkUrl)
                    .IsRequired();

                logo.Property(l => l.ThumbnailUri)
                    .HasMaxLength(EntityConfiguration.LinkUrl)
                    .IsRequired();
            });

            builder.OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.AddressLine)
                    .HasMaxLength(EntityConfiguration.Address)
                    .IsRequired();

                address.Property(a => a.City)
                    .HasMaxLength(EntityConfiguration.City);

                address.Property(a => a.Country)
                    .HasMaxLength(EntityConfiguration.Country);

                address.Property(a => a.PostalCode)
                    .HasMaxLength(EntityConfiguration.PostalCode)
                    .IsRequired();

                address.Property(a => a.Location)
                    .HasMaxLength(EntityConfiguration.Address);

                address.OwnsOne(a => a.Coordinates, coordinates =>
                {
                    coordinates.Property(c => c.Longitude)
                        .IsRequired();

                    coordinates.Property(c => c.Latitude)
                        .IsRequired();

                    coordinates.Property(c => c.Point)
                        .IsRequired();
                });
            });

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.RejectedAt);

            builder.HasMany(c => c.ContactPersons)
                .WithOne()
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(j => j.Industries)
                .WithOne()
                .HasForeignKey(s => s.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(j => j.DomainEvents);
        }
    }
}
