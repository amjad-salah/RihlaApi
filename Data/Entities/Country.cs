namespace Backend.Data.Entities;

public class Country : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public virtual List<City>? Cities { get; set; }
    public virtual List<Company>? Companies { get; set; }
}