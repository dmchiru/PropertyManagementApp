using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Api.Data;
using PropertyManagementApp.Api.Models;
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

        public async Task<MaintenanceProjectDto?> GetByIdAsync(int id)
        {
            return await _context.MaintenanceProjects
                .Include(p => p.Property)
                .Where(p => p.ProjectID == id)
                .Select(p => new MaintenanceProjectDto
                {
                    ProjectID = p.ProjectID,
                    ProjectTitle = p.ProjectTitle,
                    Status = p.Status,
                    BidAmount = p.BidAmount,
                    AssignedVendor = p.AssignedVendor,
                    PropertyName = p.Property != null ? p.Property.PropertyName : ""
                })
                .FirstOrDefaultAsync();
        }

        public async Task<MaintenanceProjectDto> CreateAsync(CreateMaintenanceProjectDto dto)
        {
            var project = new MaintenanceProject
            {
                PropertyID = dto.PropertyID,
                ProjectTitle = dto.ProjectTitle,
                BidAmount = dto.BidAmount,
                Status = dto.Status,
                AssignedVendor = dto.AssignedVendor
            };

            _context.MaintenanceProjects.Add(project);
            await _context.SaveChangesAsync();

            return new MaintenanceProjectDto
            {
                ProjectID = project.ProjectID,
                ProjectTitle = project.ProjectTitle,
                Status = project.Status,
                BidAmount = project.BidAmount,
                AssignedVendor = project.AssignedVendor
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateMaintenanceProjectDto dto)
        {
            var project = await _context.MaintenanceProjects.FindAsync(id);

            if (project == null)
            {
                return false;
            }

            project.PropertyID = dto.PropertyID;
            project.ProjectTitle = dto.ProjectTitle;
            project.BidAmount = dto.BidAmount;
            project.Status = dto.Status;
            project.AssignedVendor = dto.AssignedVendor;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.MaintenanceProjects.FindAsync(id);

            if (project == null)
            {
                return false;
            }

            _context.MaintenanceProjects.Remove(project);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task StartAsync(int id)
        {
            var project = await _context.MaintenanceProjects.FindAsync(id);

            if (project == null)
            {
                return;
            }

            project.Status = "Work Order";
            await _context.SaveChangesAsync();
        }

        public async Task CompleteAsync(int id)
        {
            var project = await _context.MaintenanceProjects.FindAsync(id);

            if (project == null)
            {
                return;
            }

            project.Status = "Closed";
            await _context.SaveChangesAsync();
        }
    }
}