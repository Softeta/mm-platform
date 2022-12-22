using Companies.Application.Commands.Validations;
using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Infrastructure.Persistence.Repositories;
using MediatR;
using Persistence.Customization.Queries;
using Persistence.Customization.TableStorage;

namespace Companies.Application.Commands.ContactPersons
{
    internal class AddCompanyContactPersonCommandHandler : IRequestHandler<AddCompanyContactPersonCommand, ContactPerson>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMediator _mediator;

        public AddCompanyContactPersonCommandHandler(
            ICompanyRepository companyRepository,
            IMediator mediator)
        {
            _companyRepository = companyRepository;
            _mediator = mediator;
        }

        public async Task<ContactPerson> Handle(AddCompanyContactPersonCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new ValidateContactPersonDuplicationByEmailValidation(request.ContactPerson.Email));

            var company = await _companyRepository.GetAsync(request.CompanyId);

            var getPictureCommand = new GetImageQuery(request.ContactPerson.Picture?.CacheId,
                    FileCacheTableStorage.Company.FilePartitionKey);
            var personImagePaths = await _mediator.Send(getPictureCommand);

            var newContactPerson = company.AddContactPerson(
                request.ContactPerson.Email,
                request.ContactPerson.Role,
                request.ContactPerson.FirstName,
                request.ContactPerson.LastName,
                request.ContactPerson.Phone?.CountryCode,
                request.ContactPerson.Phone?.Number,
                request.ContactPerson.Position?.Id,
                request.ContactPerson.Position?.Code,
                request.ContactPerson.Position?.AliasTo?.Id,
                request.ContactPerson.Position?.AliasTo?.Code,
                personImagePaths,
                request.ContactPerson.Picture?.CacheId,
                request.CreatedById,
                request.CreatedByScope);

            _companyRepository.Update(company);
            await _companyRepository.UnitOfWork.SaveEntitiesAsync<Company>(cancellationToken);

            return newContactPerson;
        }
    }
}
