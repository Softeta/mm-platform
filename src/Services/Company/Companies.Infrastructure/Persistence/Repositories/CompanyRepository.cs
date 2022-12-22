using Companies.Domain.Aggregates.CompanyAggregate;
using Domain.Seedwork;
using Domain.Seedwork.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Persistence.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyContext _context = null!;

        public IUnitOfWork UnitOfWork => _context;

        public CompanyRepository(CompanyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Company Add(Company company)
        {
            return _context.Companies.Add(company).Entity;
        }

        public async Task<Company> GetAsync(Guid id)
        {
            var company = await _context.Companies
                .Include(c => c.ContactPersons)
                .Include(c => c.Industries)
                .Where(c => c.Id == id)
                .AsSplitQuery()
                .SingleOrDefaultAsync();

            if (company == null)
            {
                throw new NotFoundException("Company not found", ErrorCodes.NotFound.CompanyNotFound);
            }

            return company;
        }

        public void Update(Company company)
        {
            _context.Entry(company).State = EntityState.Modified;
        }

        public async Task RemoveAsync(Guid id)
        {
            var companyToRemove = await _context.Companies
                .Where(x => x.Id == id)
                .SingleAsync();

            _context.Remove(companyToRemove);
        }
    }
}
