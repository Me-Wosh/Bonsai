using Bonsai.Models;
using System.Text.Json;

namespace Bonsai.Services
{
    public interface IFileService
    {
        Task<JsonElement?> ReadRawDataAsync(string jsonFileName);
        Task<T?> ReadUserDataAsync<T>(string jsonFileName) where T : UserRelatedDataTemplate;
        Task UpdateUserDataAsync<T>(string jsonFileName, T userData) where T : UserRelatedDataTemplate;
    }
}