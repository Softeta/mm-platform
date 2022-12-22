using Candidates.Application.IntegrationEventHandlers.Subscribers.Candidates.Payload;
using Candidates.Infrastructure.Clients.MicrosoftGraph;
using Candidates.Infrastructure.Persistence.Repositories;
using Domain.Seedwork.Exceptions;
using EventBus.EventHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Persistence.Customization.Commands.Files;
using Persistence.Customization.FileStorage.Clients.Private;
using Persistence.Customization.FileStorage.Clients.Public;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Candidates
{
    public class CandidateRejectedIntegrationEventHandler :
        IntegrationEventHandler,
        IIntegrationEventHandler<CandidateRejectedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public CandidateRejectedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<CandidateRejectedIntegrationEvent>(message);

            if (@event?.Payload is null) return false;

            var candidate = @event.Payload;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var candidateRepository = scope.ServiceProvider.GetRequiredService<ICandidateRepository>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var msGraphServiceClient = scope.ServiceProvider.GetRequiredService<IMsGraphServiceClient>();

            await DeleteUserFromAuthProviderAsync(msGraphServiceClient, candidate.ExternalId);

            var deletePrivateFilesTask = DeleteAllPrivateCandidateFilesAsync(mediator, candidate);
            var deletePublicFilesTask = DeleteAllPublicCandidateFilesAsync(mediator, candidate);
            await Task.WhenAll(deletePrivateFilesTask, deletePublicFilesTask);

            await candidateRepository.RemoveAsync(candidate.Id);
            await candidateRepository.UnitOfWork.SaveEntitiesAsync<Domain.Aggregates.CandidateAggregate.Candidate>();

            return true;
        }

        private async Task DeleteAllPublicCandidateFilesAsync(
            IMediator mediator,
            Candidate candidate)
        {
            var uris = new List<Uri>();

            if (candidate.Picture != null)
            {
                uris.Add(new Uri(candidate.Picture.OriginalUri));
                uris.Add(new Uri(candidate.Picture.ThumbnailUri));
            }
            var command = new DeleteFilesBatchCommand<IPublicFileDeleteClient>(uris);
            await mediator.Publish(command);
        }

        private async Task DeleteAllPrivateCandidateFilesAsync(
            IMediator mediator,
            Candidate candidate)
        {
            var uris = new List<Uri>();

            if (candidate.CurriculumVitae?.Uri != null)
            {
                uris.Add(new Uri(candidate.CurriculumVitae.Uri));
            }
            if (candidate.Video?.Uri != null)
            {
                uris.Add(new Uri(candidate.Video.Uri));
            }

            foreach (var education in candidate.Educations)
            {
                if (education.Certificate?.Uri != null)
                {
                    uris.Add(new Uri(education.Certificate.Uri));
                }
            }
            foreach (var course in candidate.Courses)
            {
                if (course.Certificate?.Uri != null)
                {
                    uris.Add(new Uri(course.Certificate.Uri));
                }
            }
            var command = new DeleteFilesBatchCommand<IPrivateFileDeleteClient>(uris);
            await mediator.Publish(command);
        }

        private async Task DeleteUserFromAuthProviderAsync(
            IMsGraphServiceClient client,
            Guid? externalId)
        {
            if (externalId is null)
            {
                throw new NotFoundException("External id of candidate was not found");
            }

            await client.DeleteUserAsync(externalId.Value);
        }
    }
}
