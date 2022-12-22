using Candidates.Domain.Aggregates.CandidateAggregate;
using MediatR;

namespace Candidates.Application.Commands
{
    public record RequestEmailVerificationCommand(Guid ExternalId) : IRequest<Candidate?>;
}
