namespace PropertyManagementApp.Api.Models
{
    public class RentSchedule
    {
        public int ScheduleID { get; set; }

        public int TenantID { get; set; }
        public Tenant? Tenant { get; set; }

        public DateTime DueDate { get; set; }

        public string? Status { get; set; }

        public decimal BaseRent { get; set; }

        public decimal LateFeeAccrued { get; set; }

        public int ReminderCount { get; set; }

        public ICollection<RentPayment> RentPayments { get; set; } = new List<RentPayment>();
    }
}