namespace Candidates.Infrastructure.Clients.Talogy
{
    public interface ITalogyApiClient
    {
        Task<TOut?> GetAsync<TOut>(string endpoint);
        Task<Stream?> GetFileAsync(string endpoint, string accept);
        Task<TOut?> PostAsync<TIn, TOut>(TIn payload, string endpoint) where TIn : class;
        Task DeleteAsync(string endpoint);
    }
}
