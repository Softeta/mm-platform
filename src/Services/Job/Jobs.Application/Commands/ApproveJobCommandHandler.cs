using Domain.Seedwork.Exceptions;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    internal class ApproveJobCommandHandler : INotificationHandler<ApproveJobCommand>
    {
        public readonly IJobRepository _jobRepository;

        public ApproveJobCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task Handle(ApproveJobCommand notification, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetAsync(notification.JobId);

            job.Approve();

            _jobRepository.Update(job);
            await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);

        }
    }
}
