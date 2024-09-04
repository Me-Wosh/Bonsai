using Bonsai.Models;
using System.Text.Json.Nodes;

namespace Bonsai.Services
{
    public interface IFileService
    {
        Task<JsonNode?> ReadRawDataAsync(string fileName);
        Task<T?> ReadUserDataAsync<T>(string fileName) where T : UserData;
        Task UpdateUserDataAsync<T>(string fileName, T userData) where T : UserData;
        User LoadUser();
    }
}