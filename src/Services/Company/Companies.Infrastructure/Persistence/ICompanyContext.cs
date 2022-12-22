using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Domain.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Persistence
{
    public interface ICompanyContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<ContactPerson> ContactPersons { get; set; }

        public DbSet<RegistryCenterCompany> RegistryCenterCompanies { get; set; }
    }
}
