namespace API.WebClients.Clients
{
    public interface ICandidateWebApiClient
    {
        Task<TOut?> PutAsync<TIn, TOut>(TIn payload, string endpoint) where TIn : class;

        Task<TOut?> PutAsync<TOut>(string endpoint);

        Task<TOut?> PostAsync<TIn, TOut>(TIn payload, string endpoint) where TIn : class;

        Task<TOut?> GetAsync<TOut>(string endpoint);

        Task<TOut?> DeleteAsync<TOut>(string endpoint);
    }
}
