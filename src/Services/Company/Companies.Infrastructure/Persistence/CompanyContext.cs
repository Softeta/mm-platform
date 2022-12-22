using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Domain.ReadModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Customization.Context;

namespace Companies.Infrastructure.Persistence
{
    public sealed class CompanyContext : BaseContext<CompanyContext>, ICompanyContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<RegistryCenterCompany> RegistryCenterCompanies { get; set; }

        public CompanyContext(DbContextOptions<CompanyContext> options, IMediator mediator)
            : base(options, mediator)
        {
            Companies = Set<Company>();
            ContactPersons = Set<ContactPerson>();
            RegistryCenterCompanies = Set<RegistryCenterCompany>();
        }
    }
}
