namespace PropertyManagementApp.Shared.DTOs
{
    public class MaintenanceProjectDto
    {
        public int ProjectID { get; set; }

        public string? ProjectTitle { get; set; }

        public string? Status { get; set; }

        public decimal BidAmount { get; set; }

        public string? AssignedVendor { get; set; }

        public string? PropertyName { get; set; }
    }
}