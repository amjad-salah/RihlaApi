using Backend.Data.Context;
using Backend.Data.DTOs.Country;
using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.CountryServices;

public class CountryServices(RihlaContext db) : ICountryServices
{
    private readonly RihlaContext _db = db;

    public async Task<List<Country>> GetAllCountriesAsync()
    {
        return await _db.Countries.ToListAsync();
    }

    public async Task<Country?> AddCountryAsync(UpsertCountry country)
    {
        var existingCountry = _db.Countries.FirstOrDefault(c => c.Name == country.Name);

        if (existingCountry != null) return null;
        
        var newCountry = new Country()
        {
            Name = country.Name,
            CountryCode = country.CountryCode,
        };
        
        await _db.Countries.AddAsync(newCountry);
        await _db.SaveChangesAsync();
        
        return newCountry;
    }

    public async Task<Country?> GetCountryByIdAsync(int id)
    {
        var country = await _db.Countries.Include(c => c.Cities)
                .FirstOrDefaultAsync(c => c.Id == id);
        
        return country;
    }

    public async Task<Country?> UpdateCountryAsync(int id, UpsertCountry country)
    {
        var existingCountry = await _db.Countries.FirstOrDefaultAsync(c => c.Id == id);
        
        if (existingCountry == null) return null;
        
        existingCountry!.Name = country.Name;
        existingCountry!.CountryCode = country.CountryCode;
        
        await _db.SaveChangesAsync();
        
        return existingCountry;
    }

    public async Task<int> DeleteCountryAsync(int id)
    {
        var affRows = await _db.Countries.Where(c => c.Id == id).ExecuteDeleteAsync();
        
        return affRows;
    }
}