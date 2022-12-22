using MediatR;

namespace Candidates.Application.Commands
{
    public record SyncAllCandidatesCommand() : INotification;
}
