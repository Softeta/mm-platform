using Jobs.Application.Queries.Jobs;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class ActivateShortlistViaEmailCommandHandler : IRequestHandler<ActivateShortlistViaEmailCommand, JobCandidates>
    {
        private readonly IJobCandidatesRepository _jobCandidatesRepository;
        private readonly IMediator _mediator;

        public ActivateShortlistViaEmailCommandHandler(IJobCandidatesRepository jobCandidatesRepository, IMediator mediator)
        {
            _jobCandidatesRepository = jobCandidatesRepository;
            _mediator = mediator;
        }

        public async Task<JobCandidates> Handle(ActivateShortlistViaEmailCommand request, CancellationToken cancellationToken)
        {
            var jobCandidates = await _jobCandidatesRepository.GetAsync(request.JobId);
            var contactPersonByEmail = await _mediator.Send(
                new GetJobContactPersonByEmailQuery(request.JobId, request.Email));

            jobCandidates.ShareShortlistViaEmail(
                request.Email,
                contactPersonByEmail.FirstName,
                contactPersonByEmail.SystemLanguage,
                contactPersonByEmail.ExternalId);

            _jobCandidatesRepository.Update(jobCandidates);
            await _jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>(cancellationToken);

            return jobCandidates;
        }
    }
}
