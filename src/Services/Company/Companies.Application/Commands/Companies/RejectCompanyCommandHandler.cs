using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Companies.Application.Commands.Companies
{
    public class RejectCompanyCommandHandler : INotificationHandler<RejectCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;

        public RejectCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task Handle(RejectCompanyCommand notification, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetAsync(notification.CompanyId);

            company.Reject();

            _companyRepository.Update(company);
            await _companyRepository.UnitOfWork.SaveEntitiesAsync<Company>(cancellationToken);
        }
    }
}
