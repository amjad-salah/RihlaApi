using Backend.Data.DTOs.Journey;
using Backend.Data.Entities;

namespace Backend.Services.JourneyServices;

public interface IJourneyServices
{
    Task<List<Journey>> GetAllJourneysAsync();
    Task<Journey?> AddJourneyAsync(UpsertJourney journey);
    Task<Journey?> GetJourneyByIdAsync(int id);
    Task<Journey?> UpdateJourneyAsync(int id, UpsertJourney journey);
    Task<int> DeleteJourneyAsync(int id);
}
