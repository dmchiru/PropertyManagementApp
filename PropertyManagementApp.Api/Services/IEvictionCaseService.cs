using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Services
{
    public interface IEvictionCaseService
    {
        Task<List<EvictionCaseDto>> GetAllAsync();
        Task<EvictionCaseDto> CreateAsync(CreateEvictionCaseDto dto);
        Task<EvictionCaseDto> GenerateNoticeAsync(int caseId);
        Task<EvictionCaseDto> CloseAsync(int caseId);
    }
}