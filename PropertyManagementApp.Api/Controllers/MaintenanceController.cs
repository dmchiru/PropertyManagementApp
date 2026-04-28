using Microsoft.AspNetCore.Mvc;
using PropertyManagementApp.Api.Services;
using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Controllers
{
    [Route("api/maintenanceprojects")]
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
            var projects = await _service.GetAllAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _service.GetByIdAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMaintenanceProjectDto dto)
        {
            var createdProject = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdProject.ProjectID },
                createdProject
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateMaintenanceProjectDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
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