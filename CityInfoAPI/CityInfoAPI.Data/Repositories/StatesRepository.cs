using CityInfoAPI.Data.DbContents;
using CityInfoAPI.Data.Entities;
using CityInfoAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CityInfoAPI.Data.Repositories;

public class StatesRepository : IStatesRepository
{
    private readonly CityInfoDbContext _dbContext;
    private readonly ILogger<StatesRepository> _logger;

    public StatesRepository(CityInfoDbContext dbContext, ILogger<StatesRepository> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException();
        _logger = logger ?? throw new ArgumentNullException();
    }

    public async Task<IEnumerable<State>> GetStatesAsync()
    {
        try
        {
            var results = await _dbContext.States.AsNoTracking().ToListAsync();
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting states. {ex}");
            throw;
        }
    }

    public async Task<State?> GetStateAsync(string stateCode)
    {
        try
        {
            return await _dbContext.States.AsNoTracking().Where(s => s.StateCode.ToLower() == stateCode.ToLower()).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting state: {stateCode}. {ex}");
            throw;
        }
    }

    public async Task<State?> GetStateByNameAsync(string stateName)
    {
        try
        {
           return await _dbContext.States.AsNoTracking().Where(s => s.Name.ToLower() == stateName.ToLower()).FirstOrDefaultAsync();
            
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting state: {stateName}. {ex}");
            throw;
        }
    }

     public async Task<State?> CreateStateAsync(State newState)
    {
        try
        {
            await _dbContext.States.AddAsync(newState);
            return newState;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while creating city. {ex}");
            throw;
        }
    }

    public async Task<bool> SaveChangesAsync()
    {
        try
        {
            var changes = await _dbContext.SaveChangesAsync();
            return changes >= 0;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while saving state. {ex}");
            throw;
        }
    }
}
