using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using Domain.Seedwork.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidates.Application.Commands
{
    internal class RejectCandidateCommandHandler : INotificationHandler<RejectCandidateCommand>
    {
        public readonly ICandidateRepository _candidateRepository;

        public RejectCandidateCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task Handle(RejectCandidateCommand notification, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetAsync(notification.CandidateId);

            candidate.Reject();

            _candidateRepository.Update(candidate);
            await _candidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);
        }
    }
}
