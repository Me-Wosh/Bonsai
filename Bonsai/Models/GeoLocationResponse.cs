namespace Bonsai.Models;

public class GeoLocationResponse
{
    public GeoLocationResponseStatus Status { get; set; }
    public GeoLocation? GeoLocation { get; set; }

    public GeoLocationResponse()
    {

    }

    public GeoLocationResponse(GeoLocationResponseStatus status, GeoLocation? geoLocation = null)
    {
        Status = status;
        GeoLocation = geoLocation;
    } 
}

public enum GeoLocationResponseStatus
{
    Success,
    NotSupported,
    NotEnabled,
    NoPermission,
    Fail
}