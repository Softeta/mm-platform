using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using API.Customization.HttpContexts;

namespace API.WebClients.HttpMessageHandlers
{
    public class SetBearerTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SetBearerTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var schema = _httpContextAccessor.HttpContext!.User.AuthenticationSchema();
            var accessToken = await _httpContextAccessor.HttpContext!.GetTokenAsync(schema, "access_token");

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentException("No access token found, request to micro service can't be made");
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
