using Microsoft.AspNetCore.Mvc;
using PropertyManagementApp.Api.Services;
using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicationsController : ControllerBase
    {
        private readonly ICommunicationLogService _service;

        public CommunicationsController(ICommunicationLogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommunicationLogDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("tenant/{tenantId}")]
        public async Task<ActionResult<List<CommunicationLogDto>>> GetByTenant(int tenantId)
        {
            return Ok(await _service.GetByTenantAsync(tenantId));
        }

        [HttpPost]
        public async Task<ActionResult<CommunicationLogDto>> Create(CreateCommunicationLogDto dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }
    }
}