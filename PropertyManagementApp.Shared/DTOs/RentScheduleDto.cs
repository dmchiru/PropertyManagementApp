namespace PropertyManagementApp.Shared.DTOs
{
    public class RentScheduleDto
    {
        public int ScheduleID { get; set; }
        public int TenantID { get; set; }

        public DateTime DueDate { get; set; }
        public string? Status { get; set; }

        public decimal BaseRent { get; set; }
        public decimal LateFeeAccrued { get; set; }
        public decimal AmountPaid { get; set; }

        public decimal Balance => BaseRent + LateFeeAccrued - AmountPaid;

        public int ReminderCount { get; set; }

        public string? TenantName { get; set; }
        public string? PropertyName { get; set; }
        public string? UnitNumber { get; set; }
    }
}