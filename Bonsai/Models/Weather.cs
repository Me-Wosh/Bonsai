namespace Bonsai.Models;

public class Weather : UserRelatedData
{
    public string WeatherType { get; set; } = Models.WeatherType.Sunny;
}