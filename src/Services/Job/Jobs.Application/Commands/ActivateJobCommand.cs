using MediatR;

namespace Jobs.Application.Commands;

public record ActivateJobCommand(Guid JobId) : IRequest<Guid>;