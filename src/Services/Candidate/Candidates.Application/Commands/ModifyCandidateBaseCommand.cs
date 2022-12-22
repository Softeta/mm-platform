using MediatR;

namespace Candidates.Application.Commands
{
    public abstract record ModifyCandidateBaseCommand<TResponse>(
        Guid CandidateId,
        Guid UserId,
        string Scope
    ): IRequest<TResponse>;
}
