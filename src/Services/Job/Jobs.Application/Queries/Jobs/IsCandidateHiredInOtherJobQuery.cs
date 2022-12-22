using MediatR;

namespace Jobs.Application.Queries.Jobs;

public record IsCandidateHiredInOtherJobQuery(Guid JobId, Guid CandidateId)
    : IRequest<bool>;