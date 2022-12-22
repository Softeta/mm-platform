using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using MediatR;
using Persistence.Customization.Queries;
using Persistence.Customization.TableStorage;

namespace Candidates.Application.Commands.Courses
{
    public class AddCandidateCourseCommandHandler : ModifyCandidateBaseCommandHandler<AddCandidateCourseCommand, Candidate>
    {
        private readonly IMediator _mediator;

        public AddCandidateCourseCommandHandler(
            ICandidateRepository candidateRepository,
            IMediator mediator)
            : base(candidateRepository)
        {
            _mediator = mediator;
        }

        protected override async Task<Candidate> Handle(AddCandidateCourseCommand request, Candidate candidate, CancellationToken cancellationToken)
        {
            var getCertificateCommand = new GetFileQuery(request.Certificate?.CacheId,
                FileCacheTableStorage.Candidate.FilePartitionKey);
            var certificate = await _mediator.Send(getCertificateCommand);

            candidate.AddCourse(
                request.Name,
                request.IssuingOrganization,
                request.Description,
                certificate.FileUrl,
                certificate.FileName,
                request.Certificate?.CacheId);

            CandidateRepository.Update(candidate);
            await CandidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidate;
        }
    }
}
