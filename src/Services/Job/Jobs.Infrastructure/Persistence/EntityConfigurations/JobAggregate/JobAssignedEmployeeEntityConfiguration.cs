using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Customization.EntityConfigurations;

namespace Jobs.Infrastructure.Persistence.EntityConfigurations.JobAggregate
{
    internal class JobAssignedEmployeeEntityConfiguration : IEntityTypeConfiguration<JobAssignedEmployee>
    {
        public void Configure(EntityTypeBuilder<JobAssignedEmployee> builder)
        {
            builder.ToTable("AssignedEmployees", Constants.DefaultSchema);

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.JobId);

            builder.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(e => e.JobId)
                .IsRequired();

            builder.OwnsOne(assignedEmployee => assignedEmployee.Employee, employee =>
            {
                employee.Property(e => e.Id)
                    .IsRequired();

                employee.Property(e => e.FirstName)
                    .HasMaxLength(EntityConfiguration.Alias)
                    .IsRequired();

                employee.Property(e => e.LastName)
                    .HasMaxLength(EntityConfiguration.Alias)
                    .IsRequired();

                employee.Property(e => e.PictureUri)
                    .HasMaxLength(EntityConfiguration.LinkUrl);
            }).Navigation(e => e.Employee).IsRequired();

            builder.Property(e => e.CreatedAt)
                .IsRequired();
        }
    }
}
