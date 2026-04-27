namespace PropertyManagementApp.Api.Models
{
    public class RentPayment
    {
        public int PaymentID { get; set; }

        public int ScheduleID { get; set; }
        public RentSchedule? RentSchedule { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal AmountPaid { get; set; }

        public string? PaymentMethod { get; set; }

        public string? TransactionRef { get; set; }

        public string Source { get; set; } = "Manual";
    }
}