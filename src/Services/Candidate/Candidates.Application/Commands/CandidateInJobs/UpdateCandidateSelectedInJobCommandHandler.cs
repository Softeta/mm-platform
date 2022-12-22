using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Candidates.Infrastructure.Persistence.Repositories;
using Contracts.Shared.Requests;
using Domain.Seedwork.Exceptions;
using MediatR;
using Persistence.Customization.FileStorage.Clients.Private;
using Persistence.Customization.Queries;
using Persistence.Customization.TableStorage;

namespace Candidates.Application.Commands.CandidateInJobs
{
    public class UpdateCandidateSelectedInJobCommandHandler : IRequestHandler<UpdateCandidateSelectedInJobCommand, CandidateSelectedInJob>
    {
        private readonly ICandidateJobsRepository _candidateJobsRepository;
        private readonly IPrivateFileDeleteClient _privateFileDeleteClient;
        private readonly IMediator _mediator;

        public UpdateCandidateSelectedInJobCommandHandler(
            ICandidateJobsRepository candidateJobsRepository,
            IPrivateFileDeleteClient privateFileDeleteClient,
            IMediator mediator)
        {
            _candidateJobsRepository = candidateJobsRepository;
            _privateFileDeleteClient = privateFileDeleteClient;
            _mediator = mediator;
        }

        public async Task<CandidateSelectedInJob> Handle(UpdateCandidateSelectedInJobCommand request, CancellationToken cancellationToken)
        {
            var candidateJobs = await _candidateJobsRepository.GetAsync(request.CandidateId);
            
            if (candidateJobs is null)
            {
                throw new NotFoundException($"CandidateJobs not found. CandidateId: {request.CandidateId}", 
                    ErrorCodes.NotFound.CandidateJobsNotFound);
            }
           
            var candidateSelectedInJob = candidateJobs.SelectedInJobs.Where(x => x.JobId == request.JobId).FirstOrDefault();
            
            if (candidateSelectedInJob is null)
            {
                throw new NotFoundException("Candidate selected in job is not found. JobId: {request.JobId", 
                    ErrorCodes.NotFound.CandidateSelectedInJobNotFound);
            }

            if (request.MotivationVideo != null)
            {
                var deleteVideoTask = DeleteOldFileAsync(
                    request.MotivationVideo.HasChanged,
                    candidateSelectedInJob.MotivationVideo?.Uri,
                    cancellationToken);
                var videoTask = GetFileAsync(request.MotivationVideo);
                await Task.WhenAll(deleteVideoTask, videoTask);
                var video = videoTask.Result;

                candidateJobs.UpdateCandidateSelectedInJob(
                    request.JobId,
                    video.FileUrl,
                    video.FileName,
                    request.MotivationVideo.HasChanged,
                    request.MotivationVideo.CacheId,
                    request.CoverLetter);
            }
            else
            {
                candidateJobs.UpdateCandidateSelectedInJob(
                    request.JobId,
                    null,
                    null,
                    false,
                    null,
                    request.CoverLetter);
            }

            _candidateJobsRepository.Update(candidateJobs);
            await _candidateJobsRepository.UnitOfWork.SaveEntitiesAsync<CandidateJobs>(cancellationToken);

            return candidateSelectedInJob;
        }

        private async Task DeleteOldFileAsync(bool hasChanged, string? currentFieUrl, CancellationToken token)
        {
            if (hasChanged && !string.IsNullOrWhiteSpace(currentFieUrl))
            {
                await _privateFileDeleteClient.DeleteAsync(currentFieUrl, token);
            }
        }

        private async Task<(string? FileUrl, string? FileName)> GetFileAsync(UpdateFileCacheRequest file)
        {
            if (file.HasChanged)
            {
                var getFileCommand = new GetFileQuery(file.CacheId,
                    FileCacheTableStorage.Candidate.FilePartitionKey);
                var result = await _mediator.Send(getFileCommand);
                return result;
            }
            return (null, null);
        }
    }
}
