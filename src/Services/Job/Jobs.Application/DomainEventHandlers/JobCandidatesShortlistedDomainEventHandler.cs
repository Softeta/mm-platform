using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Domain.Events.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.DomainEventHandlers
{
    internal class JobCandidatesShortlistedDomainEventHandler : INotificationHandler<JobCandidatesShortlistedDomainEvent>
    {
        private readonly IJobRepository _jobRepository;

        public JobCandidatesShortlistedDomainEventHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task Handle(JobCandidatesShortlistedDomainEvent notification, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetAsync(notification.JobCandidates.Id);

            job.ShortlistSent();
            _jobRepository.Update(job);

            await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);
        }
    }
}
