using Contracts.TagSystem;

namespace API.WebClients.Clients.TagSystem;

public interface ITagSystemApiClient
{
    Task<PagedResult<PositionResponse>> GetSimilarPositions(string search, int pageSize = 20, CancellationToken cancellationToken = default);

    Task<PagedResult<SkillResponse>> GetSimilarSkills(string search, int pageSize = 20, CancellationToken cancellationToken = default);
}