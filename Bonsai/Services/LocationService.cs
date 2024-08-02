using AutoMapper;
using Bonsai.Models;
using System.Diagnostics;

namespace Bonsai.Services;

public class LocationService : ILocationService
{
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;    

    public LocationService(IFileService fileService, IMapper mapper)
    {
        _fileService = fileService;
        _mapper = mapper;
    }

    public async Task<LocationData?> GetCurrentLocationAsync()
    {
        var lastLocation = await _fileService.ReadUserDataAsync<LocationData>("location");
        var timeDifference = DateTime.Now - lastLocation?.LastUpdate;

        if (timeDifference?.TotalHours < 1)
        {
            return lastLocation;
        }

        LocationData? location = null;

        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(30));
            var result = await Geolocation.Default.GetLocationAsync(request);

            if (result != null)
            {
                location = _mapper.Map<LocationData>(result);
                await _fileService.UpdateUserDataAsync<LocationData>("location", location);
            }
        }

        catch (Exception exception)
        {
            Debug.WriteLine(exception);
        }

        return location;
    }
}