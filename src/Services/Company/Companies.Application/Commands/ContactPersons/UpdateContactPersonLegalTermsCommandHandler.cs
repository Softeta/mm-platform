using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Infrastructure.Persistence.Repositories;
using Domain.Seedwork.Exceptions;
using MediatR;

namespace Companies.Application.Commands.ContactPersons
{
    internal class UpdateContactPersonLegalTermsCommandHandler : IRequestHandler<UpdateContactPersonLegalTermsCommand, ContactPerson>
    {
        private readonly ICompanyRepository _companyRepository;

        public UpdateContactPersonLegalTermsCommandHandler(
            ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<ContactPerson> Handle(UpdateContactPersonLegalTermsCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetAsync(request.CompanyId);

            var contactPerson = company.ContactPersons
                .FirstOrDefault(x => x.Id == request.ContactId);

            if (contactPerson is null)
            {
                throw new NotFoundException(
                    $"Contact person not found. ContactId: {request.ContactId}. CompanyId: {request.CompanyId}",
                    ErrorCodes.NotFound.ContactPersonNotFound);
            }

            var updatedContactPerson = company.UpdateContactPersonLegalTerms(
                request.ContactId,
                request.TermsAgreement,
                request.MarketingAgreement);

            _companyRepository.Update(company);
            await _companyRepository.UnitOfWork.SaveEntitiesAsync<Company>(cancellationToken);

            return updatedContactPerson;
        }
    }
}
