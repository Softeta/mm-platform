using Contracts.Job.Jobs.Responses;
using MediatR;

namespace Jobs.Application.Commands
{
    public record ShareJobViaLinkCommand(
       Guid JobId
       ) : IRequest<ShareJobViaLinkResponse>;
}
