using Bonsai.Models;
using System.Text.Json;

namespace Bonsai.Services;

public class FileService : IFileService
{
    public async Task<T?> ReadUserDataAsync<T>(string jsonFileName) where T : UserData
    {
        var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, $"{jsonFileName}.json");

        if (!File.Exists(filePath))
        {
            return null;
        }

        await using var fileStream = File.OpenRead(filePath);

        var userData = await JsonSerializer.DeserializeAsync<T>(fileStream);

        return userData;
    }

    public async Task UpdateUserDataAsync<T>(string jsonFileName, T userData) where T : UserData
    {
        var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, $"{jsonFileName}.json");
        await using var fileStream = File.Create(filePath);

        userData.LastUpdate = DateTime.Now;

        await JsonSerializer.SerializeAsync(fileStream, userData);
    }
}
