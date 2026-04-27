using System.ComponentModel.DataAnnotations;

namespace PropertyManagementApp.Shared.DTOs
{
    public class CreateCommunicationLogDto
    {
        [Required]
        public int TenantID { get; set; }

        public int? ScheduleID { get; set; }

        [Required]
        public string Channel { get; set; } = "SMS";

        public string? TemplateUsed { get; set; }

        [Required]
        public string MessageBody { get; set; } = "";
    }
}