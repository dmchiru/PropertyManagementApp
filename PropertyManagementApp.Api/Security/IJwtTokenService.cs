using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Security
{
    public interface IJwtTokenService
    {
        AuthResponseDto CreateAdminToken(string email, string displayName);
    }
}
