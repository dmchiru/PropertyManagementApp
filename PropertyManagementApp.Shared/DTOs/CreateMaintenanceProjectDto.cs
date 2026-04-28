using System.ComponentModel.DataAnnotations;

namespace PropertyManagementApp.Shared.DTOs
{
    public class CreateMaintenanceProjectDto
    {
        [Required]
        public int PropertyID { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProjectTitle { get; set; } = string.Empty;

        [Range(0, 999999)]
        public decimal BidAmount { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Bid";

        [MaxLength(100)]
        public string? AssignedVendor { get; set; }
    }
}