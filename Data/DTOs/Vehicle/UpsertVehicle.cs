namespace Backend.Data.DTOs.Vehicle;

public class UpsertVehicle
{
    public string Model { get; set; } = string.Empty;
    public string PlateNo { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public int? NoOfSeats { get; set; }
    public int? TotalWeight { get; set; }
    public int CompanyId { get; set; }
}