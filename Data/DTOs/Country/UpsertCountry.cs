namespace Backend.Data.DTOs.Country;

public class UpsertCountry
{
    public string Name { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
}