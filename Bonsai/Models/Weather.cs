namespace Bonsai.Models;

public class Weather : UserData
{
    private string _weatherType = Models.WeatherType.Sunny;

    public string WeatherType
    {
        get => _weatherType;
     
        set
        {
            _weatherType = value;
            LastUpdate = DateTime.Now;
        }
    }
}