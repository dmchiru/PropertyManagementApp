using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Api.Data;

namespace PropertyManagementApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentRecordsController : ControllerBase
    {
        private readonly PropertyManagementDbContext _context;

        public RentRecordsController(PropertyManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet("{tenantId}")]
        public async Task<IActionResult> GetLedger(int tenantId)
        {
            var charges = await _context.RentSchedules
                .Where(r => r.TenantID == tenantId)
                .Select(r => new LedgerRow
                {
                    Date = r.DueDate,
                    Description = "Monthly Rent",
                    Charge = r.BaseRent + r.LateFeeAccrued,
                    Payment = 0m
                })
                .ToListAsync();

            var payments = await _context.RentPayments
                .Include(p => p.RentSchedule)
                .Where(p => p.RentSchedule != null && p.RentSchedule.TenantID == tenantId)
                .Select(p => new LedgerRow
                {
                    Date = p.PaymentDate,
                    Description = "Payment - " + (p.PaymentMethod ?? "Manual"),
                    Charge = 0m,
                    Payment = p.AmountPaid
                })
                .ToListAsync();

            var all = charges
                .Concat(payments)
                .OrderBy(x => x.Date)
                .ToList();

            decimal runningBalance = 0;

            var ledger = all.Select(x =>
            {
                runningBalance += x.Charge - x.Payment;

                return new LedgerRow
                {
                    Date = x.Date,
                    Description = x.Description,
                    Charge = x.Charge,
                    Payment = x.Payment,
                    Balance = runningBalance
                };
            }).ToList();

            return Ok(ledger);
        }

        private class LedgerRow
        {
            public DateTime Date { get; set; }
            public string Description { get; set; } = "";
            public decimal Charge { get; set; }
            public decimal Payment { get; set; }
            public decimal Balance { get; set; }
        }
    }
}