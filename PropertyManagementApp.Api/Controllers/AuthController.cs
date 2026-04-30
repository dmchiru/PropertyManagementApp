using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyManagementApp.Api.Security;
using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IConfiguration configuration, IJwtTokenService jwtTokenService)
        {
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<AuthResponseDto> Login(AuthLoginRequestDto request)
        {
            var adminEmail = _configuration["AdminUser:Email"];
            var adminPassword = _configuration["AdminUser:Password"];
            var adminName = _configuration["AdminUser:Name"] ?? "Administrator";

            if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Admin credentials are not configured."
                });
            }

            var isEmailValid = string.Equals(request.Email, adminEmail, StringComparison.OrdinalIgnoreCase);
            var isPasswordValid = request.Password == adminPassword;

            if (!isEmailValid || !isPasswordValid)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }

            return Ok(_jwtTokenService.CreateAdminToken(adminEmail, adminName));
        }
    }
}