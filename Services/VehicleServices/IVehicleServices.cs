using Backend.Data.DTOs.Vehicle;
using Backend.Data.Entities;

namespace Backend.Services.VehicleServices;

public interface IVehicleServices
{
    Task<List<Vehicle>> GetAllVehiclesAsync();
    Task<Vehicle?> AddVehicleAsync(UpsertVehicle vehicle);
    Task<Vehicle?> GetVehicleByIdAsync(int id);
    Task<Vehicle?> UpdateVehicleAsync(int id, UpsertVehicle vehicle);
    Task<int> DeleteVehicleAsync(int id);
}