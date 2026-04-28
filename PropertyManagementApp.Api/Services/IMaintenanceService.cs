using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Services
{
    public interface IMaintenanceService
    {
        Task<List<MaintenanceProjectDto>> GetAllAsync();
        Task<MaintenanceProjectDto?> GetByIdAsync(int id);
        Task<MaintenanceProjectDto> CreateAsync(CreateMaintenanceProjectDto dto);
        Task<bool> UpdateAsync(int id, CreateMaintenanceProjectDto dto);
        Task<bool> DeleteAsync(int id);
        Task StartAsync(int id);
        Task CompleteAsync(int id);
    }
}