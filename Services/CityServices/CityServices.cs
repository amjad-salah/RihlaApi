using Backend.Data.Context;
using Backend.Data.DTOs.City;
using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.CityServices;

public class CityServices(RihlaContext db) : ICityServices
{
    private readonly RihlaContext _db = db;

    public async Task<List<City>> GetAllCitiesAsync()
    {
        var cities = await _db.Cities.Include(c => c.Country).ToListAsync();
        
        return cities;
    }

    public async Task<City?> AddCityAsync(UpsertCity city)
    {
        var existingCity = await _db.Cities.FirstOrDefaultAsync(c => 
            c.Name == city.Name && c.CountryId == city.CountryId);

        if (existingCity != null) return null;

        var newCity = new City()
        {
            Name = city.Name,
            CountryId = city.CountryId
        };
        
        await _db.Cities.AddAsync(newCity);
        await _db.SaveChangesAsync();
        
        return newCity;
    }

    public async Task<City?> GetCityByIdAsync(int id)
    {
        var city = await _db.Cities.Where(c => c.Id == id)
            .Include(c => c.Country)
            .Include(c => c.ArrJourneys)
            .Include(c => c.DepJourneys)
            .FirstOrDefaultAsync();

        return city;
    }

    public async Task<City?> UpdateCityAsync(int id, UpsertCity city)
    {
        var existingCity = await _db.Cities.FirstOrDefaultAsync(c => c.Id == id);

        if (existingCity == null) return null;
        
        existingCity.Name = city.Name;
        existingCity.CountryId = city.CountryId;
        
        await _db.SaveChangesAsync();
        
        return existingCity;
    }

    public async Task<int> DeleteCityAsync(int id)
    {
        var affRows = await _db.Cities.Where(c => c.Id == id).ExecuteDeleteAsync();
        
        return affRows;
    }
}