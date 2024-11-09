namespace Bonsai.Services;

public interface IUserService
{
    bool LocationUsageAcknowledged { get; }
    bool IntensitySelected { get; }
    byte IntensityGoal { get; }
    byte IntensityProgress { get; }
    byte BonsaiStage { get; }
    int Streak { get; }
    int DaysLeft { get; }
    void LoadUser();
    Task SetIntensityGoal(byte intensityGoal);
    Task LocationUsageWasAcknowledged();
    Task<bool> ResetBonsaiIfGoalNotAchievable();
    Task<bool> UpdateBonsaiStage(int toDosCompletionPercentage);
}