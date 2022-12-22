using API.WebClients.Clients.HereSearch.Models;

namespace API.WebClients.Clients.HereSearch
{
    public interface ILocationProvider
    {
        Task<AddressDetails> GetAddressDetailsAsync(string address);
    }
}
