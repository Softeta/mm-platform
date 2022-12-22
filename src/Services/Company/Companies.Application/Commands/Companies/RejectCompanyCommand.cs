using MediatR;

namespace Companies.Application.Commands.Companies
{
    public record RejectCompanyCommand(Guid CompanyId) : INotification;
}
