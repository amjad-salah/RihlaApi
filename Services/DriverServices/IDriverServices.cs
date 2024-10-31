using Backend.Data.DTOs.Driver;
using Backend.Data.Entities;

namespace Backend.Services.DriverServices;

public interface IDriverServices
{
    Task<List<Driver>> GetAllDriversAsync();
    Task<Driver?> AddDriverAsync(UpsertDriver driver);
    Task<Driver?> GetDriverByIdAsync(int id);
    Task<Driver?> UpdateDriverAsync(int id, UpsertDriver driver);
    Task<int> DeleteDriverAsync(int id);
}