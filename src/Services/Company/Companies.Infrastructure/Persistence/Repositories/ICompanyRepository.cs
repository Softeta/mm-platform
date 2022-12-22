using Companies.Domain.Aggregates.CompanyAggregate;
using Domain.Seedwork;

namespace Companies.Infrastructure.Persistence.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> GetAsync(Guid id);
        Company Add(Company company);
        void Update(Company company);
        Task RemoveAsync(Guid id);
    }
}
