namespace API.WebClients.Clients;

public interface IAdministrationSettingsClient
{
    Task<TOut?> GetAsync<TOut>(string endpoint);
}