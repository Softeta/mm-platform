using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands.Filters;

public class DeleteCandidatesFilterCommandHandler : IRequestHandler<DeleteCandidatesFilterCommand>
{
    private readonly IJobCandidatesFilterRepository _candidatesFilterRepository;

    public DeleteCandidatesFilterCommandHandler(IJobCandidatesFilterRepository candidatesFilterRepository)
    {
        _candidatesFilterRepository = candidatesFilterRepository;
    }

    public async Task<Unit> Handle(DeleteCandidatesFilterCommand request, CancellationToken cancellationToken)
    {
        await _candidatesFilterRepository.DeleteAsync(request.JobId, request.UserId, request.Index);
        return Unit.Value;
    }
}