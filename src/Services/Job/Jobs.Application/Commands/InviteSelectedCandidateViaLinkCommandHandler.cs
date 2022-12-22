using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class InviteSelectedCandidateViaLinkCommandHandler : IRequestHandler<InviteSelectedCandidateViaLinkCommand, JobCandidates>
    {
        private readonly IJobCandidatesRepository _jobCandidatesRepository;

        public InviteSelectedCandidateViaLinkCommandHandler(IJobCandidatesRepository jobCandidatesRepository)
        {
            _jobCandidatesRepository = jobCandidatesRepository;
        }

        public async Task<JobCandidates> Handle(InviteSelectedCandidateViaLinkCommand request, CancellationToken cancellationToken)
        {
            var jobCandidates = await _jobCandidatesRepository.GetAsync(request.JobId);

            jobCandidates.InviteSelectedCandidatesViaLink(request.CandidateIds);

            _jobCandidatesRepository.Update(jobCandidates);
            await _jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>(cancellationToken);

            return jobCandidates;
        }
    }
}
