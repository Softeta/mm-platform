using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Companies.Application.Commands.Companies
{
    public class ApproveCompanyCommandHandler : INotificationHandler<ApproveCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;

        public ApproveCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task Handle(ApproveCompanyCommand notification, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetAsync(notification.CompanyId);

            company.Approve();

            _companyRepository.Update(company);
            await _companyRepository.UnitOfWork.SaveEntitiesAsync<Company>(cancellationToken);
        }
    }
}
