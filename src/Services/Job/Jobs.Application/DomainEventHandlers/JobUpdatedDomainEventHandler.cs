using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Domain.Events.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.DomainEventHandlers
{
    public class JobUpdatedDomainEventHandler : INotificationHandler<JobUpdatedDomainEvent>
    {
        private readonly IJobCandidatesRepository _jobCandidatesRepository;

        public JobUpdatedDomainEventHandler(IJobCandidatesRepository jobCandidatesRepository)
        {
            _jobCandidatesRepository = jobCandidatesRepository;
        }

        public async Task Handle(JobUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var jobCandidates = await _jobCandidatesRepository.GetIfExistAsync(notification.Job.Id);

            if (jobCandidates == null) return;

            jobCandidates.SyncJob(
                notification.Job.Position, 
                notification.Job.Terms?.Freelance?.WorkType,
                notification.Job.Terms?.Permanent?.WorkType,
                notification.Job.DeadlineDate,
                notification.Job.Terms?.Availability?.From);

            _jobCandidatesRepository.Update(jobCandidates);
            await _jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>(cancellationToken);
        }
    }
}
