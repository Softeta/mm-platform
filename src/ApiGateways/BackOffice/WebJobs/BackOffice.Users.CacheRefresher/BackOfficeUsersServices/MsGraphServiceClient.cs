using API.Customization.Authentication;
using API.WebClients.Clients.MsGraphService;
using Azure.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DirectoryObject = Microsoft.Graph.DirectoryObject;
using User = Microsoft.Graph.User;

namespace BackOffice.Users.CacheRefresher.BackOfficeUsersServices
{
    public class MsGraphServiceClient : MsGraphServiceClientBase, IMsGraphServiceClient
    {
        private readonly GraphServiceClient _graphServiceClient;
        private readonly ILogger<MsGraphServiceClient> _logger;
        private const int batchSizeLimit = 20;

        public MsGraphServiceClient(
            IOptions<AppRegistrationSettings> configurations,
            ILogger<MsGraphServiceClient> logger) : base(configurations)
        {
            _graphServiceClient = GetGraphClient();
            _logger = logger;
        }

        public async Task<Dictionary<string, ProfilePhoto>> GetProfilePicturesAsync(string[] userIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var pictures = new Dictionary<string, ProfilePhoto>();
                
                var batchPages = (int)Math.Ceiling((decimal)userIds.Length / batchSizeLimit);

                for (int i = 0; i < batchPages; i++)
                {
                    var userIdsPerPage = userIds.Skip(i * batchSizeLimit).Take(batchSizeLimit).ToList();
                    
                    await GetProfilePicturesPerPageAsync(
                        pictures,
                        _graphServiceClient, 
                        userIdsPerPage,
                        cancellationToken);
                }

                return pictures;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to retrieve back-office user profile pictures");
                throw;
            }
        }

        public async Task<List<User>> GetUsersForGroupsAsync(string[] groupNames, CancellationToken cancellationToken = default)
        {
            try
            {
                var filters = $"'{string.Join("', '", groupNames)}'";
                var groupsResponse = await _graphServiceClient.Groups
                    .Request()
                    .Filter($"displayName in ({filters})")
                    .Select("id")
                    .GetAsync(cancellationToken);

                var groupIds = groupsResponse
                    .Select(x => x.Id)
                    .ToList();

                var groupMembersResponseTasks = groupIds
                    .Select(groupId => CollectUsersFromGroupAsync(_graphServiceClient, groupId, cancellationToken))
                    .ToList();

               return (await Task.WhenAll(groupMembersResponseTasks))
                    .SelectMany(x => x.OfType<User>())
                    .GroupBy(x => new
                    {
                        x.Id,
                        x.GivenName,
                        x.Surname,
                        x.Mail
                    })
                    .Select(x => new User
                    {
                        Id = x.Key.Id,
                        GivenName = x.Key.GivenName,
                        Surname = x.Key.Surname,
                        Mail = x.Key.Mail
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to retrieve back-office users");
                throw;
            }
        }

        private static async Task<List<DirectoryObject>> CollectUsersFromGroupAsync(GraphServiceClient graphClient, string groupId, CancellationToken cancellationToken)
        {
            var groupMembers = new List<DirectoryObject>();

            var membersPage = await graphClient.Groups[groupId].Members
                .Request()
                .Select("id, givenName, surname, mail")
                .GetAsync(cancellationToken);

            groupMembers.AddRange(membersPage.ToList());

            await RequestNextPageAsync(membersPage, groupMembers, cancellationToken);

            return groupMembers;
        }

        private static async Task RequestNextPageAsync(
            IGroupMembersCollectionWithReferencesPage membersPage,
            List<DirectoryObject> groupMembers,
            CancellationToken cancellationToken)
        {
            while (true)
            {
                if (membersPage.NextPageRequest is not null)
                {
                    var nextMembersPage = await membersPage.NextPageRequest.GetAsync(cancellationToken);
                    groupMembers.AddRange(nextMembersPage.ToList());
                    membersPage = nextMembersPage;

                    continue;
                }

                break;
            }
        }

        private async Task GetProfilePicturesPerPageAsync(
            Dictionary<string, ProfilePhoto> pictures, 
            GraphServiceClient graphClient,
            List<string> userIdsPerPage,
            CancellationToken cancellationToken)
        {
            var batch = new BatchRequestContent();          

            foreach (var userId in userIdsPerPage)
            {
                var photoRequestMessage = graphClient.Users[userId].Photo.Content.Request().GetHttpRequestMessage();
                photoRequestMessage.Method = HttpMethod.Get;
                batch.AddBatchRequestStep(new BatchRequestStep(userId, photoRequestMessage));
            }

            var response = await graphClient.Batch
                .Request()
                .PostAsync(batch, cancellationToken);

            foreach (var userId in userIdsPerPage)
            {
                var result = await response.GetResponseByIdAsync(userId);

                if (result is null)
                {
                    _logger.LogCritical("Failed to retrieve back-office user from batch. {userId}", userId);
                    continue;
                }

                if (result.IsSuccessStatusCode)
                {
                    var eTag = result.Headers.ETag?.Tag;
                    var base64String = await result.Content.ReadAsStringAsync(cancellationToken);
                    pictures.Add(userId, new ProfilePhoto(eTag!, Convert.FromBase64String(base64String)));
                }
            }
        }
    }
}
