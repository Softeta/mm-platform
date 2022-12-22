using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using Domain.Seedwork.Exceptions;
using MediatR;

namespace Candidates.Application.Commands
{
    public class ApproveCandidateCommandHandler : INotificationHandler<ApproveCandidateCommand>
    {
        public readonly ICandidateRepository _candidateRepository;

        public ApproveCandidateCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task Handle(ApproveCandidateCommand notification, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetAsync(notification.CandidateId);

            candidate.Approve();

            _candidateRepository.Update(candidate);
            await _candidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

        }
    }
}
