using API.WebClients.Clients.DanishCompaniesService.Models;

namespace API.WebClients.Clients.DanishCompaniesService
{
    public interface IDasnishCvrWebApiClient
    {
        Task<CrvServiceResponse?> GetAsync(int pageSize, string search, string[]? searchAfter);
    }
}
