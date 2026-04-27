using System.ComponentModel.DataAnnotations;

namespace PropertyManagementApp.Shared.DTOs
{
    public class MarkPaidDto
    {
        [Required]
        [Range(0.01, 999999)]
        public decimal AmountPaid { get; set; }

        [Required]
        public string PaymentMethod { get; set; } = "ACH";

        public string? TransactionRef { get; set; }
    }
}