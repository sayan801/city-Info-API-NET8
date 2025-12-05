using CityInfoAPI.Dtos;
using CityInfoAPI.Dtos.Models;

namespace CityInfoAPI.Service
{
    public interface ICityService
    {
        Task<IEnumerable<CityWithoutPointsOfInterestDto>> GetCitiesAsync(string name, string search, int pageNumber, int pageSize);

        Task<IEnumerable<CityWithoutPointsOfInterestDto>> GetAllCitiesAsync();

        Task<bool> CityExistsAsync(Guid cityGuid);

        Task<CityDto?> GetCityAsync(Guid cityGuid, bool includePointsOfInterest);

        Task<CityWithoutPointsOfInterestDto?> GetCityWithoutPointsOfInterestAsync(Guid cityGuid, bool includePointsOfInterest);

        Task<CityDto?> CreateCityAsync(CityCreateDto request);

        Task<CityDto?> UpdateCityAsync(CityUpdateDto request, Guid cityGuid);

        Task<bool> DeleteCityAsync(Guid cityGuid);

        Task<bool> SaveChangesAsync();
    }
}