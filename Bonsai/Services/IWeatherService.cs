using Bonsai.Models;

namespace Bonsai.Services;

public interface IWeatherService
{
    Task<WeatherData> GetWeatherAsync(LocationData? locationDto);
}