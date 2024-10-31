namespace Backend.Data.DTOs.Company;

public class UpsertCompany
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public string? Email { get; set; }
    public int CountryId { get; set; }
}