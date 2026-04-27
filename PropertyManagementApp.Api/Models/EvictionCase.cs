namespace PropertyManagementApp.Api.Models
{
    public class EvictionCase
    {
        public int CaseID { get; set; }

        public int TenantID { get; set; }
        public Tenant? Tenant { get; set; }

        public DateTime OpenedAt { get; set; } = DateTime.Now;

        public string Reason { get; set; } = "Late Rent";

        public string? CurrentStep { get; set; } = "Prepare Notice";

        public string Status { get; set; } = "Active";

        public DateTime? ClosedAt { get; set; }

        public string? Notes { get; set; }
    }
}