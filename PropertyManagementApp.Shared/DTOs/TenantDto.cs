namespace PropertyManagementApp.Shared
{
    public class TenantDto
    {
        public int TenantID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int PropertyID { get; set; }
        public string? PropertyName { get; set; }
        public string? Address { get; set; }
        public string? UnitNumber { get; set; }
    }
}