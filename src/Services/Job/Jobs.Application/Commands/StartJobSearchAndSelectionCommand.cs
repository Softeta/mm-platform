using Domain.Seedwork.Enums;
using MediatR;

namespace Jobs.Application.Commands
{
    public record StartJobSearchAndSelectionCommand(Guid JobId) : IRequest<JobStage>;
}
