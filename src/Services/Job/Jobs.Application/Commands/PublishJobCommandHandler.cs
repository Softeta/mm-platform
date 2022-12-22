using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class PublishJobCommandHandler : INotificationHandler<PublishJobCommand>
    {
        private readonly IJobRepository _jobRepository;

        public PublishJobCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task Handle(PublishJobCommand notification, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetAsync(notification.JobId);
            job.Publish();

            _jobRepository.Update(job);
            await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);
        }
    }
}
