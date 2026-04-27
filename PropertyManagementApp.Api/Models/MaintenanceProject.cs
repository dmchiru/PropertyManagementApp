namespace PropertyManagementApp.Api.Models
{
    public class MaintenanceProject
    {
        public int ProjectID { get; set; }

        public int PropertyID { get; set; }
        public Property? Property { get; set; }

        public string? ProjectTitle { get; set; }

        public decimal BidAmount { get; set; }

        public string? Status { get; set; }

        public string? AssignedVendor { get; set; }
    }
}