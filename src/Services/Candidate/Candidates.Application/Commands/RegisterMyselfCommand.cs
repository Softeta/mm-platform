using Candidates.Domain.Aggregates.CandidateAggregate;
using Domain.Seedwork.Enums;
using MediatR;

namespace Candidates.Application.Commands
{
    public record RegisterMyselfCommand(
        string Email,
        Guid ExternalId,
        SystemLanguage? SystemLanguage,
        bool AcceptTermsAndConditions,
        bool AcceptMarketingAcknowledgement
    ) : IRequest<Candidate>;
}
