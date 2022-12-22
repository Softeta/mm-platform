using Candidates.Application.NotificationHandlers;
using Candidates.Infrastructure.Clients.Talogy;
using Candidates.Infrastructure.Clients.Talogy.Models.Responses.PackageInstance;
using Candidates.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.Customization.FileStorage;
using Persistence.Customization.FileStorage.Clients.Private;

namespace Candidates.Application.Commands.Tests
{
    public class BaseSaveTestRaportsCommandHandler : BaseHandler
    {
        private readonly IPrivateFileUploadClient _privateFileUploadClient;
        private readonly BlobContainerSettings _blobContainerSettings;
        private readonly ILogger<BaseSaveTestRaportsCommandHandler> _logger;

        public BaseSaveTestRaportsCommandHandler(
            ITalogyApiClient talogyApiClient,
            IPrivateFileUploadClient privateFileUploadClient,
            IOptions<BlobContainerSettings> blobContainerSettings,
            ILogger<BaseSaveTestRaportsCommandHandler> logger) : base(talogyApiClient)
        {
            _privateFileUploadClient = privateFileUploadClient;
            _blobContainerSettings = blobContainerSettings.Value;
            _logger = logger;
        }

        protected async Task<string?> SaveRaportAsync(ReportInstance raport, ReportType raportType)
        {
            try
            {
                var fileFomat = raportType.FileFormat;
                var file = await GetReportAsync(raport.ReportInstanceId, fileFomat);
                if (file is null)
                {
                    return null;
                }

                var path = await _privateFileUploadClient.ExecuteAsync(
                    file,
                    TakeExtension(fileFomat),
                    _blobContainerSettings.CandidateTestRaports);

                if (string.IsNullOrWhiteSpace(path)) return null;

                return path;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve raport: {Payload}", raport);
                return null;
            }
        }

        private static string TakeExtension(string format) => $".{format.Split('/').Last()}";
    }
}
