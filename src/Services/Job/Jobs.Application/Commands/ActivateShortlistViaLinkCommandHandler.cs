using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class ActivateShortlistViaLinkCommandHandler : IRequestHandler<ActivateShortlistViaLinkCommand, JobCandidates>
    {
        private readonly IJobCandidatesRepository _jobCandidatesRepository;

        public ActivateShortlistViaLinkCommandHandler(IJobCandidatesRepository jobCandidatesRepository)
        {
            _jobCandidatesRepository = jobCandidatesRepository;
        }

        public async Task<JobCandidates> Handle(ActivateShortlistViaLinkCommand request, CancellationToken cancellationToken)
        {
            var jobCandidates = await _jobCandidatesRepository.GetAsync(request.JobId);

            jobCandidates.ShareShortlistViaLink();

            _jobCandidatesRepository.Update(jobCandidates);
            await _jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>(cancellationToken);

            return jobCandidates;
        }
    }
}
