using API.WebClients.Constants;
using Contracts.TagSystem;
using Domain.Seedwork.Exceptions;
using Newtonsoft.Json;

namespace API.WebClients.Clients.TagSystem;

public class TagSystemApiClient : ITagSystemApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public TagSystemApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<PagedResult<PositionResponse>> GetSimilarPositions(
        string search, 
        int pageSize = 20, 
        CancellationToken cancellationToken = default)
    {
        var endpoint = string.Format(Endpoints.TagSystem.JobPositionsSimilar, pageSize, search);
        return await GetResponse<PositionResponse>(endpoint, cancellationToken);
    }

    public async Task<PagedResult<SkillResponse>> GetSimilarSkills(
        string search, 
        int pageSize = 20, 
        CancellationToken cancellationToken = default)
    {
        
        var endpoint = string.Format(Endpoints.TagSystem.SkillsSimilar, pageSize, search);
        return await GetResponse<SkillResponse>(endpoint, cancellationToken);
    }

    private async Task<PagedResult<T>> GetResponse<T>(string endpoint, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(ApiClients.TagSystemClient);
        var response = await client.GetAsync(endpoint, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var result = JsonConvert.DeserializeObject<PagedResult<T>>(
                await response.Content.ReadAsStringAsync(cancellationToken)
            );

            if (result is null)
            {
                throw new NotFoundException("Tag system result not found", ErrorCodes.NotFound.TagSystemResultNotFound);
            }

            return result;
        }

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        throw new HttpException(
            $"Tag system API failed to response status code: {response.StatusCode}. Response content: {responseContent}",
            response.StatusCode,
            ErrorCodes.HttpRequest.TagSystemApiException);

    }
}