namespace Bonsai.Models;

public abstract class UserData
{
    public DateTime? LastUpdate { get; set; }

    protected UserData()
    {
        LastUpdate = DateTime.Now;
    }
}