using Candidates.Application.Queries;
using MediatR;

namespace Candidates.Application.Commands
{
    public class SyncAllCandidatesCommandHandler : INotificationHandler<SyncAllCandidatesCommand>
    {
        private readonly IMediator _mediator;

        public SyncAllCandidatesCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(SyncAllCandidatesCommand request, CancellationToken cancellationToken)
        {
            var candidateIds = await _mediator.Send(new GetAllCandidateIdsQuery());
            await _mediator.Publish(new SyncCandidatesCommand(candidateIds));
        }
    }
}
