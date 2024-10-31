using Backend.Data.Context;
using Backend.Data.DTOs.Driver;
using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.DriverServices;

public class DriverServices(RihlaContext db) : IDriverServices
{
    private readonly RihlaContext _db = db;

    public async Task<List<Driver>> GetAllDriversAsync()
    {
        var drivers = await _db.Drivers
            .Include(d => d.Company).ToListAsync();
        
        return drivers;
    }

    public async Task<Driver?> AddDriverAsync(UpsertDriver driver)
    {
        var existingDriver = await _db.Drivers
            .Where(d => d.LicenseNo == driver.LicenseNo)
            .FirstOrDefaultAsync();

        if (existingDriver != null) return null;

        var newDriver = new Driver()
        {
            LicenseNo = driver.LicenseNo,
            Name = driver.Name,
            CompanyId = driver.CompanyId,
            PhoneNo = driver.PhoneNo,
        };
        
        await _db.Drivers.AddAsync(newDriver);
        await _db.SaveChangesAsync();
        
        return newDriver;
    }

    public async Task<Driver?> GetDriverByIdAsync(int id)
    {
        var driver = await _db.Drivers.Where(d => d.Id == id)
            .Include(d => d.Company)
            .Include(d => d.Journeys)
            .FirstOrDefaultAsync();
        
        return driver;
    }

    public async Task<Driver?> UpdateDriverAsync(int id, UpsertDriver driver)
    {
        var existingDriver = await _db.Drivers
            .FirstOrDefaultAsync();

        if (existingDriver == null) return null;
        
        existingDriver.LicenseNo = driver.LicenseNo;
        existingDriver.Name = driver.Name;
        existingDriver.PhoneNo = driver.PhoneNo;
        existingDriver.CompanyId = driver.CompanyId;
        
        await _db.SaveChangesAsync();
        
        return existingDriver;
    }

    public async Task<int> DeleteDriverAsync(int id)
    {
        var affRows = await _db.Drivers.Where(d => d.Id == id).ExecuteDeleteAsync();
        
        return affRows;
    }
}