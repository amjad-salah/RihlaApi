namespace Backend.Data.DTOs.Driver;

public class UpsertDriver
{
    public string Name { get; set; } = string.Empty;
    public string LicenseNo { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public int CompanyId { get; set; }
}