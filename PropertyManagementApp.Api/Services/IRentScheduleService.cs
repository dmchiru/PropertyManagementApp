using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Services
{
    public interface IRentScheduleService
    {
        Task<List<RentScheduleDto>> GetAllAsync();
        Task<RentScheduleDto> MarkPaidAsync(int scheduleId, MarkPaidDto dto);
        Task<RentScheduleDto> ApplyLateFeeAsync(int scheduleId);
    }
}