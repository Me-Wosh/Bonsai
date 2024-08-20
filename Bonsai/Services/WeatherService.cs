using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Json;
using AutoMapper;
using Bonsai.Models;

namespace Bonsai.Services;

public class WeatherService : IWeatherService
{
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private static readonly HttpClient _httpClient = new();

    public WeatherService(IFileService fileService, IMapper mapper)
    {
        _fileService = fileService; 
        _mapper = mapper;
    }

    public async Task<Weather> GetWeatherAsync(GeoLocation? location)
    {
        var lastWeather = await _fileService.ReadUserDataAsync<Weather>("weather");
        var timeDifference = DateTime.Now - lastWeather?.LastUpdate;

        if (timeDifference?.TotalHours < 1)
        {
            return lastWeather!;
        }

        var weather = new Weather();

        if (location == null)
        {
            return weather;
        }

        const string apiKey = "61a2fa81deba44928bc94110240407";

        var builder = new UriBuilder("https://api.weatherapi.com/v1/forecast.json");
        builder.Query += $"key={apiKey}";
        builder.Query += $"&q={location.Latitude.ToString(CultureInfo.InvariantCulture)},{location.Longitude.ToString(CultureInfo.InvariantCulture)}";
        builder.Query += "&days=1";

        try
        {
            var response = await _httpClient.GetAsync(builder.Uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<WeatherResponse>();
                weather = _mapper.Map<Weather>(content);
                await _fileService.UpdateUserDataAsync<Weather>("weather", weather);
            }
        }

        catch (Exception exception)
        {
            Debug.WriteLine(exception);
        }

        return weather;
    }
}