using Bonsai.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Bonsai.Services;

public class FileService : IFileService
{
    public async Task<JsonNode> ReadRawDataAsync(string fileName)
    {
        await using var fileStream = await FileSystem.Current.OpenAppPackageFileAsync(fileName);
        var json = await JsonNode.ParseAsync(fileStream);

        return json!;
    }

    public async Task<T> ReadUserDataAsync<T>(string fileName) where T : UserData
    {
        var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
        await using var fileStream = File.OpenRead(filePath);
        var userData = await JsonSerializer.DeserializeAsync<T>(fileStream);

        return userData!;
    }

    public async Task UpdateUserDataAsync<T>(string fileName, T userData) where T : UserData
    {
        var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
        await using var fileStream = File.Create(filePath);

        userData.LastUpdate = DateTime.Now;

        await JsonSerializer.SerializeAsync(fileStream, userData);
    }

    public User LoadUser()
    {
        var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, Files.User);

        if (!File.Exists(filePath))
        {
            var newUser = new User();

            using var fileStream = File.Create(filePath);
            JsonSerializer.Serialize(fileStream, newUser);

            return newUser;
        }

        var json = File.ReadAllText(filePath);

        var user = JsonSerializer.Deserialize<User>(json);

        return user!;
    }
}