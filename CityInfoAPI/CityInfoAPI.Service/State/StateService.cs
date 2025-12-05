using AutoMapper;
using CityInfoAPI.Data.Entities;
using CityInfoAPI.Data.Repositories;
using CityInfoAPI.Dtos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CityInfoAPI.Service;

public class StateService : IStateService
{
    private readonly IStatesRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<StateService> _logger;

    public StateService(IStatesRepository repo, IMapper mapper, ILogger<StateService> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<StateDto>> GetStatesAsync()
    {
        try
        {
            var states = await _repo.GetStatesAsync();
            var results = _mapper.Map<IEnumerable<StateDto>>(states);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting states. {ex}");
            throw;
        }
    }

    public async Task<StateDto?> GetStateAsync(string stateCode)
    {
        try
        {
            var state = await _repo.GetStateAsync(stateCode);
            var results = _mapper.Map<StateDto>(state);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while fetching state. {ex}");
            throw;
        }
    }

    public async Task<StateDto?> GetStateByNameAsync(string stateName)
    {
        try
        {
            var state = await _repo.GetStateByNameAsync(stateName);
            var results = _mapper.Map<StateDto>(state);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while fetching state. {ex}");
            throw;
        }
    }

    public async Task<bool> StateExistsAsync(string stateCode)
    {
        try
        {
            var state = await _repo.GetStateAsync(stateCode);
            return state != null;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while fetching state. {ex}");
            throw;
        }
    }

     public async Task<bool> StateExistsByNameAsync(string stateName)
    {
        try
        {
            var state = await _repo.GetStateByNameAsync(stateName);
            return state != null;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while fetching state. {ex}");
            throw;
        }
    }

    public async Task<StateDto?> CreateStateAsync(CityCreateDto request)
    {
        try
        {
            //var newState = _mapper.Map<StateDto>(request.StateName);
            State newState = new State();
            newState.Name = request.StateName;
            newState.StateGuid = Guid.NewGuid();
            newState.StateCode = request.StateCode;
            // add it to memory.
            await _repo.CreateStateAsync(newState);
            // save it
            bool success = await SaveChangesAsync();

            if (!success)
            {
                return null;
            }

            StateDto newStateDto = _mapper.Map<StateDto>(newState);

            return newStateDto;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred when creating a state. Request: {JsonConvert.SerializeObject(request)}: Error: {ex}");
            throw;
        }
    }
    public async Task<bool> SaveChangesAsync()
    {
        try
        {
            return await _repo.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while saving changes. {ex}");
            throw;
        }
    }
}
