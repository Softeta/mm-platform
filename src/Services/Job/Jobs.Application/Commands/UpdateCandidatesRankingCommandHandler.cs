using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class UpdateCandidatesRankingCommandHandler : IRequestHandler<UpdateCandidatesRankingCommand, JobCandidates>
    {
        private readonly IJobCandidatesRepository _jobCandidatesRepository;

        public UpdateCandidatesRankingCommandHandler(IJobCandidatesRepository jobCandidatesRepository)
        {
            _jobCandidatesRepository = jobCandidatesRepository;
        }


        public async Task<JobCandidates> Handle(UpdateCandidatesRankingCommand request, CancellationToken cancellationToken)
        {
            var jobCandidates = await _jobCandidatesRepository.GetAsync(request.JobId);

            var candidateRankings = request.CandidatesRanking.Select(x => (x.Id, x.Ranking));

            jobCandidates.UpdateSelectedCandidatesRanking(candidateRankings);

            _jobCandidatesRepository.Update(jobCandidates);
            await _jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>(cancellationToken);

            return jobCandidates;
        }
    }
}
