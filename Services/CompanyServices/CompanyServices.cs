using Backend.Data.Context;
using Backend.Data.DTOs.Company;
using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.CompanyServices;

public class CompanyServices(RihlaContext db) : ICompanyServices
{
    private readonly RihlaContext _db = db;

    public async Task<List<Company>> GetAllCompaniesAsync()
    {
        var companies = await _db.Companies
                .Include(c => c.Country).ToListAsync();
        
        return companies;
    }

    public async Task<Company?> AddCompanyAsync(UpsertCompany company)
    {
        var existingCompany = await _db.Companies.FirstOrDefaultAsync(c => c.Name == company.Name);
        
        if (existingCompany != null) return null;
        
        var newCompany = new Company()
        {
            Name = company.Name,
            CountryId = company.CountryId,
            Address = company.Address,
            PhoneNo = company.PhoneNo,
            Email = company.Email,
        };
        
        await _db.Companies.AddAsync(newCompany);
        await _db.SaveChangesAsync();
        
        return newCompany;
    }

    public Task<Company?> GetCompanyByIdAsync(int id)
    {
        var company = _db.Companies
                .Include(c => c.Journeys)
                .Include(c => c.Drivers)
                .Include(c => c.Vehicles)
                .Include(c => c.Country)
                .FirstOrDefaultAsync(c => c.Id == id);

        return company;
    }

    public async Task<Company?> UpdateCompanyAsync(int id, UpsertCompany company)
    {
        var existingCompany = await _db.Companies.FirstOrDefaultAsync(c => c.Id == id);
        
        if (existingCompany == null) return null;
        existingCompany!.Name = company.Name;
        existingCompany!.CountryId = company.CountryId;
        existingCompany!.Address = company.Address;
        existingCompany!.PhoneNo = company.PhoneNo;
        existingCompany!.Email = company.Email;
        
        await _db.SaveChangesAsync();
        
        return existingCompany;
    }

    public async Task<int> DeleteCompanyAsync(int id)
    {
        var affRows = await _db.Companies.Where(c => c.Id == id).ExecuteDeleteAsync();
        return affRows;
    }
}