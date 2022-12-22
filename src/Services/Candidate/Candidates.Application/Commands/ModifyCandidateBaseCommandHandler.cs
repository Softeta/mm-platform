using API.Customization.Authorization.Constants;
using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using Domain.Seedwork.Exceptions;
using MediatR;

namespace Candidates.Application.Commands
{
    public abstract class ModifyCandidateBaseCommandHandler<TCommand, TResponse>
        : IRequestHandler<TCommand, TResponse>
          where TCommand : ModifyCandidateBaseCommand<TResponse>
    {
        protected readonly ICandidateRepository CandidateRepository;

        public ModifyCandidateBaseCommandHandler(ICandidateRepository candidateRepository)
        {
            CandidateRepository = candidateRepository;
        }

        public async Task<TResponse> Handle(
            TCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Scope == CustomScopes.FrontOffice.Company)
            {
                throw new ForbiddenException("Forbidden to edit candidate from the FrontOffice.Company scope.", 
                    ErrorCodes.Forbidden.CandidateEditForbidden);
            }

            var candidate = await CandidateRepository.GetAsync(request.CandidateId);

            if (request.Scope == CustomScopes.FrontOffice.Candidate && !candidate.ExternalId.Equals(request.UserId))
            {
                throw new ForbiddenException("Forbidden to edit candidate from the FrontOffice scope.", 
                    ErrorCodes.Forbidden.CandidateEditForbidden);
            }

            var result = await Handle(request, candidate, cancellationToken);
            return result;
        }

        protected abstract Task<TResponse> Handle(
            TCommand request,
            Candidate candidate,
            CancellationToken cancellationToken
        );
    }
}
