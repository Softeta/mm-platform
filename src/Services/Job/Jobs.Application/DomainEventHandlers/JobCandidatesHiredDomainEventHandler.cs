﻿using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Domain.Events.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.DomainEventHandlers
{
    internal class JobCandidatesHiredDomainEventHandler : INotificationHandler<JobCandidatesHiredDomainEvent>
    {
        private readonly IJobRepository _jobRepository;

        public JobCandidatesHiredDomainEventHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task Handle(JobCandidatesHiredDomainEvent notification, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetAsync(notification.JobCandidates.Id);

            job.Hire();
            _jobRepository.Update(job);

            await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);
        }
    }
}
