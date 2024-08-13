using Bonsai.Models;

namespace Bonsai.Services;

public interface IWeatherService
{
    Task<Weather> GetWeatherAsync(GeoLocation? locationDto);
}