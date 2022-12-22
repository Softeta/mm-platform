using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands;

public class ActivateJobCommandHandler : IRequestHandler<ActivateJobCommand, Guid>
{
    private readonly IJobRepository _jobRepository;

    public ActivateJobCommandHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<Guid> Handle(ActivateJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetAsync(request.JobId);
        job.Activate();

        _jobRepository.Update(job);
        await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);

        var jobCopy = new Job();
        jobCopy.CreateFromJob(job);

        _jobRepository.Add(jobCopy);
        await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);

        return jobCopy.Id;
    }
}