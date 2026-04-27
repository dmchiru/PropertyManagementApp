namespace PropertyManagementApp.Shared.DTOs
{
    public class EvictionCaseDto
    {
        public int CaseID { get; set; }
        public int TenantID { get; set; }

        public string TenantName { get; set; } = "";
        public string PropertyName { get; set; } = "";
        public string UnitNumber { get; set; } = "";

        public DateTime OpenedAt { get; set; }

        public string Reason { get; set; } = "";
        public string? CurrentStep { get; set; }
        public string Status { get; set; } = "Active";

        public DateTime? ClosedAt { get; set; }
        public string? Notes { get; set; }
    }
}