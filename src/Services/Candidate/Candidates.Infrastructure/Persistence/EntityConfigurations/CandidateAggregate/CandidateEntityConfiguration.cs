using Candidates.Domain.Aggregates.CandidateAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Candidates.Infrastructure.Persistence.EntityConfigurations.CandidateAggregate;

internal class CandidateEntityConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable("Candidates", Constants.DefaultSchema);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(c => c.Status)
            .HasMaxLength(EntityConfiguration.Clasificator)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(c => c.ExternalId)
            .HasColumnType(EntityConfiguration.Identifier);

        builder.Property(c => c.FirstName)
            .HasMaxLength(EntityConfiguration.Alias);

        builder.Property(c => c.LastName)
            .HasMaxLength(EntityConfiguration.Alias);

        builder.Property(x => x.IsShortListed)
            .IsRequired();

        builder.Property(c => c.IsHired)
            .IsRequired();

        builder.OwnsOne(c => c.Email, email =>
        {
            email.Property(p => p.Address)
                .HasMaxLength(EntityConfiguration.Email)
                .IsRequired();

            email.Property(p => p.IsVerified)
                .IsRequired();

            email.Property(p => p.VerificationKey);

            email.Property(p => p.VerificationRequestedAt);

            email.Property(p => p.VerifiedAt);
        });

        builder.OwnsOne(c => c.Picture, picture =>
        {
            picture.Property(p => p.OriginalUri)
                .HasMaxLength(EntityConfiguration.LinkUrl)
                .IsRequired();

            picture.Property(p => p.ThumbnailUri)
                .HasMaxLength(EntityConfiguration.LinkUrl)
                .IsRequired();
        });

        builder.OwnsOne(c => c.CurrentPosition, position =>
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

        builder.Property(c => c.BirthDate);

        builder.Property(c => c.OpenForOpportunities)
            .IsRequired();

        builder.Property(c => c.LinkedInUrl)
            .HasMaxLength(EntityConfiguration.LinkUrl);

        builder.Property(c => c.PersonalWebsiteUrl)
            .HasMaxLength(EntityConfiguration.LinkUrl);

        builder.Property(c => c.SystemLanguage)
            .HasMaxLength(EntityConfiguration.Clasificator)
            .HasConversion<string>();

        builder.Property(c => c.YearsOfExperience);

        builder.Property(c => c.Bio)
            .HasMaxLength(EntityConfiguration.LongDescription);

        builder.HasMany(c => c.ActivityStatuses)
            .WithOne()
            .HasForeignKey(f => f.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(c => c.CurriculumVitae, curriculumVitae =>
        {
            curriculumVitae.Property(c => c.Uri)
                .HasMaxLength(EntityConfiguration.LinkUrl);
            curriculumVitae.Property(c => c.FileName)
                .HasMaxLength(EntityConfiguration.Title);
        });

        builder.OwnsOne(c => c.Video, video =>
        {
            video.Property(v => v.Uri)
                .HasMaxLength(EntityConfiguration.LinkUrl);
            video.Property(v => v.FileName)
                .HasMaxLength(EntityConfiguration.Title);
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
                .HasMaxLength(EntityConfiguration.PostalCode);

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

        builder.OwnsOne(c => c.Terms, terms =>
        {
            terms.OwnsOne(d => d.Availability, range =>
            {
                range.Property(t => t.From);

                range.Property(t => t.To);
            });

            terms.Property(t => t.Currency);

            terms.OwnsOne(t => t.Freelance, freelance =>
            {
                freelance.Property(f => f.WorkType)
                    .HasMaxLength(EntityConfiguration.Clasificator)
                    .HasConversion<string>()
                    .IsRequired();

                freelance.Property(f => f.HourlySalary)
                    .HasColumnType(EntityConfiguration.MoneySqlType);

                freelance.Property(f => f.MonthlySalary)
                    .HasColumnType(EntityConfiguration.MoneySqlType);
            });

            terms.OwnsOne(t => t.Permanent, permanent =>
            {
                permanent.Property(p => p.WorkType)
                    .HasMaxLength(EntityConfiguration.Clasificator)
                    .HasConversion<string>()
                    .IsRequired();

                permanent.Property(p => p.MonthlySalary)
                    .HasColumnType(EntityConfiguration.MoneySqlType);
            });

            terms.OwnsOne(t => t.FulTimeWorkingHours, workingHours =>
            {
                workingHours.Property(w => w.Type)
                    .HasMaxLength(EntityConfiguration.Clasificator)
                    .HasConversion<string>()
                    .IsRequired();
            });

            terms.OwnsOne(t => t.PartTimeWorkingHours, workingHours =>
            {
                workingHours.Property(w => w.Type)
                    .HasMaxLength(EntityConfiguration.Clasificator)
                    .HasConversion<string>()
                    .IsRequired();

                workingHours.Property(w => w.Weekly);
            });

            terms.OwnsOne(t => t.ProjectWorkingHours, workingHours =>
            {
                workingHours.Property(w => w.Type)
                    .HasMaxLength(EntityConfiguration.Clasificator)
                    .HasConversion<string>()
                    .IsRequired();
            });

            terms.OwnsOne(t => t.Formats, format =>
            {
                format.Property(w => w.IsRemote)
                    .IsRequired();

                format.Property(w => w.IsOnSite)
                    .IsRequired();

                format.Property(w => w.IsHybrid)
                    .IsRequired();
            });
        }).Navigation(x => x.Terms).IsRequired();

        builder.OwnsOne(candidate => candidate.Phone, communicationPreference =>
        {
            communicationPreference.Property(c => c.CountryCode)
                .HasMaxLength(EntityConfiguration.PhoneCountryCode);

            communicationPreference.Property(c => c.Number)
                .HasMaxLength(EntityConfiguration.PhoneDigits);

            communicationPreference.Property(c => c.PhoneNumber)
                .HasMaxLength(EntityConfiguration.PhoneNumber);
        });

        builder.OwnsOne(candidate => candidate.Note, note =>
        {
            note.Property(n => n.Value)
               .HasMaxLength(EntityConfiguration.Note)
               .IsRequired();

            note.Property(n => n.EndDate);
        });

        builder.HasMany(c => c.Industries)
            .WithOne()
            .HasForeignKey(f => f.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.DesiredSkills)
            .WithOne()
            .HasForeignKey(f => f.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Skills)
            .WithOne()
            .HasForeignKey(f => f.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Languages)
            .WithOne()
            .HasForeignKey(f => f.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Skills)
            .WithOne()
            .HasForeignKey(f => f.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Educations)
            .WithOne()
            .HasForeignKey(f => f.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.WorkExperiences)
            .WithOne()
            .HasForeignKey(f => f.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Courses)
            .WithOne()
            .HasForeignKey(f => f.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Hobbies)
            .WithOne()
            .HasForeignKey(f => f.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(c => c.DomainEvents);
    }
}
