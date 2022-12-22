using Candidates.Domain.Aggregates.CandidateAggregate;
using MediatR;

namespace Candidates.Application.Commands
{
    public record VerifyCandidateCommand(
        Guid CandidateId,
        Guid Key) : IRequest<Candidate>;
}
