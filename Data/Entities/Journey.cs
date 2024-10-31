using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Entities;

public class Journey : BaseEntity
{
    public DateTime DepDate { get; set; }
    public DateTime ArrDate { get; set; }
    public int DepCityId { get; set; }
    [ForeignKey("DepCityId")] 
    public virtual City? DepCity { get; set; }
    public int ArrCityId { get; set; }
    [ForeignKey("ArrCityId")]
    public virtual City? ArrCity { get; set; }
    public int VehicleId { get; set; }
    [ForeignKey("VehicleId")]
    public virtual Vehicle? Vehicle { get; set; }
    public int DriverId { get; set; }
    [ForeignKey("DriverId")]
    public virtual Driver? Driver { get; set; }
    public string JourneyType { get; set; } = string.Empty;
    public int CompanyId { get; set; }
    [ForeignKey("CompanyId")]
    public virtual Company? Company { get; set; }
    public virtual List<Reservation>? Reservations { get; set; }
}