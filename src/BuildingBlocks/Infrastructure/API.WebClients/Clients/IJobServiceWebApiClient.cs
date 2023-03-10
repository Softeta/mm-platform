namespace API.WebClients.Clients
{
    public interface IJobServiceWebApiClient
    {
        Task<TOut?> PutAsync<TIn, TOut>(TIn payload, string endpoint) where TIn : class;

        Task<TOut?> PostAsync<TIn, TOut>(TIn payload, string endpoint) where TIn : class;

        Task<TOut?> GetAsync<TOut>(string endpoint);
    }
}
