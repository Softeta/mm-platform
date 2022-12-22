using Companies.Domain.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Companies.Infrastructure.Persistence.EntityConfigurations
{
    internal class SearchIndexEntityConfiguration : IEntityTypeConfiguration<SearchIndex>
    {
        public void Configure(EntityTypeBuilder<SearchIndex> builder)
        {
            builder.ToTable("SearchIndexes", Constants.DefaultSchema);

            builder.HasKey(s => s.Id);
            builder.HasIndex(s => s.Index);

            builder.Property(c => c.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(s => s.RegistrationNumber)
                .HasMaxLength(EntityConfiguration.Alias)
                .IsRequired();

            builder.Property(s => s.CountryCode)
                .HasMaxLength(EntityConfiguration.Country)
                .IsRequired();

            builder.Property(s => s.Index)
                .HasMaxLength(EntityConfiguration.Title)
                .IsRequired();
        }
    }
}
