using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class UpdateCandidateBriefCommandHandler : IRequestHandler<UpdateCandidateBriefCommand, JobCandidates>
    {
        private readonly IJobCandidatesRepository _jobCandidatesRepository;

        public UpdateCandidateBriefCommandHandler(IJobCandidatesRepository jobCandidatesRepository)
        {
            _jobCandidatesRepository = jobCandidatesRepository;
        }

        public async Task<JobCandidates> Handle(UpdateCandidateBriefCommand request, CancellationToken cancellationToken)
        {
            var jobCandidates = await _jobCandidatesRepository.GetAsync(request.JobId);

            jobCandidates.UpdateBrief(request.CandidateId, request.Brief);
            
            _jobCandidatesRepository.Update(jobCandidates);
            await _jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>(cancellationToken);

            return jobCandidates;
        }
    }
}
