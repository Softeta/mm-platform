namespace API.WebClients.Clients
{
    public interface IElasticSearchWebApiClient
    {
        Task<TOut?> GetAsync<TOut>(string endpoint);

        Task<TOut?> PostAsync<TIn, TOut>(TIn payload, string endpoint) where TIn : class;
    }
}
