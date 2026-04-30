using System.Net.Http.Headers;

namespace PropertyManagementApp.Client.Auth
{
    public class JwtAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly AuthTokenStore _tokenStore;

        public JwtAuthorizationMessageHandler(AuthTokenStore tokenStore)
        {
            _tokenStore = tokenStore;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenStore.GetTokenAsync();

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}