namespace PropertyManagementApp.Api.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }

        public int? ProjectID { get; set; }

        public int? ScheduleID { get; set; }
        public RentSchedule? RentSchedule { get; set; }

        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        public bool IsExported { get; set; }

        public string Status { get; set; } = "Draft";
    }
}