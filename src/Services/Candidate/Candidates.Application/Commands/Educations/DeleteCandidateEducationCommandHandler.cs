using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using Domain.Seedwork.Exceptions;
using Persistence.Customization.FileStorage.Clients.Private;

namespace Candidates.Application.Commands.Educations
{
    public class DeleteCandidateEducationCommandHandler : ModifyCandidateBaseCommandHandler<DeleteCandidateEducationCommand, Candidate>
    {
        private readonly IPrivateFileDeleteClient _privateFileDeleteClient;

        public DeleteCandidateEducationCommandHandler(
            ICandidateRepository candidateRepository,
            IPrivateFileDeleteClient privateFileDeleteClient)
            : base(candidateRepository)
        {
            _privateFileDeleteClient = privateFileDeleteClient;
        }

        protected override async Task<Candidate> Handle(DeleteCandidateEducationCommand request, Candidate candidate, CancellationToken cancellationToken)
        {
            var education = candidate.Educations
                .FirstOrDefault(x => x.Id == request.EducationId);

            if (education is null)
            {
                throw new NotFoundException(
                    $"Candidate education not found. EducationId: {request.EducationId}. CandidateId: {request.CandidateId}",
                    ErrorCodes.NotFound.CandidateEducationNotFound);
            }

            if (education.Certificate?.Uri != null)
            {
                await _privateFileDeleteClient.DeleteAsync(
                    education.Certificate.Uri,
                    cancellationToken);
            }

            candidate.DeleteEducation(request.EducationId);

            CandidateRepository.Update(candidate);
            await CandidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidate;
        }
    }
}
