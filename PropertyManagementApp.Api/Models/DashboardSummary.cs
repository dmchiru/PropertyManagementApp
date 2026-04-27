namespace PropertyManagementApp.Api.Models
{
    public class DashboardSummary
    {
        public int TotalTenants { get; set; }

        public decimal TotalRentDue { get; set; }

        public decimal TotalInvoiced { get; set; }

        public int ActiveMaintenance { get; set; }

        public int ActiveEvictions { get; set; }
    }
}