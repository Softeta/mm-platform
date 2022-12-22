using API.Customization.Authentication;
using API.WebClients.Clients.MsGraphService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System.Net;

namespace Candidates.Infrastructure.Clients.MicrosoftGraph
{
    public class MsGraphServiceClient : MsGraphServiceClientBase, IMsGraphServiceClient
    {
        private readonly GraphServiceClient _graphServiceClient;
        private readonly ILogger<MsGraphServiceClient> _logger;

        public MsGraphServiceClient(IOptions<AppRegistrationSettings> options, ILogger<MsGraphServiceClient> logger) : base(options)
        {
            _graphServiceClient = GetGraphClient();
            _logger = logger;
        }

        public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            try
            {
                await _graphServiceClient.Users[userId.ToString()].Request().DeleteAsync(cancellationToken);
            }
            catch (ServiceException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogCritical("User {UserId} was not found. Exception: {Ex}", userId, ex.Message);
                }
                else
                {
                    throw new Exception($"User was not deleted. UserId: {userId}", ex);
                }
            }
        }
    }
}
