using Candidates.Infrastructure.Clients.Talogy.Models.Token;

namespace Candidates.Infrastructure.Clients.Talogy.Authorization
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private AuthenticationResult? _authenticationResult;
        private readonly ITalogyAuthApiClient  _talogyAuthApiClient;
       
        public AuthorizationManager(ITalogyAuthApiClient talogyAuthApiClient)
        {
            _talogyAuthApiClient = talogyAuthApiClient;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            await AcquireAccessToken();

            return _authenticationResult!.AccessToken;
        }

        private async Task AcquireAccessToken()
        {
            try
            {
                await _semaphore.WaitAsync();

                if (_authenticationResult is null || _authenticationResult.ExpiresOn < DateTimeOffset.UtcNow.AddSeconds(-30))
                {
                    _authenticationResult = await _talogyAuthApiClient.AuthorizeAsync();

                    if (string.IsNullOrWhiteSpace(_authenticationResult?.AccessToken))
                    {
                        throw new HttpRequestException("No Access token");
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
