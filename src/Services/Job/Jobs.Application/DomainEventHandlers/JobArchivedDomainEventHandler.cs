using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Domain.Events.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.DomainEventHandlers;

public class JobArchivedDomainEventHandler : INotificationHandler<JobArchivedDomainEvent>
{
    private readonly IJobCandidatesRepository _jobCandidatesRepository;

    public JobArchivedDomainEventHandler(IJobCandidatesRepository jobCandidatesRepository)
    {
        _jobCandidatesRepository = jobCandidatesRepository;
    }

    public async Task Handle(JobArchivedDomainEvent notification, CancellationToken cancellationToken)
    {
        var jobCandidates = await _jobCandidatesRepository.GetAsync(notification.Job.Id);

        jobCandidates.SyncJobStage(notification.Job.Stage);
        _jobCandidatesRepository.Update(jobCandidates);

        await _jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>(cancellationToken);
    }
}