using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Services
{
    public interface IMaintenanceService
    {
        Task<List<MaintenanceProjectDto>> GetAllAsync();
        Task StartAsync(int id);
        Task CompleteAsync(int id);
    }
}