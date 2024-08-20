namespace Bonsai.Models;

public class UserData : UserRelatedData
{
    public bool GeoLocationUsageAcknowledged { get; set; }
    public byte BonsaiTreeStage { get; set; }
    public byte DaysBeforeWithering { get; set; }
    public DateTime? LastWatering { get; set; }
    public uint Streak { get; set; }
}