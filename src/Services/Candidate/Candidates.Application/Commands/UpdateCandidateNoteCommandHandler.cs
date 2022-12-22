using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects;
using Candidates.Infrastructure.Persistence.Repositories;

namespace Candidates.Application.Commands
{
    public class UpdateCandidateNoteCommandHandler : ModifyCandidateBaseCommandHandler<UpdateCandidateNoteCommand, Note?>
    {
        public UpdateCandidateNoteCommandHandler(ICandidateRepository candidateRepository) : base(candidateRepository)
        {
        }

        protected override async Task<Note?> Handle(UpdateCandidateNoteCommand request, Candidate candidate, CancellationToken cancellationToken)
        {
            candidate.UpdateNote(request.Note, request.EndDate);

            CandidateRepository.Update(candidate);
            await CandidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidate.Note;
        }
    }
}
