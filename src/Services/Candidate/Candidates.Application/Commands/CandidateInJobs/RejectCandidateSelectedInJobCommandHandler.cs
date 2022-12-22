using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using Domain.Seedwork.Exceptions;
using MediatR;

namespace Candidates.Application.Commands.CandidateInJobs
{
    public class RejectCandidateSelectedInJobCommandHandler : INotificationHandler<RejectCandidateSelectedInJobCommand>
    {
        private readonly ICandidateJobsRepository _candidateJobsRepository;

        public RejectCandidateSelectedInJobCommandHandler(ICandidateJobsRepository candidateJobsRepository)
        {
            _candidateJobsRepository = candidateJobsRepository;
        }

        public async Task Handle(RejectCandidateSelectedInJobCommand notification, CancellationToken cancellationToken)
        {
            var candidateJobs = await _candidateJobsRepository.GetAsync(notification.CandidateId);
            
            if (candidateJobs is null)
            {
                throw new NotFoundException($"CandidateJobs not found. CandidateId: {notification.CandidateId}",
                    ErrorCodes.NotFound.CandidateJobsNotFound);
            }

            candidateJobs.RejectJob(notification.JobId);

            _candidateJobsRepository.Update(candidateJobs);
            await _candidateJobsRepository.UnitOfWork.SaveEntitiesAsync<CandidateJobs>(cancellationToken);
        }
    }
}
