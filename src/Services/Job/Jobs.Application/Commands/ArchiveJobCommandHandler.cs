using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands;

public class ArchiveJobCommandHandler : IRequestHandler<ArchiveJobCommand>
{
    private readonly IJobRepository _jobRepository;

    public ArchiveJobCommandHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<Unit> Handle(ArchiveJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetAsync(request.JobId);
        job.Archive(request.Stage);

        _jobRepository.Update(job);
        await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);

        return Unit.Value;
    }
}