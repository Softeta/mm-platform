using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class SyncCandidatesCommandHandler : INotificationHandler<SyncSelectedCandidatesCommand>
    {
        private readonly IJobCandidatesRepository _jobCandidatesRepository;

        public SyncCandidatesCommandHandler(IJobCandidatesRepository jobCandidatesRepository)
        {
            _jobCandidatesRepository = jobCandidatesRepository;
        }

        public async Task Handle(SyncSelectedCandidatesCommand request, CancellationToken cancellationToken)
        {
            foreach (var jobId in request.JobIds)
            {
                try
                {
                    var candidate = await _jobCandidatesRepository.GetAsync(jobId);
                    candidate.PublishSelectedCandidatesUpdatedEvent();

                    _jobCandidatesRepository.Update(candidate);
                }
                catch
                {
                    // ignored
                }
            }

            await _jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>(cancellationToken);
        }
    }
}
