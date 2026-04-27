namespace PropertyManagementApp.Api.Models
{
    public class WorkLog
    {
        public int LogID { get; set; }

        public int ProjectID { get; set; }
        public MaintenanceProject? MaintenanceProject { get; set; }

        public DateTime? ClockInTime { get; set; }
        public DateTime? ClockOutTime { get; set; }

        public string? GPSLocation { get; set; }
        public string? ProofPhotoURL { get; set; }
        public string? MaterialsUsed { get; set; }
        public string? VendorSignature { get; set; }
    }
}