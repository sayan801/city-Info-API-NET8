using AutoMapper;
using CityInfoAPI.Data;
using CityInfoAPI.Data.Repositories;
using CityInfoAPI.Dtos.Models;
using CityInfoAPI.Service;
using CityInfoAPI.Web.Controllers.RequestHelpers.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace CityInfoAPI.Test.Tests
{
    public class CityServiceTests
    {

        private CityService _cityService;
        private readonly Mock<ICityRepository> _repo = new Mock<ICityRepository>();
        private readonly Mock<IStatesRepository> _stateRepo = new Mock<IStatesRepository>();
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<CityService>> _logger = new Mock<ILogger<CityService>>();
        private readonly Guid _cityGuid = Guid.Parse("38276231-1918-452d-a3e9-6f50873a95d2");
        private readonly Guid _invalidCityGuid = Guid.Parse("e5a5f605-627d-4aec-9f5c-e9939ea0a6cf");

        public CityServiceTests()
        {
            _mapper = SetUpAutoMapper.SetUp();
            _cityService = new CityService(_repo.Object,_stateRepo.Object, _mapper, _logger.Object);
        }

        [Fact]
        [Trait("Category","City Service Tests")]
        public async Task GetCitiesAsync_RequestingCities_ReturnsListOfCityWithoutPointOfInterestDtos()
        {
            // arrange. build moq'd repo response
            var cities = new CityInfoTestEntityData().Cities;
            _repo.Setup(x => x.GetCitiesAsync(string.Empty, string.Empty, 1, 100)).ReturnsAsync(cities);

            // act
            var cityDtos = await _cityService.GetCitiesAsync(string.Empty, string.Empty, 1, 100);

            // assert
            Assert.True(cityDtos.Count() > 0, "GetCitiesAsync did not return any Cities.");
        }

        [Fact]
        [Trait("Category","City Service Tests")]
        public async Task GetCities_RequestingCities_ReturnsListOfTypeCityWithoutPointOfInterestDtos()
        {
            // arrange. build moq'd response
            var cities = new CityInfoTestEntityData().Cities;
            _repo.Setup(x => x.GetCitiesAsync(string.Empty, string.Empty, 1, 100)).ReturnsAsync(cities);

            // act
            var cityDtos = await _cityService.GetCitiesAsync(string.Empty, string.Empty, 1, 100);

            // assert
            Assert.All(cityDtos, c => Assert.IsType<CityWithoutPointsOfInterestDto>(c));
        }

        [Fact]
        [Trait("Category","City Service Tests")]
        public async Task GetAllCities_RequestingCities_ReturnsListOfCityWithoutPointOfInterestDtos()
        {
            // arrange. build moq'd response
            var cities = new CityInfoTestEntityData().Cities;
            _repo.Setup(x => x.GetCitiesUnsortedAsync()).ReturnsAsync(cities);

            // act
            var cityDtos = await _cityService.GetAllCitiesAsync();

            // assert
            Assert.True(cityDtos.Count() > 0, "GetCitiesAsync did not return any Cities.");
        }

        [Fact]
        [Trait("Category","City Service Tests")]
        public async Task GetAllCities_RequestingCities_ReturnsListOfTypeCityWithoutPointOfInterestDtos()
        {
            // arrange. build moq'd response
            var cities = new CityInfoTestEntityData().Cities;
            _repo.Setup(x => x.GetCitiesUnsortedAsync()).ReturnsAsync(cities);

            // act
            var cityDtos = await _cityService.GetAllCitiesAsync();

            // assert
            Assert.All(cityDtos, c => Assert.IsType<CityWithoutPointsOfInterestDto>(c));
        }

        [Fact]
        [Trait("Category","City Service Tests")]
        public async Task CityExists_CityIfValidCityExists_ReturnsTrue()
        {
            // arrange. build moq'd response
            _repo.Setup(x => x.CityExistsAsync(_cityGuid)).ReturnsAsync(true);

            // act
            var response = await _cityService.CityExistsAsync(_cityGuid);

            // assert
            Assert.True(response);
        }

        [Fact]
        [Trait("Category","City Service Tests")]
        public async Task CityExists_CityIfInvalidCityExists_ReturnsFalse()
        {
            // arrange. build moq'd response
            _repo.Setup(x => x.CityExistsAsync(_invalidCityGuid)).ReturnsAsync(false);

            // act
            var response = await _cityService.CityExistsAsync(_cityGuid);

            // assert
            Assert.False(response);
        }

        [Fact]
        [Trait("Category","City Service Tests")]
        public async Task GetCitiesAsync_PagingWorksProperty_ProperNumberOfResultsAreSkipped()
        {
            // arrange
            var page = 2;
            var pageSize = 5;
            var nameFilter = string.Empty;
            var searchString = string.Empty;

            // should skip first 5
            var cities = new CityInfoTestEntityData().Cities.Skip((page - 1) * pageSize).Take(pageSize);
            _repo.Setup(x => x.GetCitiesAsync(nameFilter, searchString, page, pageSize)).ReturnsAsync(cities);

            // act
            var response = await _cityService.GetCitiesAsync(nameFilter, searchString, page, pageSize);

            // assert - should return 5
            Assert.True(response.Count() == 5);
        }

        [Fact]
        [Trait("Category","City Service Tests")]
        public async Task GetCitiesAsync_NameSearch_ProperlyFindsCitiesMatchingSearchCriteria()
        {
            // arrange
            var requestParams = new CityRequestParameters()
            {
                Search = "the"
            };
            var cities = new CityInfoTestEntityData().Cities.Where(c => c.Name.Contains(requestParams.Search) || c.Description.Contains(requestParams.Search)).Take(requestParams.PageSize);
            _repo.Setup(x => x.GetCitiesAsync(string.Empty, requestParams.Search, requestParams.PageNumber, requestParams.PageSize)).ReturnsAsync(cities);

            // act
            var response = await _cityService.GetCitiesAsync(string.Empty, requestParams.Search, requestParams.PageNumber, requestParams.PageSize);

            // assert - should return 4
            Assert.True(response.Count() == 4);
        }

        [Fact]
        [Trait("Category","City Service Tests")]
        public async Task GetCitiesAsync_NameFilter_ProperlyFindsCitiesMatchingName()
        {
            // arrange
            var requestParams = new CityRequestParameters()
            {
                Name = "Richmond (in memory)"
            };
            var cities = new CityInfoTestEntityData().Cities.Where(c => c.Name.ToLower().Equals(requestParams.Name.ToLower()));
            _repo.Setup(x => x.GetCitiesAsync(requestParams.Name.ToLower(), string.Empty, requestParams.PageNumber, requestParams.PageSize)).ReturnsAsync(cities);

            // act
            var response = await _cityService.GetCitiesAsync(requestParams.Name.ToLower(), string.Empty, requestParams.PageNumber, requestParams.PageSize);

            // assert - should return
            Assert.True(response.Count() == 1);
        }
    }
}