namespace Bonsai.Models;

public class WeatherResponse
{
    public required LocationResponse Location { get; init; }

    public required Current Current { get; init; }
}
public class Condition
{
    public string Text { get; init; } = "";
}

public class Current
{
    public required Condition Condition { get; init; }
}

public class LocationResponse
{
    public string Name { get; init; } = "";
    public string Region { get; init; } = "";
    public string Country { get; init; } = "";
    public double Lat { get; init; }
    public double Lon { get; init; }
    public string TzId { get; init; } = "";
    public int LocaltimeEpoch { get; init; }
    public string Localtime { get; init; } = "";
}