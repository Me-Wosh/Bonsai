namespace Bonsai.Services;

public interface IUserService
{
    DateTime DateJoined { get; }
    bool LocationUsageAcknowledged { get; }
    bool IntensitySelected { get; }
    byte IntensityGoal { get; }
    byte IntensityProgress { get; }
    byte BonsaiStage { get; }
    int Streak { get; }
    int RecordStreak { get; }
    int DaysLeft { get; }
    string? Username { get; }
    string? ProfilePicture { get; }
    Task LoadUser();
    Task SetIntensityGoal(byte intensityGoal);
    Task<bool> SetUsername(string username);
    Task SetProfilePicture(string title);
    Task LocationUsageWasAcknowledged();
    Task<bool> ResetBonsaiIfGoalNotAchievable();
    Task<bool> UpdateBonsaiStage(int toDosCompletionPercentage);
}