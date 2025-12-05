using CityInfoAPI.Dtos;
using CityInfoAPI.Data.Entities;

namespace CityInfoAPI.Service;

public interface IStateService
{
    Task<IEnumerable<StateDto>> GetStatesAsync();

    Task<StateDto?> GetStateAsync(string stateCode);
    Task<StateDto?> GetStateByNameAsync(string stateName);

    Task<bool> StateExistsAsync(string stateCode);
    Task<bool> StateExistsByNameAsync(string stateName);
    Task<StateDto?> CreateStateAsync(CityCreateDto request);
    Task<bool> SaveChangesAsync();
}