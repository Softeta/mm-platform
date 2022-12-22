using MediatR;

namespace Companies.Application.Commands.Companies
{
    public record ApproveCompanyCommand(Guid CompanyId) : INotification;
}
