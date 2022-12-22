using Companies.Domain.ReadModels;
using Domain.Seedwork;

namespace Companies.Infrastructure.Persistence.Repositories
{
    public class RcCompanyRepository : IRcCompanyRepository
    {
        private readonly CompanyContext _context = null!;
        public IUnitOfWork UnitOfWork => _context;

        public RcCompanyRepository(CompanyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddOrUpdateRange(IEnumerable<RegistryCenterCompany> companies)
        {
            foreach (var company in companies)
            {
                var entity = await _context.RegistryCenterCompanies.FindAsync(company.RegistrationNumber, company.Country);
                
                if (entity == null)
                {
                    await _context.RegistryCenterCompanies.AddAsync(company);
                }
                else
                {
                    entity.Name = company.Name;
                    entity.RegistrationNumber = company.RegistrationNumber;
                    entity.AddressLine = company.AddressLine;
                    entity.CountryCode = company.CountryCode;
                    entity.Country = company.Country;
                    entity.Region = company.Region;
                    entity.City = company.City;
                    entity.ZipCode = company.ZipCode;

                    _context.RegistryCenterCompanies.Update(entity);                    
                }
            }
        }
    }
}
