using MediatR;

namespace Companies.Application.Commands.RegistryCenter
{
    public record SyncDanishCompaniesCommand() : INotification;
}
