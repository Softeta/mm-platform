using Companies.Infrastructure.Persistence;
using Domain.Seedwork.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Companies.Application.Commands.Validations
{
    public class ValidateContactPersonDuplicationByEmailValidationHandler : INotificationHandler<ValidateContactPersonDuplicationByEmailValidation>
    {
        private readonly ICompanyContext _companyContext;

        public ValidateContactPersonDuplicationByEmailValidationHandler(ICompanyContext companyContext)
        {
            _companyContext = companyContext;
        }

        public async Task Handle(ValidateContactPersonDuplicationByEmailValidation notification, CancellationToken cancellationToken)
        {
            var contactPerson = await _companyContext.ContactPersons
                .AsNoTracking()
                .Where(x => x.Email.Address == notification.Email)
                .Select(x => new { x.FirstName, x.LastName })
                .SingleOrDefaultAsync(cancellationToken);

            if (contactPerson is not null)
            {
                throw new BadRequestException($"Contact person with {notification.Email} email already exists",
                    ErrorCodes.BadRequest.ContactPersonAlreadyExists,
                    new string[] { $"{contactPerson.FirstName} {contactPerson.LastName}" });
            }
        }
    }
}
