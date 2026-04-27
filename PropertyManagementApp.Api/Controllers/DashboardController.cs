using Microsoft.AspNetCore.Mvc;
using PropertyManagementApp.Api.Services;

namespace PropertyManagementApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _service;

        public DashboardController(DashboardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<DashboardSummaryDto>> GetSummary()
        {
            return Ok(await _service.GetSummaryAsync());
        }
    }
}