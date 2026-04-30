using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Security
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthResponseDto CreateAdminToken(string email, string displayName)
        {
            var issuer = _configuration["Jwt:Issuer"] ?? "PropertyManagementApp.Api";
            var audience = _configuration["Jwt:Audience"] ?? "PropertyManagementApp.Client";
            var key = _configuration["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(key) || key.Length < 32)
            {
                throw new InvalidOperationException("JWT key must be configured and must contain at least 32 characters.");
            }

            var lifetimeMinutes = _configuration.GetValue<int?>("Jwt:TokenLifetimeMinutes") ?? 480;
            var expiresAt = DateTime.UtcNow.AddMinutes(lifetimeMinutes);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Name, displayName),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, AppRoles.Admin),
                new Claim("role", AppRoles.Admin)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = email,
                DisplayName = displayName,
                Role = AppRoles.Admin,
                ExpiresAt = expiresAt
            };
        }
    }
}