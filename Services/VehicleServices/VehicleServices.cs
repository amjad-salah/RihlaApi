using Backend.Data.Context;
using Backend.Data.DTOs.Vehicle;
using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.VehicleServices;

public class VehicleServices(RihlaContext db) : IVehicleServices
{
    private readonly RihlaContext _db = db;

    public async Task<List<Vehicle>> GetAllVehiclesAsync()
    {
        var vehicles = await _db.Vehicles.Include(v => v.Company).ToListAsync();
        
        return vehicles;
    }

    public async Task<Vehicle?> AddVehicleAsync(UpsertVehicle vehicle)
    {
        var existingVehicle = await _db.Vehicles.FirstOrDefaultAsync(v => v.PlateNo == vehicle.PlateNo);

        if (existingVehicle is not null) return null;

        var newVehicle = new Vehicle()
        {
            PlateNo = vehicle.PlateNo,
            Model = vehicle.Model,
            TotalWeight = vehicle.TotalWeight,
            NoOfSeats = vehicle.NoOfSeats,
            CompanyId = vehicle.CompanyId,
            VehicleType = vehicle.VehicleType
        };
        
        await _db.Vehicles.AddAsync(newVehicle);
        await _db.SaveChangesAsync();
        
        return newVehicle;
    }

    public async Task<Vehicle?> GetVehicleByIdAsync(int id)
    {
        var vehicle = await _db.Vehicles
            .Include(v => v.Company)
            .Include(v => v.Journeys)
            .FirstOrDefaultAsync(v => v.Id == id);
        
        return vehicle;
    }

    public async Task<Vehicle?> UpdateVehicleAsync(int id, UpsertVehicle vehicle)
    {
        var existingVehicle = await _db.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
        
        if (existingVehicle is null) return null;
        
        existingVehicle.PlateNo = vehicle.PlateNo;
        existingVehicle.Model = vehicle.Model;
        existingVehicle.TotalWeight = vehicle.TotalWeight;
        existingVehicle.NoOfSeats = vehicle.NoOfSeats;
        existingVehicle.CompanyId = vehicle.CompanyId;
        existingVehicle.VehicleType = vehicle.VehicleType;
        
        await _db.SaveChangesAsync();
        
        return existingVehicle;
    }

    public async Task<int> DeleteVehicleAsync(int id)
    {
        var affRows = await _db.Vehicles.Where(v => v.Id == id).ExecuteDeleteAsync();
        
        return affRows;
    }
}