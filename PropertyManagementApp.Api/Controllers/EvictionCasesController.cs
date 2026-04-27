using Microsoft.AspNetCore.Mvc;
using PropertyManagementApp.Api.Services;
using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvictionCasesController : ControllerBase
    {
        private readonly IEvictionCaseService _service;

        public EvictionCasesController(IEvictionCaseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<EvictionCaseDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult<EvictionCaseDto>> Create(CreateEvictionCaseDto dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }

        [HttpPost("{id}/generate-notice")]
        public async Task<ActionResult<EvictionCaseDto>> GenerateNotice(int id)
        {
            return Ok(await _service.GenerateNoticeAsync(id));
        }

        [HttpPost("{id}/close")]
        public async Task<ActionResult<EvictionCaseDto>> Close(int id)
        {
            return Ok(await _service.CloseAsync(id));
        }
    }
}