using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

public class RollerCoaster : Attraction
{
    private static List<RollerCoaster> _extent = new();
    public static IReadOnlyList<RollerCoaster> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/rollerCoasters.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    
    private double _trackLength;
    private double _maxSpeed;
    private int _numberOfLoops;

    public double TrackLength 
    { 
        get => _trackLength;
        set
        {
            if (value < 0)
                throw new ArgumentException("Track length cannot be negative.");
            _trackLength = value;
        }
    }

    public double MaxSpeed 
    { 
        get => _maxSpeed;
        set
        {
            if (value < 0)
                throw new ArgumentException("Max speed cannot be negative.");
            _maxSpeed = value;
        }
    }

    public int NumberOfLoops 
    { 
        get => _numberOfLoops;
        set
        {
            if (value < 0)
                throw new ArgumentException("Number of loops cannot be negative.");
            _numberOfLoops = value;
        }
    }

    public RollerCoaster(string name, int height, int maxSeats, bool vipPassWorks,
        double trackLength, double maxSpeed, int numberOfLoops, Zone? zone)
        : base(name, height, maxSeats, vipPassWorks, zone)
    {
        TrackLength = trackLength;
        MaxSpeed = maxSpeed;
        NumberOfLoops = numberOfLoops;
        _extent.Add(this);
    }
}