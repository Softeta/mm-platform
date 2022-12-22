using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class UpdateCandidatesStageCommandHandler : IRequestHandler<UpdateCandidatesStageCommand, JobCandidates>
    {
        private readonly IJobCandidatesRepository _jobCandidatesRepository;

        public UpdateCandidatesStageCommandHandler(IJobCandidatesRepository jobCandidatesRepository)
        {
            _jobCandidatesRepository = jobCandidatesRepository;
        }

        public async Task<JobCandidates> Handle(UpdateCandidatesStageCommand request, CancellationToken cancellationToken)
        {
            var jobCandidates = await _jobCandidatesRepository.GetAsync(request.JobId);

            if (!request.CandidateIds.Any())
            {
                return jobCandidates;
            }

            if(request.Stage == SelectedCandidateStage.Hired)
            {
                jobCandidates.HireSelectedCandidates(request.CandidateIds);
            }
            else
            {
                jobCandidates.UpdateSelectedCandidates(request.CandidateIds, request.Stage);
            }

            _jobCandidatesRepository.Update(jobCandidates);
            await _jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>(cancellationToken);

            return jobCandidates;
        }
    }
}
