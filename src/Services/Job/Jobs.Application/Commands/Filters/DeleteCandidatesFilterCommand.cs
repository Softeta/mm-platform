using MediatR;

namespace Jobs.Application.Commands.Filters;

public record DeleteCandidatesFilterCommand(Guid JobId, Guid UserId, int Index) : IRequest;