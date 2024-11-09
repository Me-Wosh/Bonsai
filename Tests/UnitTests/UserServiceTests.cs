using Bonsai.Models;
using Bonsai.Services;
using Microsoft.Extensions.Time.Testing;
using NSubstitute;

namespace UnitTests;

public class UserServiceTests
{
    private readonly IFileService _fileServiceMock;
    private readonly FakeTimeProvider _fakeTimeProvider;
    private readonly UserService _userService;
    private User _user = new();

    public UserServiceTests()
    {
        // Arrange
        _fileServiceMock = Substitute.For<IFileService>();
        _fileServiceMock
            .LoadUser()
            .Returns(_ => _user);

        _fakeTimeProvider = new FakeTimeProvider();
        _fakeTimeProvider.SetLocalTimeZone(TimeZoneInfo.Local);
        
        // Act
        _userService = new UserService(_fileServiceMock, _fakeTimeProvider);
    }

    [Fact]
    public async Task ResetBonsaiIfGoalNotAchievable_ShouldResetBonsaiAndReturnTrue_IfUserMissedTooManyDays()
    {
        // Arrange
        _fakeTimeProvider.SetUtcNow(new DateTime(2024, 10, 25)); // this week friday

        _user = new User
        {
            DateProgressStarted = new DateTime(2024, 10, 23), // this week wednesday
            IntensityGoal = 5,
            IntensityProgress = 1,
            BonsaiStage = 1
        };

        // Act
        _userService.LoadUser();
        var result = await _userService.ResetBonsaiIfGoalNotAchievable();

        // Assert
        Assert.True(result);
        Assert.Null(_user.DateProgressStarted);
        Assert.Equal(0, _user.IntensityProgress);
        Assert.Equal(0, _user.BonsaiStage);        
    }

    [Fact]
    public async Task ResetBonsaiIfGoalNotAchievable_ShouldResetBonsaiAndReturnTrue_IfTodayIsMondayAndPreviousWeekIntensityGoalIsNotAchievable()
    {
        // Arrange
        _fakeTimeProvider.SetUtcNow(new DateTime(2024, 10, 21)); // this week monday

        _user = new User
        {
            DateProgressStarted = new DateTime(2024, 10, 14), // last week monday
            IntensityGoal = 4,
            IntensityProgress = 3,
            BonsaiStage = 3
        };

        // Act
        _userService.LoadUser();
        var result = await _userService.ResetBonsaiIfGoalNotAchievable();

        // Assert
        Assert.True(result);
        Assert.Null(_user.DateProgressStarted);
        Assert.Equal(0, _user.IntensityProgress);
        Assert.Equal(0, _user.BonsaiStage);
    }

    [Fact]
    public async Task ResetBonsaiIfGoalNotAchievable_ShouldNotResetBonsaiAndReturnFalse_IfUserDidntMissTooManyDays()
    {
        // Arrange
        _fakeTimeProvider.SetUtcNow(new DateTime(2024, 10, 25)); // this week friday

        _user = new User
        {
            DateProgressStarted = new DateTime(2024, 10, 20), // previous week sunday
            IntensityGoal = 4,
            IntensityProgress = 1,
            BonsaiStage = 1
        };

        // Act
        _userService.LoadUser();
        var result = await _userService.ResetBonsaiIfGoalNotAchievable();

        // Assert
        Assert.False(result);
        Assert.NotNull(_user.DateProgressStarted);
        Assert.NotEqual(0, _user.IntensityProgress);
        Assert.NotEqual(0, _user.BonsaiStage);
    }

    [Fact]
    public async Task ResetBonsaiIfGoalNotAchievable_ShouldNotResetBonsaiAndReturnFalse_IfUserStartedProgressOnDifferentDayThanMondayAndDidntMissAnyDays()
    {
        // Arrange
        var today = new DateTime(2024, 10, 25); // this week friday
        _fakeTimeProvider.SetUtcNow(today);

        _user = new User
        {
            DateProgressStarted = today,
            IntensityGoal = 7,
            IntensityProgress = 1,
            BonsaiStage = 1
        };

        // Act
        _userService.LoadUser();
        var result = await _userService.ResetBonsaiIfGoalNotAchievable();

        // Assert

        // since user can start their progress on any day of the week they shouldnt be punished for selecting goal
        // greater than maximum reachable goal this week if they didnt miss any days this week

        Assert.False(result);
        Assert.NotNull(_user.DateProgressStarted);
        Assert.NotEqual(0, _user.IntensityProgress);
        Assert.NotEqual(0, _user.BonsaiStage);
    }

