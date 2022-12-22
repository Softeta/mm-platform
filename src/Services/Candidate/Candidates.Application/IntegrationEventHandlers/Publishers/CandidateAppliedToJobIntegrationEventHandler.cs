using Candidates.Application.EventBus.Publishers;
using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads;
using Candidates.Application.Queries;
using Candidates.Domain.Events.CandidateJobsAggregate;
using Domain.Seedwork.Exceptions;
using MediatR;

namespace Candidates.Application.IntegrationEventHandlers.Publishers
{
    internal class CandidateAppliedToJobIntegrationEventHandler :
        INotificationHandler<CandidateAppliedToJobDomainEvent>
    {
        private readonly ICandidateJobsEventBusPublisher _eventBus;
        private readonly IMediator _mediator;

        public CandidateAppliedToJobIntegrationEventHandler(ICandidateJobsEventBusPublisher eventBus, IMediator mediator)
        {
            _eventBus = eventBus;
            _mediator = mediator;
        }

        public async Task Handle(CandidateAppliedToJobDomainEvent notification, CancellationToken cancellationToken)
        {
            var candidate = await _mediator.Send(new GetCandidateByIdQuery(notification.CandidateJobs.Id));

            if (candidate is null)
            {
                throw new NotFoundException("Candidate not found", ErrorCodes.NotFound.CandidateNotFound);
            }

            var payload = CandidateAppliedToJobPayload.FromEvent(
                notification.CandidateJobs,
                notification.JobId,
                candidate);

            var @event = new CandidateAppliedToJobIntegrationEvent(payload, notification.EmittedAt);
            await _eventBus.PublishAsync(@event, notification.EventName, cancellationToken);
        }
    }
}
