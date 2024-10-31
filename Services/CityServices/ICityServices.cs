using Backend.Data.DTOs.City;
using Backend.Data.DTOs.Company;
using Backend.Data.Entities;

namespace Backend.Services.CityServices;

public interface ICityServices
{
    Task<List<City>> GetAllCitiesAsync();
    Task<City?> AddCityAsync(UpsertCity city);
    Task<City?> GetCityByIdAsync(int id);
    Task<City?> UpdateCityAsync(int id, UpsertCity city);
    Task<int> DeleteCityAsync(int id);
}