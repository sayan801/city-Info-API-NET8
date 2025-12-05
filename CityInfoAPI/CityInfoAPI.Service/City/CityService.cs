using AutoMapper;
using CityInfoAPI.Data.Entities;
using CityInfoAPI.Data.Repositories;
using CityInfoAPI.Dtos;
using CityInfoAPI.Dtos.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CityInfoAPI.Service
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _repo;
		private readonly IStatesRepository _stateRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CityService> _logger;

        public CityService(ICityRepository repo,IStatesRepository statesRepository, IMapper mapper, ILogger<CityService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
			_stateRepo = statesRepository;
        }

        public async Task<IEnumerable<CityWithoutPointsOfInterestDto>> GetCitiesAsync(string name, string search, int pageNumber, int pageSize)
        {
            try
            {
                var cities = await _repo.GetCitiesAsync(name, search, pageNumber, pageSize);
                var results = _mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cities);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting cities. {ex}");
                throw;
            }
        }

        public async Task<IEnumerable<CityWithoutPointsOfInterestDto>> GetAllCitiesAsync()
        {
            try
            {
                var cities = await _repo.GetCitiesUnsortedAsync();
                var results = _mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cities);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting all cities. {ex}");
                throw;
            }
        }

        public async Task<bool> CityExistsAsync(Guid cityGuid)
        {
            try
            {
                return await _repo.CityExistsAsync(cityGuid);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while checking if city exists. {ex}");
                throw;
            }
        }

        public async Task<CityDto?> GetCityAsync(Guid cityGuid, bool includePointsOfInterest)
        {
            try
            {
                var city = await _repo.GetCityAsync(cityGuid, includePointsOfInterest);
                var results = _mapper.Map<CityDto>(city);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching city. {ex}");
                throw;
            }
        }

        public async Task<CityWithoutPointsOfInterestDto?> GetCityWithoutPointsOfInterestAsync(Guid cityGuid, bool includePointsOfInterest)
        {
            try
            {
                var city = await _repo.GetCityAsync(cityGuid, includePointsOfInterest);
                var results = _mapper.Map<CityWithoutPointsOfInterestDto>(city);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching city without points of interest. {ex}");
                throw;
            }
        }

        public async Task<CityDto?> CreateCityAsync(CityCreateDto request)
        {
            try
            {
                var newCityEntity = _mapper.Map<City>(request);
				State? state = await _stateRepo.GetStateByNameAsync(request.StateName);
				if(state == null)
				{
					return null;
				} 
				newCityEntity.StateId = state.Id;

                // add it to memory.
                await _repo.CreateCityAsync(newCityEntity);

                // if the city has POIs from the request, handle those. update the city guids.
                if (newCityEntity.PointsOfInterest.Any())
                {
                    foreach (var poi in newCityEntity.PointsOfInterest)
                    {
                        poi.CityGuid = newCityEntity.CityGuid;
                    }
                }

                // save it
                bool success = await SaveChangesAsync();

                if (!success)
                {
                    return null;
                }

                // map new entity to dto and return it
                CityDto newCityDto = _mapper.Map<CityDto>(newCityEntity);

                return newCityDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred when creating a city. Request: {JsonConvert.SerializeObject(request)}: Error: {ex}");
                throw;
            }
        }

        public async Task<CityDto?> UpdateCityAsync(CityUpdateDto request, Guid cityGuid)
        {
            try
            {
                var city = await _repo.GetCityAsync(cityGuid, false);

                // map the request - override the values of the destination object w/ source
                _mapper.Map(request, city);

                var success = await SaveChangesAsync();

                if (!success)
                {
                    return null;
                }

                // map new entity to dto and return it
                CityDto updatedCityDto = _mapper.Map<CityDto>(request);

                return updatedCityDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred when updating city. Request: {JsonConvert.SerializeObject(request)}: Error: {ex}");
                throw;
            }
        }

        public async Task<bool> DeleteCityAsync(Guid cityGuid)
        {
            try
            {
                // delete the city
                await _repo.DeleteCityAsync(cityGuid);
                var results = await SaveChangesAsync();
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred when deleting city {cityGuid}. Error: {ex}");
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
}
