using BackOffice.Shared.Entities;
using BackOffice.Shared.Queries;
using BackOffice.Users.CacheRefresher.BackOfficeUsersServices;
using BackOffice.Users.CacheRefresher.Commands;
using BackOffice.Users.CacheRefresher.Constants;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackOffice.Users.CacheRefresher
{
    public class BackOfficeUsersCacheRefreshFunction
    {
        private readonly ILogger<BackOfficeUsersCacheRefreshFunction> _logger;
        private readonly IMsGraphServiceClient _msGraphServiceClient;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public BackOfficeUsersCacheRefreshFunction(
            ILogger<BackOfficeUsersCacheRefreshFunction> logger,
            IMsGraphServiceClient msGraphServiceClient,
            IConfiguration configuration, 
            IMediator mediator)
        {
            _msGraphServiceClient = msGraphServiceClient;
            _configuration = configuration;
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName(nameof(BackOfficeUsersCacheRefreshFunction))]
        public async Task Run(
            [TimerTrigger("0 */1 * * *")] TimerInfo myTimer,
            [DurableClient] IDurableOrchestrationClient orchestrationClient,
            CancellationToken cancellationToken)
        {
            var groupNames = JsonConvert.DeserializeObject<string[]>(_configuration[AppSettingNames.GroupNames]);
            
            if (groupNames?.Length is 0 or null)
            {
                _logger.LogCritical($"Empty or can not resolve {AppSettingNames.GroupNames} App setting value");
            }

            var azureAdUsersTask = _msGraphServiceClient.GetUsersForGroupsAsync(groupNames!, cancellationToken);

            var cachedUsers = await _mediator.Send(new GetCachedBackOfficeUsersQuery(), cancellationToken);
            var backOfficeUsers = (await azureAdUsersTask)
                .Select(BackOfficeUserEntity.FromAdUser)
                .ToList();

            var removableUsers = cachedUsers
                .Where(cachedUser => backOfficeUsers
                    .Any(backOfficeUser => backOfficeUser.RowKey == cachedUser.RowKey) == false)
                .ToList();

            var usersToSync = backOfficeUsers
                .Where(x =>
                    !string.IsNullOrWhiteSpace(x.FirstName) &&
                    !string.IsNullOrWhiteSpace(x.LastName))
                .ToList();

            await Task.WhenAll(
                _mediator.Publish(
                    new AddCachedUsersCommand(
                        usersToSync,
                        cachedUsers),
                    cancellationToken),
                _mediator.Publish(
                    new UpdateCachedUsersCommand(
                        usersToSync,
                        cachedUsers),
                    cancellationToken),
                _mediator.Publish(
                    new DeleteCachedUsersCommand(
                        removableUsers),
                    cancellationToken));

            await orchestrationClient.StartNewAsync(
                nameof(BackOfficeUserProfilePicturesSyncFunction),
                usersToSync.Select(x => x.RowKey));

            _logger.LogInformation($"{removableUsers.Count} back-office users deleted. {usersToSync.Count} created or updated out of {backOfficeUsers.Count}");
            InformAboutNotPreparedUsers(usersToSync, backOfficeUsers);
        }

        private void InformAboutNotPreparedUsers(
            ICollection<BackOfficeUserEntity> usersToSync,
            ICollection<BackOfficeUserEntity> backOfficeUsers)
        {
            if (usersToSync.Count == backOfficeUsers.Count)
            {
                return;
            }

            //  For now we will log a critical message, but later on we should think about functionality such as sending an email to admin
            var notPreparedUsers = backOfficeUsers
                .Where(x =>
                    string.IsNullOrWhiteSpace(x.FirstName) || string.IsNullOrWhiteSpace(x.LastName))
                .ToList();

            _logger.LogCritical("Not prepared users: {Users}", notPreparedUsers);
        }
    }
}
