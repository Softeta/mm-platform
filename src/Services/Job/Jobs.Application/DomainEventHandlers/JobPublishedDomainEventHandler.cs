using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Domain.Events.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.DomainEventHandlers
{
    internal class JobPublishedDomainEventHandler : INotificationHandler<JobPublishedDomainEvent>
    {
        private readonly IJobCandidatesRepository _jobCandidatesRepository;

        public JobPublishedDomainEventHandler(IJobCandidatesRepository jobCandidatesRepository)
        {
            _jobCandidatesRepository = jobCandidatesRepository;
        }

        public async Task Handle(JobPublishedDomainEvent notification, CancellationToken cancellationToken)
        {
            var job = notification.Job;
            var jobCandidates = await _jobCandidatesRepository.GetIfExistAsync(job.Id);

            if (jobCandidates is null)
            {
                _jobCandidatesRepository.Add(new JobCandidates(
                    job.Id,
                    job.Stage,
                    job.Position.Id,
                    job.Position.Code,
                    job.Position.AliasTo?.Id,
                    job.Position.AliasTo?.Code,
                    job.Company.Id,
                    job.Company.Name,
                    job.Company.LogoUri,
                    job.Terms?.Freelance?.WorkType,
                    job.Terms?.Permanent?.WorkType,
                    job.DeadlineDate,
                    job.Terms?.Availability?.From));

                await _jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>(cancellationToken);
            }            
        }
    }
}
