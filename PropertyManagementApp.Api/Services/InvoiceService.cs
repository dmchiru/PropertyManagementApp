using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Api.Data;
using PropertyManagementApp.Api.Models;
using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly PropertyManagementDbContext _context;

        public InvoiceService(PropertyManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<InvoiceDto>> GetAllAsync()
        {
            return await _context.Invoices
                .Include(i => i.RentSchedule)
                    .ThenInclude(r => r!.Tenant)
                    .ThenInclude(t => t!.Property)
                .Select(i => new InvoiceDto
                {
                    InvoiceID = i.InvoiceID,
                    ProjectID = i.ProjectID,
                    ScheduleID = i.ScheduleID,
                    InvoiceDate = i.InvoiceDate,
                    TotalAmount = i.TotalAmount,
                    IsExported = i.IsExported,
                    Status = i.Status,
                    TenantName = i.RentSchedule != null && i.RentSchedule.Tenant != null
                        ? i.RentSchedule.Tenant.FirstName + " " + i.RentSchedule.Tenant.LastName
                        : "Maintenance / General",
                    PropertyName = i.RentSchedule != null &&
                                   i.RentSchedule.Tenant != null &&
                                   i.RentSchedule.Tenant.Property != null
                        ? i.RentSchedule.Tenant.Property.PropertyName
                        : ""
                })
                .ToListAsync();
        }

        public async Task<InvoiceDto> CreateAsync(CreateInvoiceDto dto)
        {
            var invoice = new Invoice
            {
                ProjectID = dto.ProjectID,
                ScheduleID = dto.ScheduleID,
                TotalAmount = dto.TotalAmount,
                Status = dto.Status ?? "Draft",
                InvoiceDate = DateTime.Now,
                IsExported = false
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return (await GetAllAsync()).First(x => x.InvoiceID == invoice.InvoiceID);
        }

        public async Task<InvoiceDto> MarkPaidAsync(int invoiceId)
        {
            var invoice = await _context.Invoices
                .Include(i => i.RentSchedule)
                    .ThenInclude(r => r!.RentPayments)
                .FirstOrDefaultAsync(i => i.InvoiceID == invoiceId);

            if (invoice == null)
                throw new KeyNotFoundException("Invoice not found.");

            invoice.Status = "Paid";

            if (invoice.RentSchedule != null)
            {
                var schedule = invoice.RentSchedule;

                var existingPaid = schedule.RentPayments.Sum(p => p.AmountPaid);
                var totalDue = schedule.BaseRent + schedule.LateFeeAccrued;
                var remainingBalance = totalDue - existingPaid;

                if (remainingBalance > 0)
                {
                    var paymentAmount = Math.Min(invoice.TotalAmount, remainingBalance);

                    var payment = new RentPayment
                    {
                        ScheduleID = schedule.ScheduleID,
                        PaymentDate = DateTime.Now,
                        AmountPaid = paymentAmount,
                        PaymentMethod = "Invoice",
                        TransactionRef = $"Invoice #{invoice.InvoiceID}",
                        Source = "Invoice"
                    };

                    _context.RentPayments.Add(payment);

                    var newTotalPaid = existingPaid + paymentAmount;
                    schedule.Status = newTotalPaid >= totalDue ? "Paid" : "Partial";
                }
            }

            await _context.SaveChangesAsync();

            return (await GetAllAsync()).First(x => x.InvoiceID == invoiceId);
        }

        public async Task<InvoiceDto> SendAsync(int invoiceId)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);

            if (invoice == null)
                throw new KeyNotFoundException("Invoice not found.");

            if (invoice.Status != "Paid")
            {
                invoice.Status = "Sent";
            }

            await _context.SaveChangesAsync();

            return (await GetAllAsync()).First(x => x.InvoiceID == invoiceId);
        }

        public async Task<InvoiceDto> GenerateFromRentAsync(int scheduleId)
        {
            var schedule = await _context.RentSchedules
                .FirstOrDefaultAsync(r => r.ScheduleID == scheduleId);

            if (schedule == null)
                throw new KeyNotFoundException("Rent schedule not found.");

            var invoice = new Invoice
            {
                ScheduleID = schedule.ScheduleID,
                ProjectID = null,
                InvoiceDate = DateTime.Now,
                TotalAmount = schedule.BaseRent + schedule.LateFeeAccrued,
                Status = "Draft",
                IsExported = false
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return (await GetAllAsync()).First(x => x.InvoiceID == invoice.InvoiceID);
        }
    }
}