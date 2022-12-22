using API.Customization.Authentication;
using API.WebClients.Clients.MsGraphService;
using Companies.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System.Net;

namespace Companies.Infrastructure.Clients
{
    public class MsGraphServiceClient : MsGraphServiceClientBase, IMsGraphServiceClient
    {
        private readonly GraphServiceClient _graphServiceClient;
        private readonly CompanyB2CExtensionsAppSettings _extensionsAppSettings;
        private readonly ILogger<MsGraphServiceClient> _logger;

        public MsGraphServiceClient(
            IOptions<AppRegistrationSettings> options, 
            IOptions<CompanyB2CExtensionsAppSettings> extensionsAppSettings,
            ILogger<MsGraphServiceClient> logger) : base(options)
        {
            _graphServiceClient = GetGraphClient();
            _extensionsAppSettings = extensionsAppSettings.Value;
            _logger = logger;
        }

        public async Task UpdateUserAttributesAsync(
            Guid objectId, 
            Guid companyId,
            Guid contactId,
            bool isAdmin, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                var extensionsAppApplicationId = _extensionsAppSettings.ApplicationId.Replace("-", string.Empty);

                var customAttributes = new Dictionary<string, object>();
                customAttributes.Add($"extension_{extensionsAppApplicationId}_CompanyId", companyId);
                customAttributes.Add($"extension_{extensionsAppApplicationId}_ContactId", contactId);
                customAttributes.Add($"extension_{extensionsAppApplicationId}_IsAdmin", isAdmin);

                var user = new User
                {
                    AdditionalData = customAttributes
                };

                await _graphServiceClient
                        .Users[objectId.ToString()]
                        .Request()
                        .UpdateAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(
                    ex, 
                    "Exception - Error while updating user attributes. UserId: {objectId}, CompanyId: {CompanyId}, ContactId: {ContactId}", 
                    objectId,
                    companyId,
                    contactId);
            }
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
