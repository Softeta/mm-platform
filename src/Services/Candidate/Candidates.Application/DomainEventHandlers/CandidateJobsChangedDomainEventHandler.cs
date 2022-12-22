using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Domain.Events.CandidateJobsAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Candidates.Application.DomainEventHandlers
{
    internal class CandidateJobsChangedDomainEventHandler : 
        INotificationHandler<CandidateJobsUpdatedDomainEvent>,
        INotificationHandler<CandidateJobsArchivedDomainEvent>,
        INotificationHandler<CandidateJobsHiredDomainEvent>
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateJobsChangedDomainEventHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task Handle(CandidateJobsUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CandidateJobsArchivedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CandidateJobsHiredDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        private async Task HandleAsync(CandidateJobsChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var candidateId = notification.CandidateJobs.Id;
            var isShortListed = notification.CandidateJobs.IsShortlisted;
            var isHired = notification.CandidateJobs.IsHired;

            var candidate = await _candidateRepository.GetAsync(candidateId);

            candidate.ToggleIsShortlisted(isShortListed);
            candidate.ToggleIsHired(isHired);
            _candidateRepository.Update(candidate);

            await _candidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);
        }
    }
}
