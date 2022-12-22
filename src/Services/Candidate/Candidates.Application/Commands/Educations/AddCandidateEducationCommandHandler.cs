using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using MediatR;
using Persistence.Customization.Queries;
using Persistence.Customization.TableStorage;

namespace Candidates.Application.Commands.Educations
{
    public class AddCandidateEducationCommandHandler : ModifyCandidateBaseCommandHandler<AddCandidateEducationCommand, Candidate>
    {
        private readonly IMediator _mediator;

        public AddCandidateEducationCommandHandler(
            ICandidateRepository candidateRepository,
            IMediator mediator)
            : base(candidateRepository)
        {
            _mediator = mediator;
        }

        protected override async Task<Candidate> Handle(AddCandidateEducationCommand request, Candidate candidate, CancellationToken cancellationToken)
        {
            var getCertificateCommand = new GetFileQuery(request.Certificate?.CacheId,
                FileCacheTableStorage.Candidate.FilePartitionKey);
            var certificate = await _mediator.Send(getCertificateCommand);

            candidate.AddEducation(
                request.SchoolName,
                request.Degree,
                request.FieldOfStudy,
                request.From,
                request.To,
                request.StudiesDescription,
                request.IsStillStudying,
                certificate.FileUrl,
                certificate.FileName,
                request.Certificate?.CacheId);

            CandidateRepository.Update(candidate);
            await CandidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidate;
        }
    }
}
