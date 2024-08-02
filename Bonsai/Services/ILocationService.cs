using Bonsai.Models;

namespace Bonsai.Services;

public interface ILocationService
{
    Task<LocationData?> GetCurrentLocationAsync();
}