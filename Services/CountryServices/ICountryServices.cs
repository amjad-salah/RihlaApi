using Backend.Data.DTOs.Country;
using Backend.Data.Entities;

namespace Backend.Services.CountryServices;

public interface ICountryServices
{
    Task<List<Country>> GetAllCountriesAsync();
    Task<Country?> AddCountryAsync(UpsertCountry country);
    Task<Country?> GetCountryByIdAsync(int id);
    Task<Country?> UpdateCountryAsync(int id, UpsertCountry country);
    Task<int> DeleteCountryAsync(int id);
}