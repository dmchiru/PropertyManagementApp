using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Api.Data;
using PropertyManagementApp.Api.Models;
using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Services
{
    public class RentScheduleService : IRentScheduleService
    {
        private readonly PropertyManagementDbContext _context;

        public RentScheduleService(PropertyManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<RentScheduleDto>> GetAllAsync()
        {
            return await _context.RentSchedules
                .Include(r => r.Tenant)
                .ThenInclude(t => t!.Property)
                .Include(r => r.RentPayments)
                .Select(r => new RentScheduleDto
                {
                    ScheduleID = r.ScheduleID,
                    TenantID = r.TenantID,
                    DueDate = r.DueDate,
                    Status = r.Status,
                    BaseRent = r.BaseRent,
                    LateFeeAccrued = r.LateFeeAccrued,
                    ReminderCount = r.ReminderCount,
                    AmountPaid = r.RentPayments.Sum(p => p.AmountPaid),
                    TenantName = r.Tenant != null ? r.Tenant.FirstName + " " + r.Tenant.LastName : "",
                    PropertyName = r.Tenant != null && r.Tenant.Property != null ? r.Tenant.Property.PropertyName : "",
                    UnitNumber = r.Tenant != null && r.Tenant.Property != null ? r.Tenant.Property.UnitNumber : ""
                })
                .ToListAsync();
        }

        public async Task<RentScheduleDto> MarkPaidAsync(int scheduleId, MarkPaidDto dto)
        {
            var schedule = await _context.RentSchedules
                .Include(r => r.RentPayments)
                .FirstOrDefaultAsync(r => r.ScheduleID == scheduleId);

            if (schedule == null)
                throw new KeyNotFoundException("Rent schedule not found.");

            var payment = new RentPayment
            {
                ScheduleID = scheduleId,
                AmountPaid = dto.AmountPaid,
                PaymentMethod = dto.PaymentMethod,
                TransactionRef = dto.TransactionRef,
                PaymentDate = DateTime.Now
            };

            _context.RentPayments.Add(payment);

            var totalPaid = schedule.RentPayments.Sum(p => p.AmountPaid) + dto.AmountPaid;
            var totalDue = schedule.BaseRent + schedule.LateFeeAccrued;

            schedule.Status = totalPaid >= totalDue ? "Paid" : "Partial";

            await _context.SaveChangesAsync();

            return (await GetAllAsync()).First(x => x.ScheduleID == scheduleId);
        }

        public async Task<RentScheduleDto> ApplyLateFeeAsync(int scheduleId)
        {
            var schedule = await _context.RentSchedules.FindAsync(scheduleId);

            if (schedule == null)
                throw new KeyNotFoundException("Rent schedule not found.");

            if (schedule.Status != "Paid")
            {
                schedule.LateFeeAccrued = 75;
                schedule.Status = "Late";
            }

            await _context.SaveChangesAsync();

            return (await GetAllAsync()).First(x => x.ScheduleID == scheduleId);
        }
    }
}