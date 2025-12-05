using AutoMapper;
using CityInfoAPI.Data.Entities;
using CityInfoAPI.Dtos;

namespace CityInfoAPI.Web.Profiles;

#pragma warning disable CS1591
public class StateProfile : Profile
{
    public StateProfile()
    {
        // source, destination
        CreateMap<State, StateDto>();
    }
}
#pragma warning restore CS1591

