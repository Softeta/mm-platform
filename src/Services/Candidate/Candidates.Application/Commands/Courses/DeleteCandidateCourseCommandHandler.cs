using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using Domain.Seedwork.Exceptions;
using Persistence.Customization.FileStorage.Clients.Private;

namespace Candidates.Application.Commands.Courses
{
    public class DeleteCandidateCourseCommandHandler : ModifyCandidateBaseCommandHandler<DeleteCandidateCourseCommand, Candidate>
    {
        private readonly IPrivateFileDeleteClient _privateFileDeleteClient;

        public DeleteCandidateCourseCommandHandler(
            ICandidateRepository candidateRepository,
            IPrivateFileDeleteClient privateFileDeleteClient)
            : base(candidateRepository)
        {
            _privateFileDeleteClient = privateFileDeleteClient;
        }

        protected override async Task<Candidate> Handle(DeleteCandidateCourseCommand request, Candidate candidate, CancellationToken cancellationToken)
        {
            var course = candidate.Courses
                .FirstOrDefault(x => x.Id == request.CourseId);

            if (course is null)
            {
                throw new NotFoundException(
                    $"Candidate course not found. CourseId: {request.CourseId}. CandidateId: {request.CandidateId}",
                    ErrorCodes.NotFound.CandidateCourseNotFound);
            }

            if (course.Certificate?.Uri != null)
            {
                await _privateFileDeleteClient.DeleteAsync(
                    course.Certificate.Uri,
                    cancellationToken);
            }

            candidate.DeleteCourse(request.CourseId);

            CandidateRepository.Update(candidate);
            await CandidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidate;
        }
    }
}
