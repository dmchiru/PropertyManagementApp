namespace PropertyManagementApp.Shared.DTOs
{
    public class CommunicationLogDto
    {
        public int LogID { get; set; }
        public int TenantID { get; set; }
        public int? ScheduleID { get; set; }

        public string TenantName { get; set; } = "";
        public string Channel { get; set; } = "";
        public string? TemplateUsed { get; set; }
        public string MessageBody { get; set; } = "";
        public string SentBy { get; set; } = "";
        public DateTime SentAt { get; set; }
    }
}
