using AutoMapper;
using Bonsai.Models;
using Location = Microsoft.Maui.Devices.Sensors.Location;

namespace Bonsai.AutoMapper.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, LocationData>();
    }
}