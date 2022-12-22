using Domain.Seedwork.Exceptions;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class RejectJobCommandHandler : INotificationHandler<RejectJobCommand>
    {
        private readonly IJobRepository _jobRepository;

        public RejectJobCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task Handle(RejectJobCommand notification, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetAsync(notification.JobId);
           
            job.Reject();

            await _jobRepository.RemoveAsync(notification.JobId);
            await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);
        }
    }
}
