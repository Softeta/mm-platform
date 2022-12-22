using Companies.Application.Queries;
using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Infrastructure.Persistence.Repositories;
using Domain.Seedwork.Exceptions;
using MediatR;

namespace Companies.Application.Commands.ContactPersons
{
    public class RequestEmailVerificationCommandHandler : INotificationHandler<RequestEmailVerificationCommand>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMediator _mediator;

        public RequestEmailVerificationCommandHandler(ICompanyRepository companyRepository, IMediator mediator)
        {
            _companyRepository = companyRepository;
            _mediator = mediator;
        }

        public async Task Handle(RequestEmailVerificationCommand request, CancellationToken cancellationToken)
        {
            var company = await _mediator.Send(
                new GetCompanyByContactPersonExternaldQuery(request.ExternalId),
                cancellationToken);

            if (company is null)
            {
                throw new NotFoundException(
                    $"Contact person does not exist. ExternalId: {request.ExternalId}", 
                    ErrorCodes.NotFound.ContactPersonNotFound);
            }

            company.RequestEmailVerification(request.ExternalId);

            _companyRepository.Update(company);
            await _companyRepository.UnitOfWork.SaveEntitiesAsync<Company>(cancellationToken);
        }
    }
}
