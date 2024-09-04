namespace Bonsai.Models;

public class GeoLocation : UserData
{
    private double _latitude;
    private double _longitude;

    public double Latitude 
    { 
        get => _latitude; 
        
        set
        { 
            _latitude = value;
            LastUpdate = DateTime.Now;
        } 
    }

    public double Longitude
    {
        get => _longitude;

        set
        {
            _longitude = value;
            LastUpdate = DateTime.Now;
        }
    }
}