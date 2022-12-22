using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class SyncJobsAsUpdatedCommandHandler : INotificationHandler<SyncJobsAsUpdatedCommand>
    {
        private readonly IJobRepository _jobRepository;

        public SyncJobsAsUpdatedCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task Handle(SyncJobsAsUpdatedCommand request, CancellationToken cancellationToken)
        {
            foreach (var jobId in request.JobIds)
            {
                try
                {
                    var job = await _jobRepository.GetAsync(jobId);
                    job.PublishJobUpdatedEvent();

                    _jobRepository.Update(job);
                }
                catch
                {
                    // ignored
                }
            }

            await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);
        }
    }
}
