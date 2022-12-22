using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Companies.Application.Commands.ContactPersons
{
    public  class RejectContactPersonCommandHandler : INotificationHandler<RejectContactPersonCommand>
    {
        private readonly ICompanyRepository _companyRepository;

        public RejectContactPersonCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task Handle(RejectContactPersonCommand notification, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetAsync(notification.CompanyId);
            company.RejectContactPerson(notification.ContactId);

            _companyRepository.Update(company);
            await _companyRepository.UnitOfWork.SaveEntitiesAsync<Company>(cancellationToken);
        }
    }
}
