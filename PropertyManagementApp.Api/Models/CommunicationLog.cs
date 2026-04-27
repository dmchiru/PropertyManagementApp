namespace PropertyManagementApp.Api.Models
{
    public class CommunicationLog
    {
        public int LogID { get; set; }

        public int TenantID { get; set; }
        public Tenant? Tenant { get; set; }

        public int? ScheduleID { get; set; }
        public RentSchedule? RentSchedule { get; set; }

        public string Channel { get; set; } = "SMS";
        public string? TemplateUsed { get; set; }
        public string MessageBody { get; set; } = "";
        public string SentBy { get; set; } = "System";
        public DateTime SentAt { get; set; } = DateTime.Now;
    }
}