namespace Backend.Data.DTOs.City;

public class UpsertCity
{
    public int CountryId { get; set; }
    public string Name { get; set; } = string.Empty;
}