using Domain.Seedwork.Enums;
using MediatR;

namespace Jobs.Application.Commands
{
    public record ArchiveJobCommand(Guid JobId, JobStage Stage) : IRequest;
}