namespace Backend.Data.DTOs.Journey;

public class UpsertJourney
{
    public DateTime DepDate { get; set; }
    public DateTime ArrDate { get; set; }
    public int DepCityId { get; set; }
    public int ArrCityId { get; set; }
    public int VehicleId { get; set; }
    public int DriverId { get; set; }
    public string JourneyType { get; set; } = string.Empty;
    public int CompanyId { get; set; }
}
