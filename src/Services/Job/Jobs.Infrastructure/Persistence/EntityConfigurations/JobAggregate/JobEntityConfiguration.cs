using Jobs.Domain.Aggregates.JobAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Jobs.Infrastructure.Persistence.EntityConfigurations.JobAggregate
{
    internal class JobEntityConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Jobs", Constants.DefaultSchema);

            builder.HasKey(j => j.Id);

            builder.Property(j => j.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(j => j.DeadlineDate);

            builder.Property(j => j.Description)
                .HasMaxLength(EntityConfiguration.LongDescription);

            builder.Property(j => j.Stage)
                .HasConversion<string>()
                .HasMaxLength(EntityConfiguration.Clasificator)
                .IsRequired();

            builder.Property(j => j.CreatedAt)
                .IsRequired();

            builder.Property(j => j.IsPriority)
                .IsRequired();

            builder.Property(j => j.Location)
                .HasMaxLength(EntityConfiguration.Address);

            builder.Property(j => j.IsArchived);
            
            builder.Property(j => j.IsActivated);

            builder.Property(j => j.IsPublished).IsRequired();

            builder.Property(j => j.IsSelectionStarted).IsRequired();

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

                company.Property(c => c.Status)
                    .HasMaxLength(EntityConfiguration.Clasificator)
                    .HasConversion<string>()
                    .IsRequired();

                company.Property(c => c.Name)
                    .HasMaxLength(EntityConfiguration.Alias)
                    .IsRequired();

                company.Property(c => c.Description)
                    .HasMaxLength(EntityConfiguration.LongDescription);

                company.Property(c => c.LogoUri)
                    .HasMaxLength(EntityConfiguration.LinkUrl);

                company.OwnsOne(c => c.Address, address =>
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

                company.OwnsMany(c => c.ContactPersons, cp =>
                {
                    cp.ToTable("JobContactPersons", Constants.DefaultSchema);

                    cp.HasKey(p => p.Id);

                    cp.HasIndex(p => p.JobId);

                    cp.Property(p => p.Id)
                        .IsRequired()
                        .ValueGeneratedNever();

                    cp.Property(p => p.JobId)
                        .IsRequired();

                    cp.Property(p => p.PersonId)
                        .IsRequired();

                    cp.Property(p => p.IsMainContact)
                        .IsRequired();

                    cp.Property(p => p.FirstName)
                        .HasMaxLength(EntityConfiguration.Alias)
                        .IsRequired();

                    cp.Property(p => p.LastName)
                        .HasMaxLength(EntityConfiguration.Alias)
                        .IsRequired();

                    cp.OwnsOne(p => p.Position, position =>
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
                    });

                    cp.Property(p => p.PhoneNumber)
                        .HasMaxLength(EntityConfiguration.PhoneNumber);

                    cp.Property(p => p.Email)
                        .HasMaxLength(EntityConfiguration.Email)
                        .IsRequired();

                    cp.Property(p => p.PictureUri)
                        .HasMaxLength(EntityConfiguration.LinkUrl);

                    cp.Property(p => p.SystemLanguage)
                        .HasConversion<string>()
                        .HasMaxLength(EntityConfiguration.Clasificator);

                    cp.Property(p => p.ExternalId);

                    cp.Property(p => p.CreatedAt)
                        .IsRequired();

                    cp.WithOwner().HasForeignKey(p => p.JobId);
                });
            }).Navigation(j => j.Company).IsRequired();


            builder.OwnsOne(job => job.Owner, owner =>
            {
                owner.Property(o => o.Id)
                    .IsRequired();

                owner.Property(o => o.FirstName)
                    .HasMaxLength(EntityConfiguration.Alias)
                    .IsRequired();

                owner.Property(o => o.LastName)
                    .HasMaxLength(EntityConfiguration.Alias)
                    .IsRequired();

                owner.Property(o => o.PictureUri)
                    .HasMaxLength(EntityConfiguration.LinkUrl);
            });

            builder.OwnsOne(job => job.YearExperience, yearExperience =>
            {
                yearExperience.Property(y => y.From);

                yearExperience.Property(y => y.To);
            });

            builder.OwnsOne(job => job.Terms, terms =>
            {
                terms.Property(d => d.IsUrgent)
                    .IsRequired();

                terms.OwnsOne(d => d.Availability, range =>
                {
                    range.Property(t => t.From);

                    range.Property(t => t.To);
                });

                terms.Property(t => t.Currency)
                    .HasMaxLength(EntityConfiguration.Currency);

                terms.OwnsOne(t => t.Freelance, freelance =>
                {
                    freelance.Property(f => f.WorkType)
                        .HasMaxLength(EntityConfiguration.Clasificator)
                        .HasConversion<string>()
                        .IsRequired();

                    freelance.Property(f => f.HoursPerProject);

                    freelance.OwnsOne(f => f.HourlyBudget, hourlyBudget =>
                    {
                        hourlyBudget.Property(h => h.From)
                            .HasColumnType(EntityConfiguration.MoneySqlType);

                        hourlyBudget.Property(h => h.To)
                            .HasColumnType(EntityConfiguration.MoneySqlType);
                    });

                    freelance.OwnsOne(f => f.MonthlyBudget, monthlyBudget =>
                    {
                        monthlyBudget.Property(h => h.From)
                            .HasColumnType(EntityConfiguration.MoneySqlType);

                        monthlyBudget.Property(h => h.To)
                            .HasColumnType(EntityConfiguration.MoneySqlType);
                    });
                });

                terms.OwnsOne(t => t.Permanent, permanent =>
                    {
                        permanent.Property(p => p.WorkType)
                            .HasMaxLength(EntityConfiguration.Clasificator)
                            .HasConversion<string>()
                            .IsRequired();

                        permanent.OwnsOne(f => f.MonthlyBudget, monthlyBudget =>
                        {
                            monthlyBudget.Property(h => h.From)
                                .HasColumnType(EntityConfiguration.MoneySqlType);

                            monthlyBudget.Property(h => h.To)
                                .HasColumnType(EntityConfiguration.MoneySqlType);
                        });
                    });

                terms.OwnsOne(t => t.PartTimeWorkingHours, partTimeWorkingHours =>
                {
                    partTimeWorkingHours.Property(p => p.Type)
                        .HasMaxLength(EntityConfiguration.Clasificator)
                        .HasConversion<string>()
                        .IsRequired();

                    partTimeWorkingHours.Property(p => p.Weekly);
                });

                terms.OwnsOne(t => t.FullTimeWorkingHours, fullTimeWorkingHours =>
                {
                    fullTimeWorkingHours.Property(p => p.Type)
                        .HasMaxLength(EntityConfiguration.Clasificator)
                        .HasConversion<string>()
                        .IsRequired();
                });

                terms.OwnsOne(t => t.ProjectWorkingHours, projectWorkingHours =>
                {
                    projectWorkingHours.Property(p => p.Type)
                        .HasMaxLength(EntityConfiguration.Clasificator)
                        .HasConversion<string>()
                        .IsRequired();
                });

                terms.OwnsOne(t => t.Formats, formats =>
                {
                    formats.Property(f => f.IsRemote)
                        .IsRequired();

                    formats.Property(f => f.IsHybrid)
                        .IsRequired();

                    formats.Property(f => f.IsOnSite)
                        .IsRequired();
                });
            }).Navigation(x => x.Terms).IsRequired();

            builder.OwnsOne(job => job.Sharing, sharing =>
            {
                sharing.Property(s => s.Key)
                    .IsRequired();

                sharing.Property(s => s.Date)
                    .IsRequired();
            });

            builder.HasMany(j => j.AssignedEmployees)
                .WithOne()
                .HasForeignKey(e => e.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(j => j.Languages)
                .WithOne()
                .HasForeignKey(l => l.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(j => j.SeniorityLevels)
                .WithOne()
                .HasForeignKey(s => s.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(j => j.Skills)
                .WithOne()
                .HasForeignKey(s => s.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(j => j.Industries)
                .WithOne()
                .HasForeignKey(s => s.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(j => j.ParentJobId);

            builder.Ignore(j => j.DomainEvents);
        }
    }
}
