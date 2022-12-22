using Candidates.Infrastructure.Clients.Talogy.Models.Token;

namespace Candidates.Infrastructure.Clients.Talogy.Authorization
{
    public interface ITalogyAuthApiClient
    {
        Task<AuthenticationResult?> AuthorizeAsync();
    }
}
