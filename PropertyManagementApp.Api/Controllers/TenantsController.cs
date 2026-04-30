using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Api.Data;
using PropertyManagementApp.Api.Models;
using PropertyManagementApp.Api.Security;
using PropertyManagementApp.Shared;

namespace PropertyManagementApp.Api.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly PropertyManagementDbContext _context;

        public TenantsController(PropertyManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TenantDto>>> GetTenants()
        {
            var tenants = await TenantQuery()
                .OrderBy(t => t.LastName)
                .ThenBy(t => t.FirstName)
                .ToListAsync();

            return Ok(tenants);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TenantDto>> GetTenant(int id)
        {
            var tenant = await TenantQuery()
                .FirstOrDefaultAsync(t => t.TenantID == id);

            if (tenant == null)
            {
                return NotFound(new { message = "Tenant was not found." });
            }

            return Ok(tenant);
        }

        [HttpPost]
        public async Task<ActionResult<TenantDto>> CreateTenant(TenantDto dto)
        {
            var validationMessage = await ValidateTenantAsync(dto);

            if (!string.IsNullOrWhiteSpace(validationMessage))
            {
                return BadRequest(new { message = validationMessage });
            }

            var tenant = new Tenant
            {
                FirstName = dto.FirstName?.Trim(),
                LastName = dto.LastName?.Trim(),
                Email = dto.Email?.Trim(),
                PhoneNumber = dto.PhoneNumber?.Trim(),
                PropertyID = dto.PropertyID
            };

            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();

            var createdTenant = await TenantQuery()
                .FirstAsync(t => t.TenantID == tenant.TenantID);

            return CreatedAtAction(nameof(GetTenant), new { id = createdTenant.TenantID }, createdTenant);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTenant(int id, TenantDto dto)
        {
            if (id != dto.TenantID)
            {
                return BadRequest(new { message = "Route tenant ID does not match request tenant ID." });
            }

            var validationMessage = await ValidateTenantAsync(dto);

            if (!string.IsNullOrWhiteSpace(validationMessage))
            {
                return BadRequest(new { message = validationMessage });
            }

            var tenant = await _context.Tenants.FindAsync(id);

            if (tenant == null)
            {
                return NotFound(new { message = "Tenant was not found." });
            }

            tenant.FirstName = dto.FirstName?.Trim();
            tenant.LastName = dto.LastName?.Trim();
            tenant.Email = dto.Email?.Trim();
            tenant.PhoneNumber = dto.PhoneNumber?.Trim();
            tenant.PropertyID = dto.PropertyID;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTenant(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);

            if (tenant == null)
            {
                return NotFound(new { message = "Tenant was not found." });
            }

            var scheduleIds = await _context.RentSchedules
                .Where(schedule => schedule.TenantID == id)
                .Select(schedule => schedule.ScheduleID)
                .ToListAsync();

            var relatedInvoices = await _context.Invoices
                .Where(invoice => invoice.ScheduleID.HasValue && scheduleIds.Contains(invoice.ScheduleID.Value))
                .ToListAsync();

            foreach (var invoice in relatedInvoices)
            {
                invoice.ScheduleID = null;
            }

            var communicationLogs = await _context.CommunicationLogs
                .Where(log => log.TenantID == id ||
                              (log.ScheduleID.HasValue && scheduleIds.Contains(log.ScheduleID.Value)))
                .ToListAsync();

            var rentPayments = await _context.RentPayments
                .Where(payment => scheduleIds.Contains(payment.ScheduleID))
                .ToListAsync();

            var rentSchedules = await _context.RentSchedules
                .Where(schedule => schedule.TenantID == id)
                .ToListAsync();

            var evictionCases = await _context.EvictionCases
                .Where(evictionCase => evictionCase.TenantID == id)
                .ToListAsync();

            _context.CommunicationLogs.RemoveRange(communicationLogs);
            _context.RentPayments.RemoveRange(rentPayments);
            _context.RentSchedules.RemoveRange(rentSchedules);
            _context.EvictionCases.RemoveRange(evictionCases);
            _context.Tenants.Remove(tenant);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private IQueryable<TenantDto> TenantQuery()
        {
            return _context.Tenants
                .Include(t => t.Property)
                .Select(t => new TenantDto
                {
                    TenantID = t.TenantID,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Email = t.Email,
                    PhoneNumber = t.PhoneNumber,
                    PropertyID = t.PropertyID,
                    PropertyName = t.Property != null ? t.Property.PropertyName : null,
                    Address = t.Property != null ? t.Property.Address : null,
                    UnitNumber = t.Property != null ? t.Property.UnitNumber : null
                });
        }

        private async Task<string?> ValidateTenantAsync(TenantDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName))
            {
                return "First name is required.";
            }

            if (string.IsNullOrWhiteSpace(dto.LastName))
            {
                return "Last name is required.";
            }

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                return "Email is required.";
            }

            if (!dto.Email.Contains("@"))
            {
                return "Email format is not valid.";
            }

            if (dto.PropertyID <= 0)
            {
                return "A property must be selected.";
            }

            var propertyExists = await _context.Properties
                .AnyAsync(property => property.PropertyID == dto.PropertyID);

            if (!propertyExists)
            {
                return "Selected property does not exist.";
            }

            return null;
        }
    }
}