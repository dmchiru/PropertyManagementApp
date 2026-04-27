using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Api.Data;
using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly PropertyManagementDbContext _context;

        public MaintenanceService(PropertyManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<MaintenanceProjectDto>> GetAllAsync()
        {
            return await _context.MaintenanceProjects
                .Include(p => p.Property)
                .Select(p => new MaintenanceProjectDto
                {
                    ProjectID = p.ProjectID,
                    ProjectTitle = p.ProjectTitle,
                    Status = p.Status,
                    BidAmount = p.BidAmount,
                    AssignedVendor = p.AssignedVendor,
                    PropertyName = p.Property != null ? p.Property.PropertyName : ""
                })
                .ToListAsync();
        }

        public async Task StartAsync(int id)
        {
            var project = await _context.MaintenanceProjects.FindAsync(id);
            if (project == null) return;

            project.Status = "Work Order";
            await _context.SaveChangesAsync();
        }

        public async Task CompleteAsync(int id)
        {
            var project = await _context.MaintenanceProjects.FindAsync(id);
            if (project == null) return;

            project.Status = "Closed";
            await _context.SaveChangesAsync();
        }
    }
}