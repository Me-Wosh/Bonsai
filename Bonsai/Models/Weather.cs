namespace Bonsai.Models;

public class Weather : UserData
{
    public string WeatherType { get; set; } = Models.WeatherType.Sunny;
}