using Jobs.Application.Queries.Jobs;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class ActivateArchiveCandidatesCommandHandler : IRequestHandler<ActivateArchiveCandidatesCommand, JobCandidates>
    {
        private readonly IJobCandidatesRepository _jobCandidatesRepository;
        private readonly IMediator _mediator;

        public ActivateArchiveCandidatesCommandHandler(IJobCandidatesRepository jobCandidatesRepository, IMediator mediator)
        {
            _jobCandidatesRepository = jobCandidatesRepository;
            _mediator = mediator;
        }

        public async Task<JobCandidates> Handle(ActivateArchiveCandidatesCommand request, CancellationToken cancellationToken)
        {
            var jobCandidates = await _jobCandidatesRepository.GetAsync(request.JobId);

            if (request.CandidateIds.Any())
            {
                var candidates = await GetCandidatesWithInfoInOtherJobAsync(request.CandidateIds, request.JobId);

                jobCandidates.ActivateArchivedCandidates(candidates);
            }

            _jobCandidatesRepository.Update(jobCandidates);
            await _jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>(cancellationToken);

            return jobCandidates;
        }

        private async Task<List<(Guid CandidateId, bool IsShortListedInOtherJob, bool IsHiredInOtherJobs)>> GetCandidatesWithInfoInOtherJobAsync(
            IEnumerable<Guid> newCandidates,
            Guid jobId)
        {
            List<(Guid, bool, bool)> selectedCandidates = new();

            foreach (var candidateId in newCandidates)
            {
                var isShortlistedInOtherJob = await _mediator.Send(
                    new IsCandidateShortlistedInOtherJobQuery(jobId, candidateId)
                );

                var isHiredInOtherJob = await _mediator.Send(
                    new IsCandidateHiredInOtherJobQuery(jobId, candidateId)
                );

                selectedCandidates.Add((candidateId, isShortlistedInOtherJob, isHiredInOtherJob));
            }

            return selectedCandidates;
        }
    }
}
