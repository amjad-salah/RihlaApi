using Backend.Data.Context;
using Backend.Data.DTOs.Journey;
using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.JourneyServices;

public class JourneyServices(RihlaContext db) : IJourneyServices
{
    private readonly RihlaContext _db = db;

    public async Task<Journey?> AddJourneyAsync(UpsertJourney journey)
    {
        var driverJrns = await _db.Journeys.Where(j =>
                                            j.DriverId == journey.DriverId &&
                                            (j.DepDate >= journey.DepDate && j.DepDate <= journey.ArrDate))
                                            .ToListAsync();

        if (driverJrns.Count > 0) return null;

        var vehicleJrns = await _db.Journeys.Where(j =>
                                            j.VehicleId == journey.VehicleId &&
                                            (j.DepDate >= journey.DepDate && j.DepDate <= journey.ArrDate))
                                            .ToListAsync();

        if (vehicleJrns.Count > 0) return null;

        var newJourney = new Journey()
        {
            DepDate = journey.DepDate,
            ArrDate = journey.ArrDate,
            DepCityId = journey.DepCityId,
            ArrCityId = journey.ArrCityId,
            DriverId = journey.DriverId,
            VehicleId = journey.VehicleId,
            JourneyType = journey.JourneyType,
            CompanyId = journey.CompanyId
        };

        await _db.Journeys.AddAsync(newJourney);
        await _db.SaveChangesAsync();

        return newJourney;
    }

    public async Task<int> DeleteJourneyAsync(int id)
    {
        var affRows = await _db.Journeys.Where(j => j.Id == id).ExecuteDeleteAsync();

        return affRows;
    }

    public async Task<List<Journey>> GetAllJourneysAsync()
    {
        var journeys = await _db.Journeys
                                .Include(j => j.ArrCity)
                                .Include(j => j.DepCity)
                                .Include(j => j.Driver)
                                .Include(j => j.Vehicle)
                                .Include(j => j.Company)
                                .ToListAsync();

        return journeys;
    }

    public async Task<Journey?> GetJourneyByIdAsync(int id)
    {
        var journey = await _db.Journeys.Where(j => j.Id == id)
                                        .Include(j => j.ArrCity)
                                        .Include(j => j.DepCity)
                                        .Include(j => j.Driver)
                                        .Include(j => j.Vehicle)
                                        .Include(j => j.Company)
                                        .FirstOrDefaultAsync();

        return journey;
    }

    public async Task<Journey?> UpdateJourneyAsync(int id, UpsertJourney journey)
    {
        var existJourney = await _db.Journeys.Where(j => j.Id == id).FirstOrDefaultAsync();

        if (existJourney is null) return null;

        var driverJrns = await _db.Journeys.Where(j =>
                                            j.DriverId == journey.DriverId &&
                                            (j.DepDate >= journey.DepDate && j.DepDate <= journey.ArrDate))
                                            .ToListAsync();

        if (driverJrns.Count > 0) return null;

        var vehicleJrns = await _db.Journeys.Where(j =>
                                            j.VehicleId == journey.VehicleId &&
                                            (j.DepDate >= journey.DepDate && j.DepDate <= journey.ArrDate))
                                            .ToListAsync();

        if (vehicleJrns.Count > 0) return null;

        existJourney.ArrCityId = journey.ArrCityId;
        existJourney.ArrDate = journey.ArrDate;
        existJourney.CompanyId = journey.CompanyId;
        existJourney.DepCityId = journey.DepCityId;
        existJourney.DepDate = journey.DepDate;
        existJourney.DriverId = journey.DriverId;
        existJourney.VehicleId = journey.VehicleId;
        existJourney.JourneyType = journey.JourneyType;

        await _db.SaveChangesAsync();

        return existJourney;
    }
}
