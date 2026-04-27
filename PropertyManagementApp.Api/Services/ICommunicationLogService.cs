using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Services
{
    public interface ICommunicationLogService
    {
        Task<List<CommunicationLogDto>> GetAllAsync();
        Task<List<CommunicationLogDto>> GetByTenantAsync(int tenantId);
        Task<CommunicationLogDto> CreateAsync(CreateCommunicationLogDto dto);
    }
}