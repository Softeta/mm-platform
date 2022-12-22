using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Candidates.Infrastructure.Persistence.EntityConfigurations.CandidateAggregate
{
    internal class CandidateHobbyEntityConfiguration : IEntityTypeConfiguration<CandidateHobby>
    {
        public void Configure(EntityTypeBuilder<CandidateHobby> builder)
        {
            builder.ToTable("CandidateHobbies", Constants.DefaultSchema);

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => new { s.CandidateId, s.HobbyId }).IsUnique();

            builder.HasIndex(s => s.CandidateId);

            builder.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(s => s.HobbyId)
                .IsRequired();

            builder.Property(s => s.CandidateId)
                .IsRequired();

            builder.Property(s => s.Code)
                .HasMaxLength(EntityConfiguration.Clasificator)
                .IsRequired();

            builder.Property(s => s.CreatedAt)
                .IsRequired();
        }
    }
}
