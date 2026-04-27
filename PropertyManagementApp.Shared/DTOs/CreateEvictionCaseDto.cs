using System.ComponentModel.DataAnnotations;

namespace PropertyManagementApp.Shared.DTOs
{
    public class CreateEvictionCaseDto
    {
        [Required]
        public int TenantID { get; set; }

        [Required]
        [StringLength(100)]
        public string Reason { get; set; } = "Late Rent";

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}