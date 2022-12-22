namespace Contracts.Company.Responses
{
    public record GetCompaniesResponse(
        int Count,
        IEnumerable<GetCompanyBriefResponse> Companies);
}
