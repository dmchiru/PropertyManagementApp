using Microsoft.AspNetCore.Mvc;
using PropertyManagementApp.Api.Services;
using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentSchedulesController : ControllerBase
    {
        private readonly IRentScheduleService _service;

        public RentSchedulesController(IRentScheduleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<RentScheduleDto>>> GetRentSchedules()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost("{id}/mark-paid")]
        public async Task<ActionResult<RentScheduleDto>> MarkPaid(int id, [FromBody] MarkPaidDto dto)
        {
            return Ok(await _service.MarkPaidAsync(id, dto));
        }

        [HttpPost("{id}/apply-late-fee")]
        public async Task<ActionResult<RentScheduleDto>> ApplyLateFee(int id)
        {
            return Ok(await _service.ApplyLateFeeAsync(id));
        }
    }
}