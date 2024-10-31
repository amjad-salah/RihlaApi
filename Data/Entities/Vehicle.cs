using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Entities;

public class Vehicle : BaseEntity
{
    public string Model { get; set; } = string.Empty;
    public string PlateNo { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public int? NoOfSeats { get; set; }
    public int? TotalWeight { get; set; }
    public int CompanyId { get; set; }
    [ForeignKey("CompanyId")] 
    public virtual Company? Company { get; set; }
    public virtual List<Journey>? Journeys { get; set; }
}