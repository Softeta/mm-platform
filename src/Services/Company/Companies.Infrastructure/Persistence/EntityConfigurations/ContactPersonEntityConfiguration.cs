using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Companies.Infrastructure.Persistence.EntityConfigurations
{
    internal class ContactPersonEntityConfigurationEntityConfiguration : IEntityTypeConfiguration<ContactPerson>
    {
        public void Configure(EntityTypeBuilder<ContactPerson> builder)
        {
            builder.ToTable("ContactPersons", Constants.DefaultSchema);

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.CompanyId);

            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(p => p.CompanyId)
                .IsRequired();

            builder.Property(p => p.Stage)
                .HasMaxLength(EntityConfiguration.Clasificator)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.Role)
                .HasMaxLength(EntityConfiguration.Clasificator)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.FirstName)
                .HasMaxLength(EntityConfiguration.Alias);

            builder.Property(p => p.LastName)
                .HasMaxLength(EntityConfiguration.Alias);

            builder.OwnsOne(p => p.Position, position =>
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
                        .HasMaxLength(EntityConfiguration.Clasificator)
                        .IsRequired();
                });
            });

            builder.OwnsOne(p => p.Phone, phone =>
            {
                phone.Property(ph => ph.CountryCode)
                    .HasMaxLength(EntityConfiguration.PhoneCountryCode);

                phone.Property(ph => ph.Number)
                    .HasMaxLength(EntityConfiguration.PhoneDigits);

                phone.Property(ph => ph.PhoneNumber)
                    .HasMaxLength(EntityConfiguration.PhoneNumber);
            });

            builder.OwnsOne(p => p.Email, email =>
            {
                email.Property(e => e.Address)
                    .HasMaxLength(EntityConfiguration.Email)
                    .IsRequired();

                email.HasIndex(e => e.Address)
                    .IsUnique();

                email.Property(e => e.IsVerified)
                    .IsRequired();

                email.Property(e => e.VerificationKey);

                email.Property(e => e.VerificationRequestedAt);

                email.Property(e => e.VerifiedAt);
            }).Navigation(p => p.Email).IsRequired();

            builder.OwnsOne(c => c.Picture, picture =>
            {
                picture.Property(p => p.OriginalUri)
                    .HasMaxLength(EntityConfiguration.LinkUrl)
                    .IsRequired();

                picture.Property(p => p.ThumbnailUri)
                    .HasMaxLength(EntityConfiguration.LinkUrl)
                    .IsRequired();
            });

            builder.Property(c => c.SystemLanguage)
                .HasMaxLength(EntityConfiguration.Clasificator)
                .HasConversion<string>();

            builder.OwnsOne(c => c.TermsAndConditions, terms =>
            {
                terms.Property(p => p.Agreed)
                    .IsRequired();

                terms.Property(p => p.ModifiedAt);
            });

            builder.OwnsOne(c => c.MarketingAcknowledgement, terms =>
            {
                terms.Property(p => p.Agreed)
                    .IsRequired();

                terms.Property(p => p.ModifiedAt);
            });

            builder.OwnsOne(c => c.CreatedBy, createdBy =>
            {
                createdBy.Property(p => p.Id)
                    .IsRequired();

                createdBy.Property(p => p.Scope)
                    .HasMaxLength(EntityConfiguration.Clasificator)
                    .HasConversion<string>()
                    .IsRequired();
            });

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.RejectedAt);
        }
    }
}
