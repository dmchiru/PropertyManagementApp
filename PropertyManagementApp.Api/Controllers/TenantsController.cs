using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Api.Data;
using PropertyManagementApp.Api.Models;

namespace PropertyManagementApp.Api.Controllers
{
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
        public async Task<ActionResult<IEnumerable<object>>> GetTenants()
        {
            var tenants = await _context.Tenants
                .Include(t => t.Property)
                .Select(t => new
                {
                    t.TenantID,
                    t.FirstName,
                    t.LastName,
                    t.Email,
                    t.PhoneNumber,
                    t.PropertyID,
                    PropertyName = t.Property != null ? t.Property.PropertyName : null,
                    Address = t.Property != null ? t.Property.Address : null,
                    UnitNumber = t.Property != null ? t.Property.UnitNumber : null
                })
                .ToListAsync();

            return Ok(tenants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTenant(int id)
        {
            var tenant = await _context.Tenants
                .Include(t => t.Property)
                .Where(t => t.TenantID == id)
                .Select(t => new
                {
                    t.TenantID,
                    t.FirstName,
                    t.LastName,
                    t.Email,
                    t.PhoneNumber,
                    t.PropertyID,
                    PropertyName = t.Property != null ? t.Property.PropertyName : null,
                    Address = t.Property != null ? t.Property.Address : null,
                    UnitNumber = t.Property != null ? t.Property.UnitNumber : null
                })
                .FirstOrDefaultAsync();

            if (tenant == null)
            {
                return NotFound();
            }

            return Ok(tenant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTenant(Tenant tenant)
        {
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTenant), new { id = tenant.TenantID }, tenant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTenant(int id, Tenant tenant)
        {
            if (id != tenant.TenantID)
            {
                return BadRequest();
            }

            _context.Entry(tenant).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTenant(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);

            if (tenant == null)
            {
                return NotFound();
            }

            _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}