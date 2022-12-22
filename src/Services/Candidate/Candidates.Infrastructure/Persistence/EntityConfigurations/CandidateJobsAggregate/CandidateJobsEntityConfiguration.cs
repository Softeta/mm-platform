using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candidates.Infrastructure.Persistence.EntityConfigurations.CandidateJobsAggregate;

internal class CandidateJobsEntityConfiguration : IEntityTypeConfiguration<CandidateJobs>
{
    public void Configure(EntityTypeBuilder<CandidateJobs> builder)
    {
        builder.ToTable("CandidateJobs", Constants.DefaultSchema);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(c => c.IsShortlisted)
            .IsRequired();

        builder.HasMany(c => c.SelectedInJobs)
            .WithOne()
            .HasForeignKey(f => f.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.ArchivedInJobs)
            .WithOne()
            .HasForeignKey(f => f.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.AppliedInJobs)
            .WithOne()
            .HasForeignKey(f => f.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(c => c.DomainEvents);
    }
}