    [Fact]
    public async Task ResetBonsaiIfGoalNotAchievable_ShouldReturnFalse_IfProgressNotStarted()
    {
        // Act
        _userService.LoadUser();
        var result = await _userService.ResetBonsaiIfGoalNotAchievable();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ResetBonsaiIfGoalNotAchievable_ShouldNotResetBonsaiAndReturnFalse_IfIntensityGoalReached()
    {
        // Arrange
        _fakeTimeProvider.SetUtcNow(new DateTime(2024, 10, 25)); // this week friday

        _user = new User
        {
            DateProgressStarted = new DateTime(2024, 10, 23), // this week wednesday
            IntensityGoal = 3,
            IntensityProgress = 3,
            BonsaiStage = 3
        };

        // Act
        _userService.LoadUser();
        var result = await _userService.ResetBonsaiIfGoalNotAchievable();

        // Assert
        Assert.False(result);
        Assert.NotNull(_user.DateProgressStarted);
        Assert.NotEqual(0, _user.IntensityProgress);
        Assert.NotEqual(0, _user.BonsaiStage);
    }

    [Fact]
    public async Task ResetBonsaiIfGoalNotAchievable_ShouldResetIntensityProgressWithoutResettingBonsaiAndReturnFalse_IfTodayIsMonday()
    {
        // Arrange
        _fakeTimeProvider.SetUtcNow(new DateTime(2024, 10, 21)); // this week monday

        _user = new User
        {
            DateProgressStarted = new DateTime(2024, 10, 18), // last week friday
            IntensityGoal = 3,
            IntensityProgress = 3,
            BonsaiStage = 3
        };

        // Act
        _userService.LoadUser();
        var result = await _userService.ResetBonsaiIfGoalNotAchievable();

        // Assert
        Assert.False(result);
        Assert.NotNull(_user.DateProgressStarted);
        Assert.Equal(0, _user.IntensityProgress);
        Assert.NotEqual(0, _user.BonsaiStage);
    }

    [Fact]
    public async Task UpdateBonsaiStage_ShouldLevelUpBonsaiAndReturnTrue_IfTodaysGoalReached()
    {
        // Arrange
        var toDosCompletionPercentage = 100;

        _user = new User
        {
            BonsaiStage = 0
        };
        
        // Act
        _userService.LoadUser();
        var result = await _userService.UpdateBonsaiStage(toDosCompletionPercentage);
        
        // Assert
        Assert.True(result);
        Assert.Equal(1, _user.BonsaiStage);
    }

    [Fact]
    public async Task UpdateBonsaiStage_ShouldNotLevelUpBonsaiAndReturnFalse_IfMaxBonsaiStageReached()
    {
        // Arrange
        var toDosCompletionPercentage = 100;

        _user = new User
        {
            BonsaiStage = _user.BonsaiMaxStage
        };
        
        // Act
        _userService.LoadUser();
        var result = await _userService.UpdateBonsaiStage(toDosCompletionPercentage);
        
        // Assert
        Assert.False(result);
        Assert.Equal(_user.BonsaiMaxStage, _user.BonsaiStage);
    }

    [Fact]
    public async Task UpdateBonsaiStage_ShouldNotLevelUpBonsaiAndReturnFalse_IfBonsaiWasAlreadyLeveledUpToday()
    {
        // Arrange
        _fakeTimeProvider.SetUtcNow(DateTime.UtcNow.Date);
        
        var toDosCompletionPercentage = 100;

        _user = new User
        {
            DateLeveledUp = DateTime.Today,
            BonsaiStage = 1
        };
        
        // Act
        _userService.LoadUser();
        var result = await _userService.UpdateBonsaiStage(toDosCompletionPercentage);
        
        // Assert
        Assert.False(result);
        Assert.Equal(1, _user.BonsaiStage);
    }
    
    [Fact]
    public async Task UpdateBonsaiStage_ShouldLevelDownBonsaiAndReturnTrue_IfTodaysGoalIsNotReachableAnymore()
    {
        // Arrange
        _fakeTimeProvider.SetUtcNow(DateTime.UtcNow);

        var toDosCompletionPercentage = 75;

        _user = new User
        {
            DateLeveledUp = DateTime.Today,
            BonsaiStage = 1
        };
        
        // Act
        _userService.LoadUser();
        var result = await _userService.UpdateBonsaiStage(toDosCompletionPercentage);
        
        // Assert
        Assert.True(result);
        Assert.Null(_user.DateLeveledUp);
        Assert.Equal(0, _user.BonsaiStage);
    }
}