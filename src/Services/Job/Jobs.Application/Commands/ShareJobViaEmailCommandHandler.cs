using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class ShareJobViaEmailCommandHandler : IRequestHandler<ShareJobViaEmailCommand, DateTimeOffset>
    {
        private readonly IJobRepository _jobRepository;

        public ShareJobViaEmailCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<DateTimeOffset> Handle(ShareJobViaEmailCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetAsync(request.JobId);

            job.ShareViaEmail(request.ReceiverEmail);

            _jobRepository.Update(job);
            await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);

            return job.Sharing!.Date;
        }
    }
}
