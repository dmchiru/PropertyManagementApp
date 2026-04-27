namespace PropertyManagementApp.Shared.DTOs
{
    public class InvoiceDto
    {
        public int InvoiceID { get; set; }
        public int? ProjectID { get; set; }
        public int? ScheduleID { get; set; }

        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }

        public bool IsExported { get; set; }

        public string? TenantName { get; set; }
        public string? PropertyName { get; set; }
        public string? Status { get; set; }
    }
}