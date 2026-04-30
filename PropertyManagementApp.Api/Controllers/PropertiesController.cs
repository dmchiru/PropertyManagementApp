using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Api.Data;
using PropertyManagementApp.Api.Models;
using PropertyManagementApp.Api.Security;
using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly PropertyManagementDbContext _context;

        public PropertiesController(PropertyManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyDto>>> GetProperties()
        {
            var properties = await _context.Properties
                .OrderBy(p => p.PropertyName)
                .ThenBy(p => p.UnitNumber)
                .Select(p => new PropertyDto
                {
                    PropertyID = p.PropertyID,
                    PropertyName = p.PropertyName,
                    Address = p.Address,
                    UnitNumber = p.UnitNumber,
                    MonthlyRent = p.MonthlyRent
                })
                .ToListAsync();

            return Ok(properties);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PropertyDto>> GetProperty(int id)
        {
            var property = await _context.Properties
                .Where(p => p.PropertyID == id)
                .Select(p => new PropertyDto
                {
                    PropertyID = p.PropertyID,
                    PropertyName = p.PropertyName,
                    Address = p.Address,
                    UnitNumber = p.UnitNumber,
                    MonthlyRent = p.MonthlyRent
                })
                .FirstOrDefaultAsync();

            if (property == null)
            {
                return NotFound(new { message = "Property was not found." });
            }

            return Ok(property);
        }

        [HttpPost]
        public async Task<ActionResult<PropertyDto>> CreateProperty(PropertyDto dto)
        {
            var validationMessage = ValidateProperty(dto);

            if (!string.IsNullOrWhiteSpace(validationMessage))
            {
                return BadRequest(new { message = validationMessage });
            }

            var property = new Property
            {
                PropertyName = dto.PropertyName?.Trim(),
                Address = dto.Address?.Trim(),
                UnitNumber = dto.UnitNumber?.Trim(),
                MonthlyRent = dto.MonthlyRent
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            dto.PropertyID = property.PropertyID;

            return CreatedAtAction(nameof(GetProperty), new { id = property.PropertyID }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProperty(int id, PropertyDto dto)
        {
            if (id != dto.PropertyID)
            {
                return BadRequest(new { message = "Route property ID does not match request property ID." });
            }

            var validationMessage = ValidateProperty(dto);

            if (!string.IsNullOrWhiteSpace(validationMessage))
            {
                return BadRequest(new { message = validationMessage });
            }

            var property = await _context.Properties.FindAsync(id);

            if (property == null)
            {
                return NotFound(new { message = "Property was not found." });
            }

            property.PropertyName = dto.PropertyName?.Trim();
            property.Address = dto.Address?.Trim();
            property.UnitNumber = dto.UnitNumber?.Trim();
            property.MonthlyRent = dto.MonthlyRent;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var property = await _context.Properties.FindAsync(id);

            if (property == null)
            {
                return NotFound(new { message = "Property was not found." });
            }

            var hasTenants = await _context.Tenants.AnyAsync(t => t.PropertyID == id);
            var hasMaintenanceProjects = await _context.MaintenanceProjects.AnyAsync(p => p.PropertyID == id);

            if (hasTenants || hasMaintenanceProjects)
            {
                return BadRequest(new
                {
                    message = "This property cannot be deleted because it has assigned tenants or maintenance projects."
                });
            }

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string? ValidateProperty(PropertyDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.PropertyName))
            {
                return "Property name is required.";
            }

            if (string.IsNullOrWhiteSpace(dto.Address))
            {
                return "Address is required.";
            }

            if (string.IsNullOrWhiteSpace(dto.UnitNumber))
            {
                return "Unit number is required.";
            }

            if (dto.MonthlyRent <= 0)
            {
                return "Monthly rent must be greater than zero.";
            }

            return null;
        }
    }
}