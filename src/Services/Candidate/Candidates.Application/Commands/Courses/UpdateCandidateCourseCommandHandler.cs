using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using Contracts.Shared.Requests;
using Domain.Seedwork.Exceptions;
using MediatR;
using Persistence.Customization.FileStorage.Clients.Private;
using Persistence.Customization.Queries;
using Persistence.Customization.TableStorage;

namespace Candidates.Application.Commands.Courses
{
    public class UpdateCandidateCourseCommandHandler : ModifyCandidateBaseCommandHandler<UpdateCandidateCourseCommand, Candidate>
    {
        private readonly IMediator _mediator;
        private readonly IPrivateFileDeleteClient _privateFileDeleteClient;

        public UpdateCandidateCourseCommandHandler(
            ICandidateRepository candidateRepository,
            IMediator mediator,
            IPrivateFileDeleteClient privateFileDeleteClient)
            : base(candidateRepository)
        {
            _mediator = mediator;
            _privateFileDeleteClient = privateFileDeleteClient;
         }

        protected override async Task<Candidate> Handle(UpdateCandidateCourseCommand request, Candidate candidate, CancellationToken cancellationToken)
        {
            var course = candidate.Courses
                .FirstOrDefault(x => x.Id == request.CourseId);

            if (course is null)
            {
                throw new NotFoundException(
                    $"Candidate course not found. CourseId: {request.CourseId}. CandidateId: {request.CandidateId}",
                    ErrorCodes.NotFound.CandidateCourseNotFound);
            }

            var deleteOldFileTask = DeleteOldCertificateAsync(request.Certificate.HasChanged, course.Certificate?.Uri, cancellationToken);
            var getCertificateTask = GetCertificateAsync(request.Certificate);

            await Task.WhenAll(deleteOldFileTask, getCertificateTask);

            var certificate = getCertificateTask.Result;

            candidate.UpdateCourse(
                request.CourseId,
                request.Name,
                request.IssuingOrganization,
                request.Description,
                certificate.FileUrl,
                certificate.FileName,
                request.Certificate.HasChanged,
                request.Certificate.CacheId);

            CandidateRepository.Update(candidate);
            await CandidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidate;
        }

        private async Task DeleteOldCertificateAsync(bool hasChanged, string? currentFieUrl, CancellationToken token)
        {
            if (hasChanged && !string.IsNullOrWhiteSpace(currentFieUrl))
            {
                await _privateFileDeleteClient.DeleteAsync(currentFieUrl, token);
            }
        }

        private async Task<(string? FileUrl, string? FileName)> GetCertificateAsync(UpdateFileCacheRequest certificate)
        {
            if (certificate.HasChanged)
            {
                var getCertificateCommand = new GetFileQuery(certificate.CacheId,
                    FileCacheTableStorage.Candidate.FilePartitionKey);
                return await _mediator.Send(getCertificateCommand);
            }
            return (null, null);
        }
    }
}
