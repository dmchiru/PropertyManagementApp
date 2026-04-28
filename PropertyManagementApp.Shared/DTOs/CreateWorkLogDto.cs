namespace PropertyManagementApp.Shared.DTOs
{
    public class CreateWorkLogDto
    {
        public int ProjectID { get; set; }

        public DateTime? ClockInTime { get; set; }
        public DateTime? ClockOutTime { get; set; }

        public string? GPSLocation { get; set; }
        public string? ProofPhotoURL { get; set; }
        public string? MaterialsUsed { get; set; }
        public string? VendorSignature { get; set; }
    }
}