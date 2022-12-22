using BackOffice.Shared.Entities;
using BackOffice.Shared.Queries;
using BackOffice.Users.CacheRefresher.BackOfficeUsersServices;
using BackOffice.Users.CacheRefresher.Commands;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BackOffice.Users.CacheRefresher
{
    public class BackOfficeUserProfilePicturesSyncFunction
    {
        private readonly IMsGraphServiceClient _msGraphServiceClient;
        private readonly IServiceProvider _serviceProvider;

        public BackOfficeUserProfilePicturesSyncFunction(
            IMsGraphServiceClient msGraphServiceClient,
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _msGraphServiceClient = msGraphServiceClient;
        }

        [FunctionName(nameof(BackOfficeUserProfilePicturesSyncFunction))]
        public async Task Run([OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var userIds = context.GetInput<string[]>();

            if (userIds?.Length is null or 0)
            {
                return;
            }

            await using var scope = _serviceProvider.CreateAsyncScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var profilePicturesTask = _msGraphServiceClient.GetProfilePicturesAsync(userIds);
            var cachedUsers = await mediator.Send(new GetCachedBackOfficeUsersQuery());

            var profilePhotosSyncTasks = new Collection<Task>();

            foreach (var (userId, profilePhoto) in await profilePicturesTask)
            {
                var cachedUser = cachedUsers.FirstOrDefault(x => x.RowKey == userId);

                if (cachedUser is null)
                {
                    return;
                }

                profilePhotosSyncTasks.Add(
                    UpdateProfilePhotoAsync(
                        cachedUser,
                        profilePhoto, mediator));
            }

            await Task.WhenAll(profilePhotosSyncTasks);
        }

        private static async Task UpdateProfilePhotoAsync(
            BackOfficeUserEntity cachedUser,
            ProfilePhoto profilePhoto,
            IPublisher mediator)
        {
            if (profilePhoto.ETag == cachedUser.PictureETag)
            {
                return;
            }

            await Task.WhenAll(
                mediator.Publish(new DeleteProfilePhotoCommand(cachedUser.PictureUri)),
                mediator.Publish(new UploadProfilePhotoCommand(cachedUser, profilePhoto)));
        }
    }
}
