namespace Bonsai.Models;

public class User : UserData
{
    public DateTime? DateLeveledUp { get; set; }
    public DateTime? DateProgressStarted { get; set; }
    public bool LocationUsageAcknowledged { get; set; }
    public byte IntensityGoal { get; set; }
    public byte IntensityProgress { get; set; }
    public byte BonsaiStage { get; set; }
    public byte BonsaiMaxStage { get; } = 14;
    public DateTime? DateBonsaiMaxStageReached { get; set; }
}