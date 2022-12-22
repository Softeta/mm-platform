using Contracts.Job.Jobs.Responses;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class ShareJobViaLinkCommandHandler : IRequestHandler<ShareJobViaLinkCommand, ShareJobViaLinkResponse>
    {
        private readonly IJobRepository _jobRepository;

        public ShareJobViaLinkCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<ShareJobViaLinkResponse> Handle(ShareJobViaLinkCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetAsync(request.JobId);

            job.ShareViaLink();

            _jobRepository.Update(job);
            await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);

            return new ShareJobViaLinkResponse
            {
                Key = job.Sharing!.Key,
                Date = job.Sharing!.Date
            };
        }
    }
}
