using System.Diagnostics;
using Bonsai.Helpers;
using Bonsai.Models;

namespace Bonsai.Services;

public class UserService : IUserService
{
    private readonly IFileService _fileService;
    private readonly TimeProvider _timeProvider;
    private static User _user = new();

    public UserService(IFileService fileService, TimeProvider timeProvider)
    {
        _fileService = fileService;
        _timeProvider = timeProvider;
    }

    public DateTime DateJoined => _user.DateJoined;
    public bool LocationUsageAcknowledged => _user.LocationUsageAcknowledged;
    public bool IntensitySelected => _user.IntensityGoal > 0;
    public byte IntensityGoal => _user.IntensityGoal;
    public byte IntensityProgress => _user.IntensityProgress;
    public byte BonsaiStage => _user.BonsaiStage;
    public int Streak => (int?)(Today - _user.DateLevelingStarted)?.TotalDays + 1 ?? 0;
    public int RecordStreak => _user.RecordStreak;
    public int DaysLeft => Today.DayOfWeek == DayOfWeek.Sunday ? 0 : 7 - (int)Today.DayOfWeek;
    public string? Username => _user.Username;
    public string? ProfilePicture => _user.ProfilePicture != null ? Convert.ToBase64String(_user.ProfilePicture) : null;
    private bool TodayLeveledUp => Today.Date == _user.DateLeveledUp?.Date;
    private DateTime Today => _timeProvider.GetLocalNow().DateTime;

    public async Task LoadUser()
    {
        _user = _fileService.LoadUser();

        if (Streak > RecordStreak)
        {
            _user.RecordStreak = Streak;
            await UpdateUser();
        }
    }

    public async Task LocationUsageWasAcknowledged()
    {
        _user.LocationUsageAcknowledged = true;
        await UpdateUser();
    }

    public async Task SetIntensityGoal(byte intensityGoal)
    {
        _user.IntensityGoal = intensityGoal;
        await UpdateUser();
    }

    public async Task<bool> SetUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return false;
        }

        username = username.Trim();
        var maxLength = DataAnnotationsHelper.GetMaxLengthAttribute(_user.GetType(), nameof(_user.Username));

        if (username == _user.Username || username.Length > maxLength)
        {
            return false;
        }

        _user.Username = username;
        await UpdateUser();
        return true;
    }

    public async Task SetProfilePicture(string title)
    {
        try
        {
            var options = new PickOptions 
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = title
            };

            var result = await FilePicker.Default.PickAsync(options);

            if (result == null)
            {
                return;
            }

            _user.ProfilePicture = await File.ReadAllBytesAsync(result.FullPath);
            await UpdateUser();
        }

        catch (Exception exception)
        {
            Debug.WriteLine(exception);
        }
    }

    public async Task<bool> ResetBonsaiIfGoalNotAchievable()
    {
        if (Today.DayOfWeek == DayOfWeek.Monday)
        {
            if (!IntensityGoalAchievable(Today.AddDays(-1), true))
            {
                await ResetBonsai();
                return true;
            }

            _user.IntensityProgress = 0;
            return false;
        }

        if (_user.DateLevelingStarted == null || _user.IntensityProgress == _user.IntensityGoal)
        {
            return false;
        }
        
        var nextSunday = Today.AddDays(DaysLeft);

        if (!IntensityGoalAchievable(nextSunday))
        {
            await ResetBonsai();
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateBonsaiStage(int toDosCompletionPercentage)
    {
        if (toDosCompletionPercentage == 100)
        {
            return await LevelUpBonsai();
        }

        else if (toDosCompletionPercentage < 100)
        {
            return await LevelDownBonsai();
        }

        return false;
    }

    private bool IntensityGoalAchievable(DateTime nextSunday, bool isPreviousWeek = false)
    {
        if (nextSunday.DayOfWeek != DayOfWeek.Sunday)
        {
            throw new ArgumentException($"{nextSunday} is not sunday");
        }

        var maxProgress = (int?)(nextSunday - _user.DateLevelingStarted)?.TotalDays + 1;
        var maxGoal = maxProgress < 7 && maxProgress < _user.IntensityGoal ? maxProgress : _user.IntensityGoal;
        var daysLeft = isPreviousWeek ? 0 : (int)(nextSunday - Today).TotalDays + 1;

        return daysLeft >= maxGoal - _user.IntensityProgress;
    }

    private async Task ResetBonsai()
    {
        _user.DateLeveledUp = null;
        _user.DateLevelingStarted = null;
        _user.IntensityProgress = 0;
        _user.BonsaiStage = 0;
        _user.DateBonsaiMaxStageReached = null;

        await UpdateUser();
    }

    private async Task<bool> LevelUpBonsai()
    {
        if (TodayLeveledUp)
        {
            return false;
        }
    
        _user.DateLeveledUp = Today;
        _user.DateLevelingStarted ??= Today;
        _user.IntensityProgress++;

        if (Streak > _user.RecordStreak)
        {
            _user.RecordStreak = Streak;
        }

        if (_user.BonsaiStage < _user.BonsaiMaxStage)
        {
            _user.BonsaiStage++;

            if (_user.BonsaiStage == _user.BonsaiMaxStage)
            {
                _user.DateBonsaiMaxStageReached = Today;
            }

            await UpdateUser();
            return true;
        }

        await UpdateUser();
        return false;
    }

    private async Task<bool> LevelDownBonsai()
    {
        if (!TodayLeveledUp)
        {
            return false;
        }

        if (_user.DateLevelingStarted?.Date == Today.Date)
        {
            _user.DateLevelingStarted = null;
        }

        _user.DateLeveledUp = null;
        _user.IntensityProgress--;

        if ((Today - _user.DateBonsaiMaxStageReached)?.TotalDays >= 1)
        {
            await UpdateUser();
            return false;
        }

        _user.BonsaiStage--;
        _user.DateBonsaiMaxStageReached = null;

        await UpdateUser();
        return true;
    }

    private async Task UpdateUser()
    {
        await _fileService.UpdateUserDataAsync(Files.User, _user);
    }
}