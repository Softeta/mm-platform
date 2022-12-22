using Contracts.Job.Companies.Requests;
using Contracts.Shared;
using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobAggregate;
using MediatR;

namespace Jobs.Application.Commands;

public record InitializePendingJobCommand(
    CreateJobCompanyRequest Company,
    Position Position,
    DateTimeOffset? StartDate,
    DateTimeOffset? EndDate,
    ICollection<WorkType> WorkTypes,
    bool IsUrgent
    ) : IRequest<Job>;
