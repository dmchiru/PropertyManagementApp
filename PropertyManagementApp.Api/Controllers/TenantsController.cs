using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Api.Data;

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
    }
}