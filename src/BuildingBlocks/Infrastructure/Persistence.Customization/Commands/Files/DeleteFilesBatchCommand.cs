using MediatR;
using Persistence.Customization.FileStorage.Clients;

namespace Persistence.Customization.Commands.Files
{
    public record DeleteFilesBatchCommand<TDeleteClient>(List<Uri> Uris)
        : INotification where TDeleteClient : IFileDeleteClient;
}
