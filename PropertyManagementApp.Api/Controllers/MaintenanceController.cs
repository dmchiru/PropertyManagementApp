using Microsoft.AspNetCore.Mvc;
using PropertyManagementApp.Api.Services;

namespace PropertyManagementApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _service;

        public MaintenanceController(IMaintenanceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost("{id}/start")]
        public async Task<IActionResult> Start(int id)
        {
            await _service.StartAsync(id);
            return Ok();
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            await _service.CompleteAsync(id);
            return Ok();
        }
    }
}