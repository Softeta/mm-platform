using Azure.Data.Tables;
using BackOffice.Shared.Constants;
using Persistence.Customization.TableStorage.Clients;

namespace BackOffice.Bff.API.Services
{
    public class HostedServices : IHostedService
    {
        private readonly ILogger<HostedServices> _logger;
        private readonly IWebTableServiceClient _tableServiceClient;

        public HostedServices(
            IWebTableServiceClient tableServiceClient,
            ILogger<HostedServices> logger)
        {
            _logger = logger;
            _tableServiceClient = tableServiceClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Back-Office API hosted services");
            await _tableServiceClient.CreateTableIfNotExistsAsync(TableStorageConstants.BackOfficeUsersTable, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
