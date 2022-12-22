using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Domain.Events.CandidateJobsAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using MediatR;
using Persistence.Customization.FileStorage.Clients.Private;

namespace Candidates.Application.DomainEventHandlers
{
    internal class CandidateInJobJobArchivedDomainEventHandler : INotificationHandler<CandidateInJobJobArchivedDomainEvent>
    {
        private readonly IPrivateFileDeleteClient _privateFileDeleteClient;
        private readonly ICandidateJobsRepository _candidateJobsRepository;
        public CandidateInJobJobArchivedDomainEventHandler(
            IPrivateFileDeleteClient privateFileDeleteClient,
            ICandidateJobsRepository candidateJobsRepository)
        {
            _privateFileDeleteClient = privateFileDeleteClient;
            _candidateJobsRepository = candidateJobsRepository;
        }

        public async Task Handle(CandidateInJobJobArchivedDomainEvent notification, CancellationToken cancellationToken)
        {
            var selectedInJob = notification.CandidateJobs.GetCandidateInJob(notification.JobId);
            var motivationVideoUri = selectedInJob?.MotivationVideo?.Uri;

            if (motivationVideoUri is not null)
            {
                await _privateFileDeleteClient.DeleteAsync(motivationVideoUri, cancellationToken);

                notification.CandidateJobs.RemoveCandidateJobMotivationVideo(notification.JobId);

                _candidateJobsRepository.Update(notification.CandidateJobs);
                await _candidateJobsRepository.UnitOfWork.SaveEntitiesAsync<CandidateJobs>(cancellationToken);
            }
        }
    }
}
