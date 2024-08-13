namespace Bonsai.Models;

public class WeatherResponse
{
    public LocationResponse Location { get; set; }
    public Current Current { get; set; }
}
public class Condition
{
    public string Text { get; set; }
}

public class Current
{
    public Condition Condition { get; set; }
}

public class LocationResponse
{
    public string Name { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string TzId { get; set; }
    public int LocaltimeEpoch { get; set; }
    public string Localtime { get; set; }
}