using CityInfoAPI.Data.Entities;

namespace CityInfoAPI.Data.Repositories;

public interface IStatesRepository
{
    Task<IEnumerable<State>> GetStatesAsync();

    Task<State?> GetStateAsync(string stateCode);
    
    Task<State?> GetStateByNameAsync(string stateName);
    Task<State?> CreateStateAsync(State newState);
    Task<bool> SaveChangesAsync();
}
