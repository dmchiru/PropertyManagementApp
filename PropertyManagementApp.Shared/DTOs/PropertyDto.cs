namespace PropertyManagementApp.Shared.DTOs
{
    public class PropertyDto
    {
        public int PropertyID { get; set; }
        public string? PropertyName { get; set; }
        public string? Address { get; set; }
        public string? UnitNumber { get; set; }
        public decimal MonthlyRent { get; set; }

        public string DisplayName
        {
            get
            {
                var unit = string.IsNullOrWhiteSpace(UnitNumber) ? string.Empty : $" Unit {UnitNumber}";
                return $"{PropertyName} - {Address}{unit}";
            }
        }
    }
}