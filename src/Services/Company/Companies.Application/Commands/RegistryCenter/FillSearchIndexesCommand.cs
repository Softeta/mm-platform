using MediatR;

namespace Companies.Application.Commands.RegistryCenter
{
    public record FillSearchIndexesCommand() : INotification;
}
