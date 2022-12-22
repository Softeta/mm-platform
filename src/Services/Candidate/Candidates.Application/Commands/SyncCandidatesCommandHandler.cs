using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Candidates.Application.Commands
{
    public class SyncCandidatesCommandHandler : INotificationHandler<SyncCandidatesCommand>
    {
        private readonly ICandidateRepository _candidateRepository;

        public SyncCandidatesCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task Handle(SyncCandidatesCommand request, CancellationToken cancellationToken)
        {
            foreach (var candidateId in request.CandidateIds)
            {
                try
                {
                    var candidate = await _candidateRepository.GetAsync(candidateId);
                    candidate.PublishCandidateUpdatedEvent();

                    _candidateRepository.Update(candidate);
                }
                catch
                {
                    // Ignored
                }
            }

            await _candidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);
        }
    }
}
