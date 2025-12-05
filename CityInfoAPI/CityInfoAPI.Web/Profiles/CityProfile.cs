using AutoMapper;
using CityInfoAPI.Data.Entities;
using CityInfoAPI.Dtos;
using CityInfoAPI.Dtos.Models;

namespace CityInfoAPI.Web.Profiles
{
    #pragma warning disable CS1591
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            // source, destination
            CreateMap<City, CityWithoutPointsOfInterestDto>();
            CreateMap<City, CityDto>();
            CreateMap<City, CityCreateDto>();
            CreateMap<CityCreateDto, City>();
            CreateMap<CityUpdateDto, City>();
            CreateMap<City, CityUpdateDto>();
            CreateMap<CityDto, CityWithoutPointsOfInterestDto>();
            CreateMap<CityUpdateDto, CityDto>();
            CreateMap<CityDto, CityUpdateDto>();
        }
    }
    #pragma warning restore CS1591
}
