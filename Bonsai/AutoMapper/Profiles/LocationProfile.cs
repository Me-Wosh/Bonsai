using AutoMapper;
using Bonsai.Models;

namespace Bonsai.AutoMapper.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, GeoLocation>();
    }
}