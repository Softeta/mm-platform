namespace Candidates.Infrastructure.Clients.Talogy.Authorization
{
    public interface IAuthorizationManager
    {
        Task<string> GetAccessTokenAsync();
    }
}
