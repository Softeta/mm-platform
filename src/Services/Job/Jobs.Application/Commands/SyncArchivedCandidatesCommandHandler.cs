using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class SyncArchivedCandidatesCommandHandler : INotificationHandler<SyncArchivedCandidatesCommand>
    {
        private readonly IJobCandidatesRepository _jobCandidatesRepository;

        public SyncArchivedCandidatesCommandHandler(IJobCandidatesRepository jobCandidatesRepository)
        {
            _jobCandidatesRepository = jobCandidatesRepository;
        }

        public async Task Handle(SyncArchivedCandidatesCommand request, CancellationToken cancellationToken)
        {
            foreach (var jobId in request.JobIds)
            {
                try
                {
                    var candidate = await _jobCandidatesRepository.GetAsync(jobId);
                    candidate.PublishArchivedCandidatesUpdatedEvent();

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
