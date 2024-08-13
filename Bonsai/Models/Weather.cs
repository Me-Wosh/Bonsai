namespace Bonsai.Models;

public class Weather : UserRelatedDataTemplate
{
    public string WeatherType { get; set; } = Models.WeatherType.Sunny;
}