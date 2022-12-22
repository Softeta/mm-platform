using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class StartJobSearchAndSelectionCommandHandler : IRequestHandler<StartJobSearchAndSelectionCommand, JobStage>
    {
        private readonly IJobRepository _jobRepository;

        public StartJobSearchAndSelectionCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<JobStage> Handle(StartJobSearchAndSelectionCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetAsync(request.JobId);
            job.StartSearchAndSelection();

            _jobRepository.Update(job);
            await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);

            return job.Stage;
        }
    }
}
