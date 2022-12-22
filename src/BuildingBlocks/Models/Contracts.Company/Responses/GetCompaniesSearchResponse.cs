namespace Contracts.Company.Responses
{
    public record GetCompaniesSearchResponse(
        int Count,
        IEnumerable<GetCompanySearchResponse> Companies, 
        bool ExistsInternally);
}
