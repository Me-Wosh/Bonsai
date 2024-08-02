namespace Bonsai.Models;

public class WeatherData : UserData
{
    public string WeatherType { get; set; } = Models.WeatherType.Sunny;
}