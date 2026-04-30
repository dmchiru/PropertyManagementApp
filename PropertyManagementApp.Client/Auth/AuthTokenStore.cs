using Microsoft.JSInterop;
using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Client.Auth
{
    public class AuthTokenStore
    {
        private const string TokenKey = "propertyManagement.auth.token";
        private const string EmailKey = "propertyManagement.auth.email";
        private const string DisplayNameKey = "propertyManagement.auth.displayName";
        private const string RoleKey = "propertyManagement.auth.role";
        private const string ExpiresAtKey = "propertyManagement.auth.expiresAt";

        private readonly IJSRuntime _js;

        public AuthTokenStore(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SaveAsync(AuthResponseDto response)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", TokenKey, response.Token);
            await _js.InvokeVoidAsync("localStorage.setItem", EmailKey, response.Email);
            await _js.InvokeVoidAsync("localStorage.setItem", DisplayNameKey, response.DisplayName);
            await _js.InvokeVoidAsync("localStorage.setItem", RoleKey, response.Role);
            await _js.InvokeVoidAsync("localStorage.setItem", ExpiresAtKey, response.ExpiresAt.ToString("O"));
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _js.InvokeAsync<string?>("localStorage.getItem", TokenKey);
        }

        public async Task ClearAsync()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", TokenKey);
            await _js.InvokeVoidAsync("localStorage.removeItem", EmailKey);
            await _js.InvokeVoidAsync("localStorage.removeItem", DisplayNameKey);
            await _js.InvokeVoidAsync("localStorage.removeItem", RoleKey);
            await _js.InvokeVoidAsync("localStorage.removeItem", ExpiresAtKey);
        }
    }
}
