using Companies.Application.Queries;
using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Companies.Application.Commands.ContactPersons
{
    public class RegisterMyselfCommandHandler : IRequestHandler<RegisterMyselfCommand, ContactPerson>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMediator _mediator;

        public RegisterMyselfCommandHandler(
            ICompanyRepository companyRepository,
            IMediator mediator)

        {
            _companyRepository = companyRepository;
            _mediator = mediator;
        }

        public async Task<ContactPerson> Handle(RegisterMyselfCommand request, CancellationToken cancellationToken)
        {
            var company = await _mediator.Send(
                new GetCompanyByContactPersonEmailQuery(request.Email), 
                cancellationToken);

            if (company == null)
            {
                company = new Company();

                company.RegisterMyself(
                    request.Email,
                    request.ExternalId,
                    request.SystemLanguage,
                    request.AcceptTermsAndConditions,
                    request.AcceptMarketingAcknowledgement);

                _companyRepository.Add(company);
            } 
            else
            {
                company.LinkContactPerson(
                    request.Email,
                    request.ExternalId,
                    request.SystemLanguage,
                    request.AcceptTermsAndConditions,
                    request.AcceptMarketingAcknowledgement);

                _companyRepository.Update(company);
            }

            await _companyRepository.UnitOfWork.SaveEntitiesAsync<Company>(cancellationToken);

            return company.ContactPersons.Single(x => x.Email.Address == request.Email);
        }
    }
}
