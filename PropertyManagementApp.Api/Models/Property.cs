namespace PropertyManagementApp.Api.Models
{
    public class Property
    {
        public int PropertyID { get; set; }
        public string? PropertyName { get; set; }
        public string? Address { get; set; }
        public string? UnitNumber { get; set; }
        public decimal MonthlyRent { get; set; }

        public ICollection<Tenant>? Tenants { get; set; }
    }
}