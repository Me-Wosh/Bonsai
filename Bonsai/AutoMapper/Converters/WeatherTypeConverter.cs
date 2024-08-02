using AutoMapper;
using Bonsai.Models;

namespace Bonsai.AutoMapper.Converters;

public class WeatherTypeConverter : IValueConverter<string, string>
{
    public string Convert(string sourceMember, ResolutionContext context)
    {
        if (sourceMember.Contains("thunder", StringComparison.CurrentCultureIgnoreCase))
        {
            return WeatherType.Thundery;
        }

        if (sourceMember.Contains("partly cloudy", StringComparison.CurrentCultureIgnoreCase))
        {
            return WeatherType.PartlyCloudy;
        }

        if (sourceMember.Contains("cloudy", StringComparison.CurrentCultureIgnoreCase) ||
            sourceMember.Contains("fog", StringComparison.CurrentCultureIgnoreCase) ||
            sourceMember.Contains("mist", StringComparison.CurrentCultureIgnoreCase) ||
            sourceMember.Contains("overcast", StringComparison.CurrentCultureIgnoreCase))
        {
            return WeatherType.Cloudy;
        }

        if (sourceMember.Contains("drizzle", StringComparison.CurrentCultureIgnoreCase) ||
            sourceMember.Contains("rain", StringComparison.CurrentCultureIgnoreCase) ||
            sourceMember.Contains("sleet", StringComparison.CurrentCultureIgnoreCase))
        {
            return WeatherType.Rainy;
        }

        if (sourceMember.Contains("blizzard", StringComparison.CurrentCultureIgnoreCase) ||
            sourceMember.Contains("ice pellets", StringComparison.CurrentCultureIgnoreCase) ||
            sourceMember.Contains("snow", StringComparison.CurrentCultureIgnoreCase))
        {
            return WeatherType.Snowy;
        }

        if (sourceMember.Contains("clear", StringComparison.CurrentCultureIgnoreCase) ||
            sourceMember.Contains("sunny", StringComparison.CurrentCultureIgnoreCase))
        {
            return WeatherType.Sunny;
        }

        return WeatherType.Sunny;
    }
}