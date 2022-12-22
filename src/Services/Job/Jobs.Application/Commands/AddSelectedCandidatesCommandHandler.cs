using Contracts.Job.SelectedCandidates.Requests;
using Jobs.Application.Queries.Jobs;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;
using DomainEntities = Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;

namespace Jobs.Application.Commands
{
    public class AddSelectedCandidatesCommandHandler : IRequestHandler<AddSelectedCandidatesCommand, JobCandidates>
    {
        private readonly IJobCandidatesRepository _jobCandidatesRepository;
        private readonly IMediator _mediator;

        public AddSelectedCandidatesCommandHandler(IJobCandidatesRepository jobCandidatesRepository, IMediator mediator)
        {
            _jobCandidatesRepository = jobCandidatesRepository;
            _mediator = mediator;
        }
        public async Task<JobCandidates> Handle(AddSelectedCandidatesCommand request, CancellationToken cancellationToken)
        {
            var jobCandidates = await _jobCandidatesRepository.GetAsync(request.JobId);

            if (request.SelectedCandidates.Any())
            {
                var selectedCandidates = await GetSelectedCandidatesAsync(request.SelectedCandidates, request.JobId);
                jobCandidates.AddSelectedCandidates(selectedCandidates.Select(a =>
                    new DomainEntities.JobSelectedCandidate(
                        request.JobId,
                        a.SelectedCandidate.Id,
                        a.SelectedCandidate.FirstName,
                        a.SelectedCandidate.LastName,
                        a.SelectedCandidate.Email,
                        a.SelectedCandidate.PhoneNumber,
                        a.SelectedCandidate.Picture?.Uri,
                        a.SelectedCandidate.Position?.Id,
                        a.SelectedCandidate.Position?.Code,
                        a.SelectedCandidate.Position?.AliasTo?.Id,
                        a.SelectedCandidate.Position?.AliasTo?.Code,
                        a.SelectedCandidate.SystemLanguage,
                        a.IsShortListedInOtherJob,
                        a.isHiredInOtherJob,
                        null,
                        null,
                        false)));

                _jobCandidatesRepository.Update(jobCandidates);
                await _jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>(cancellationToken);
            }

            return jobCandidates;
        }

        private async Task<List<JobSelectedCandidateExtendedRequest>> GetSelectedCandidatesAsync(
            IEnumerable<JobSelectedCandidateRequest> newCandidates,
            Guid jobId)
        {
            List <JobSelectedCandidateExtendedRequest> selectedCandidates = new();

            foreach (var candidate in newCandidates)
            {
                var isShortlistedInOtherJob = await _mediator.Send(
                    new IsCandidateShortlistedInOtherJobQuery(jobId, candidate.Id)
                );

                var isHiredInOtherJob = await _mediator.Send(
                    new IsCandidateHiredInOtherJobQuery(jobId, candidate.Id)
                );

                selectedCandidates.Add(new JobSelectedCandidateExtendedRequest
                {
                    SelectedCandidate = candidate,
                    isHiredInOtherJob = isHiredInOtherJob,
                    IsShortListedInOtherJob = isShortlistedInOtherJob
                });
            }

            return selectedCandidates;
        }
    }
}
