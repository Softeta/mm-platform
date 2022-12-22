using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using Contracts.Shared.Requests;
using Domain.Seedwork.Exceptions;
using MediatR;
using Persistence.Customization.FileStorage.Clients.Private;
using Persistence.Customization.Queries;
using Persistence.Customization.TableStorage;

namespace Candidates.Application.Commands.Educations
{
    public class UpdateCandidateEducationCommandHandler : ModifyCandidateBaseCommandHandler<UpdateCandidateEducationCommand, Candidate>
    {
        private readonly IMediator _mediator;
        private readonly IPrivateFileDeleteClient _privateFileDeleteClient;

        public UpdateCandidateEducationCommandHandler(
            ICandidateRepository candidateRepository,
            IMediator mediator,
            IPrivateFileDeleteClient privateFileDeleteClient)
            : base(candidateRepository)
        {
            _mediator = mediator;
            _privateFileDeleteClient = privateFileDeleteClient;
        }

        protected override async Task<Candidate> Handle(UpdateCandidateEducationCommand request, Candidate candidate, CancellationToken cancellationToken)
        {
            var education = candidate.Educations
                .FirstOrDefault(x => x.Id == request.EducationId);

            if (education is null)
            {
                throw new NotFoundException(
                    $"Candidate education not found. EducationId: {request.EducationId}. CandidateId: {request.CandidateId}",
                    ErrorCodes.NotFound.CandidateEducationNotFound);
            }

            var deleteOldFileTask = DeleteOldCertificateAsync(request.Certificate.HasChanged, education.Certificate?.Uri, cancellationToken);
            var getCertificateTask = GetCertificateAsync(request.Certificate);
            
            await Task.WhenAll(deleteOldFileTask, getCertificateTask);

            var certificate = getCertificateTask.Result;

            candidate.UpdateEducation(
                request.EducationId,
                request.SchoolName,
                request.Degree,
                request.FieldOfStudy,
                request.From,
                request.To,
                request.StudiesDescription,
                request.IsStillStudying,
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
