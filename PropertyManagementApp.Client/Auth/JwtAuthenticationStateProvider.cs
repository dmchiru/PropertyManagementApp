using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace PropertyManagementApp.Client.Auth
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly AuthTokenStore _tokenStore;
        private static readonly ClaimsPrincipal AnonymousUser = new(new ClaimsIdentity());

        public JwtAuthenticationStateProvider(AuthTokenStore tokenStore)
        {
            _tokenStore = tokenStore;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _tokenStore.GetTokenAsync();

            if (string.IsNullOrWhiteSpace(token) || IsTokenExpired(token))
            {
                await _tokenStore.ClearAsync();
                return new AuthenticationState(AnonymousUser);
            }

            var identity = new ClaimsIdentity(ParseClaims(token), "jwt");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public void NotifyUserAuthenticationChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void NotifyUserLogout()
        {
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(AnonymousUser)));
        }

        private static bool IsTokenExpired(string token)
        {
            var claims = ParseClaims(token).ToList();
            var exp = claims.FirstOrDefault(c => c.Type == "exp")?.Value;

            if (!long.TryParse(exp, out var seconds))
            {
                return true;
            }

            var expiration = DateTimeOffset.FromUnixTimeSeconds(seconds);
            return expiration <= DateTimeOffset.UtcNow;
        }

        private static IEnumerable<Claim> ParseClaims(string token)
        {
            var payload = token.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes) ?? new();

            foreach (var item in keyValuePairs)
            {
                if (item.Key == "role")
                {
                    yield return new Claim(ClaimTypes.Role, item.Value.ToString() ?? string.Empty);
                }

                yield return new Claim(item.Key, item.Value.ToString() ?? string.Empty);
            }
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            base64 = base64.Replace('-', '+').Replace('_', '/');

            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;

                case 3:
                    base64 += "=";
                    break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}