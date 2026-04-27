using System.ComponentModel.DataAnnotations;

namespace PropertyManagementApp.Shared.DTOs
{
    public class CreateInvoiceDto
    {
        public int? ProjectID { get; set; }
        public int? ScheduleID { get; set; }

        [Required]
        [Range(0.01, 999999)]
        public decimal TotalAmount { get; set; }

        public string? Status { get; set; } = "Draft";
    }
}