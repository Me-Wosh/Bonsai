namespace Bonsai.Models;

public class User : UserData
{
    public bool LocationUsageAcknowledged { get; set; }
    public bool IntensitySelected { get; set; }
    public byte IntensityGoal { get; set; }
    public byte IntensityProgress { get; set; }
    public byte BonsaiStage { get; set; }
    public uint Streak { get; set; }
}