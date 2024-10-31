using Backend.Data.DTOs.Company;
using Backend.Data.Entities;

namespace Backend.Services.CompanyServices;

public interface ICompanyServices
{
    Task<List<Company>> GetAllCompaniesAsync();
    Task<Company?> AddCompanyAsync(UpsertCompany company);
    Task<Company?> GetCompanyByIdAsync(int id);
    Task<Company?> UpdateCompanyAsync(int id, UpsertCompany company);
    Task<int> DeleteCompanyAsync(int id);
}