using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Api.Data;
using PropertyManagementApp.Api.Models;
using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Services
{
    public class CommunicationLogService : ICommunicationLogService
    {
        private readonly PropertyManagementDbContext _context;

        public CommunicationLogService(PropertyManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<CommunicationLogDto>> GetAllAsync()
        {
            return await _context.CommunicationLogs
                .Include(c => c.Tenant)
                .OrderByDescending(c => c.SentAt)
                .Select(c => new CommunicationLogDto
                {
                    LogID = c.LogID,
                    TenantID = c.TenantID,
                    ScheduleID = c.ScheduleID,
                    TenantName = c.Tenant != null ? c.Tenant.FirstName + " " + c.Tenant.LastName : "",
                    Channel = c.Channel,
                    TemplateUsed = c.TemplateUsed,
                    MessageBody = c.MessageBody,
                    SentBy = c.SentBy,
                    SentAt = c.SentAt
                })
                .ToListAsync();
        }

        public async Task<List<CommunicationLogDto>> GetByTenantAsync(int tenantId)
        {
            return await _context.CommunicationLogs
                .Include(c => c.Tenant)
                .Where(c => c.TenantID == tenantId)
                .OrderByDescending(c => c.SentAt)
                .Select(c => new CommunicationLogDto
                {
                    LogID = c.LogID,
                    TenantID = c.TenantID,
                    ScheduleID = c.ScheduleID,
                    TenantName = c.Tenant != null ? c.Tenant.FirstName + " " + c.Tenant.LastName : "",
                    Channel = c.Channel,
                    TemplateUsed = c.TemplateUsed,
                    MessageBody = c.MessageBody,
                    SentBy = c.SentBy,
                    SentAt = c.SentAt
                })
                .ToListAsync();
        }

        public async Task<CommunicationLogDto> CreateAsync(CreateCommunicationLogDto dto)
        {
            var log = new CommunicationLog
            {
                TenantID = dto.TenantID,
                ScheduleID = dto.ScheduleID,
                Channel = dto.Channel,
                TemplateUsed = dto.TemplateUsed,
                MessageBody = dto.MessageBody,
                SentBy = "Admin Demo",
                SentAt = DateTime.Now
            };

            _context.CommunicationLogs.Add(log);

            if (dto.ScheduleID.HasValue)
            {
                var schedule = await _context.RentSchedules.FindAsync(dto.ScheduleID.Value);
                if (schedule != null)
                {
                    schedule.ReminderCount += 1;
                }
            }

            await _context.SaveChangesAsync();

            return (await GetAllAsync()).First(x => x.LogID == log.LogID);
        }
    }
}