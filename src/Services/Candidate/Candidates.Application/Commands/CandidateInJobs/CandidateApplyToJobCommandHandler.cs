using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Candidates.Application.Commands.CandidateInJobs
{
    public class CandidateApplyToJobCommandHandler : INotificationHandler<CandidateApplyToJobCommand>
    {
        private readonly ICandidateJobsRepository _candidateJobsRepository;

        public CandidateApplyToJobCommandHandler(ICandidateJobsRepository candidateJobsRepository)
        {
            _candidateJobsRepository = candidateJobsRepository;
        }

        public async Task Handle(CandidateApplyToJobCommand notification, CancellationToken cancellationToken)
        {
            var candidateJobs = await _candidateJobsRepository.GetAsync(notification.CandidateId);
            
            if (candidateJobs is null)
            {
                candidateJobs = new CandidateJobs(notification.CandidateId);
                ApplyToJob(candidateJobs, notification);
                _candidateJobsRepository.Add(candidateJobs);
            }
            else
            {
                ApplyToJob(candidateJobs, notification);
                _candidateJobsRepository.Update(candidateJobs);
            }

            await _candidateJobsRepository.UnitOfWork.SaveEntitiesAsync<CandidateJobs>(cancellationToken);
        }

        private void ApplyToJob(CandidateJobs candidateJobs, CandidateApplyToJobCommand notification)
        {
            candidateJobs.ApplyToJob(
                notification.JobId,
                notification.JobStage,
                notification.PositionId,
                notification.PositionCode,
                notification.PositionAliasToId,
                notification.PositionAliasToCode,
                notification.CompanyId,
                notification.CompanyName,
                notification.CompanyLogo,
                notification.Freelance,
                notification.Permanent,
                notification.StartDate,
                notification.DeadlineDate);
        }
    }
}
