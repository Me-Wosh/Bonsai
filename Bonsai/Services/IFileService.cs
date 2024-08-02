using Bonsai.Models;

namespace Bonsai.Services
{
    public interface IFileService
    {
        Task<T?> ReadUserDataAsync<T>(string jsonFileName) where T : UserData;
        Task UpdateUserDataAsync<T>(string jsonFileName, T userData) where T : UserData;
    }
}