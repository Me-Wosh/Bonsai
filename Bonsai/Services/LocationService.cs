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

    public async Task<GeoLocationResponse> GetCurrentLocationAsync()
    {
        var lastLocation = await _fileService.ReadUserDataAsync<GeoLocation>("location");
        var timeDifference = DateTime.Now - lastLocation?.LastUpdate;

        if (timeDifference?.TotalHours < 1)
        {
            return new GeoLocationResponse(GeoLocationResponseStatus.Success, lastLocation);
        }

        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(30));
            var result = await Geolocation.Default.GetLocationAsync(request);

            if (result != null)
            {
                var location = _mapper.Map<GeoLocation>(result);
                await _fileService.UpdateUserDataAsync<GeoLocation>("location", location);
                
                return new GeoLocationResponse(GeoLocationResponseStatus.Success, location);
            }
        }

        catch (FeatureNotSupportedException)
        {
            return new GeoLocationResponse(GeoLocationResponseStatus.NotSupported);
        }

        catch(FeatureNotEnabledException)
        {
            return new GeoLocationResponse(GeoLocationResponseStatus.NotEnabled);
        }

        catch(PermissionException)
        {
            return new GeoLocationResponse(GeoLocationResponseStatus.NoPermission);
        }

        catch (Exception exception)
        {
            Debug.WriteLine(exception);
        }

        return new GeoLocationResponse(GeoLocationResponseStatus.Fail);
    }
}