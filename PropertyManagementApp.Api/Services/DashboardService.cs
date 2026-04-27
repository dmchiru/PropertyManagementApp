using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Api.Data;

namespace PropertyManagementApp.Api.Services
{
    public class DashboardService
    {
        private readonly PropertyManagementDbContext _context;

        public DashboardService(PropertyManagementDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync()
        {
            var totalTenants = await _context.Tenants.CountAsync();

            var rentSchedules = await _context.RentSchedules
                .Include(r => r.RentPayments)
                .ToListAsync();

            var totalRentDue = rentSchedules
                .Where(r => r.Status != "Paid")
                .Sum(r =>
                    (r.BaseRent + r.LateFeeAccrued) -
                    r.RentPayments.Sum(p => p.AmountPaid));

            var totalInvoiced = await _context.Invoices
                .SumAsync(i => i.TotalAmount);

            var activeMaintenance = await _context.MaintenanceProjects
                .CountAsync(m => m.Status != "Closed");

            var activeEvictions = await _context.EvictionCases
                .CountAsync(e => e.Status == "Active");

            var openInvoices = await _context.Invoices
                .CountAsync(i => i.Status != "Paid");

            return new DashboardSummaryDto
            {
                TotalTenants = totalTenants,
                TotalRentDue = totalRentDue,
                TotalInvoiced = totalInvoiced,
                ActiveMaintenance = activeMaintenance,
                ActiveEvictions = activeEvictions,
                OpenInvoices = openInvoices
            };
        }
    }

    public class DashboardSummaryDto
    {
        public int TotalTenants { get; set; }
        public decimal TotalRentDue { get; set; }
        public decimal TotalInvoiced { get; set; }
        public int ActiveMaintenance { get; set; }
        public int ActiveEvictions { get; set; }
        public int OpenInvoices { get; set; }
    }
}