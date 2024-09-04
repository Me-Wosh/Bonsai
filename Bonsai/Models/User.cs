namespace Bonsai.Models;

public class User : UserData
{
    private bool _locationUsageAcknowledged;
    private bool _intensitySelected;
    private byte _intensityGoal;
    private byte _intensityProgress;
    private byte _bonsaiStage;
    private uint _streak;
    
    public bool LocationUsageAcknowledged 
    { 
        get => _locationUsageAcknowledged;
        
        set
        {
            _locationUsageAcknowledged = value;
            LastUpdate = DateTime.Now;
        }
    }

    public bool IntensitySelected 
    { 
        get => _intensitySelected; 
        
        set
        {
            _intensitySelected = value;
            LastUpdate = DateTime.Now;
        }
    }

    public byte IntensityGoal
    {
        get => _intensityGoal;

        set
        {
            _intensityGoal= value;
            LastUpdate = DateTime.Now;
        }
    }
    public byte IntensityProgress
    {
        get => _intensityProgress;

        set
        {
            _intensityProgress = value;
            LastUpdate = DateTime.Now;
        }
    }

    public byte BonsaiStage 
    { 
        get => _bonsaiStage; 
        
        set
        {
            _bonsaiStage = value;
            LastUpdate = DateTime.Now;
        }
    }

    public uint Streak 
    { 
        get => _streak;

        set
        {
            _streak = value;
            LastUpdate = DateTime.Now;
        }
    }
}