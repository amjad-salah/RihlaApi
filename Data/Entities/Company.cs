using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Entities;

public class Company : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public string? Email { get; set; }
    public int CountryId { get; set; }
    [ForeignKey("CountryId")] 
    public virtual Country? Country { get; set; }
    public virtual List<Vehicle>? Vehicles { get; set; }
    public virtual List<Driver>? Drivers { get; set; } 
    public virtual List<Journey>? Journeys { get; set; } 
    public virtual List<User>? Users { get; set; }
}