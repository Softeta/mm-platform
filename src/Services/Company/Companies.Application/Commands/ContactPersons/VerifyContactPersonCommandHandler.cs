using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Infrastructure.Persistence.Repositories;
using Companies.Infrastructure.Settings;
using Domain.Seedwork.Exceptions;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.Graph.ExternalConnectors;

namespace Companies.Application.Commands.ContactPersons
{
    public class VerifyContactPersonCommandHandler : IRequestHandler<VerifyContactPersonCommand, ContactPerson>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly VerificationSettings _verificationSettings;

        public VerifyContactPersonCommandHandler(
            ICompanyRepository candidateRepository, 
            IOptions<VerificationSettings> options)
        {
            _companyRepository = candidateRepository;
            _verificationSettings = options.Value;
        }

        public async Task<ContactPerson> Handle(VerifyContactPersonCommand notification, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetAsync(notification.CompanyId);

            var contactPerson = company.VerifyContactPersonEmail(
                notification.ContactId,
                notification.Key,
                _verificationSettings.ExpiresInMinutes);

            _companyRepository.Update(company);
            await _companyRepository.UnitOfWork.SaveEntitiesAsync<Company>(cancellationToken);

            return contactPerson;
        }
    }
}
