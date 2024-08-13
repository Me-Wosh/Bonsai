using AutoMapper;
using Bonsai.AutoMapper.Converters;
using Bonsai.Models;

namespace Bonsai.AutoMapper.Profiles;

public class WeatherProfile : Profile
{
    public WeatherProfile()
    {
        CreateMap<WeatherResponse, Weather>()
            .ForMember(
                dest => dest.WeatherType,
                opt => opt.ConvertUsing(new WeatherTypeConverter(), src => src.Current.Condition.Text));
    }
}