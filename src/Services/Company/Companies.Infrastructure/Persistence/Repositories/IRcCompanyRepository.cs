using Companies.Domain.ReadModels;
using Domain.Seedwork;

namespace Companies.Infrastructure.Persistence.Repositories
{
    public interface IRcCompanyRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task AddOrUpdateRange(IEnumerable<RegistryCenterCompany> companies);
    }
}
