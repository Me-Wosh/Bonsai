using System.ComponentModel.DataAnnotations;

namespace Bonsai.Models;

public class User : UserData
{
    public DateTime DateJoined => DateTime.Today;
    public DateTime? DateLastLeveledUp { get; set; }
    public DateTime? DateLevelingStarted { get; set; }
    public DateTime? DateBonsaiMaxStageReached { get; set; }
    public bool LocationUsageAcknowledged { get; set; }
    public byte IntensityGoal { get; set; }
    public byte IntensityProgress { get; set; }
    public byte BonsaiStage { get; set; }
    public byte BonsaiMaxStage => 14;
    public int RecordStreak { get; set; }
    [MaxLength(30)]
    public string? Username { get; set; }
    public byte[]? ProfilePicture { get; set; }
}