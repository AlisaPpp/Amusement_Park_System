using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

public class WaterRide : Attraction
{
    
    private static List<WaterRide> _extent = new();
    public static IReadOnlyList<WaterRide> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/waterRides.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    
    private double _waterDepth;
    private double _waterTemperature;

    public double WaterDepth 
    { 
        get => _waterDepth;
        set
        {
            if (value < 0)
                throw new ArgumentException("Water depth cannot be negative.");
            _waterDepth = value;
        }
    }

    public double WaterTemperature 
    { 
        get => _waterTemperature;
        set => _waterTemperature = value;
    }

    public WaterRide(string name, int height, int maxSeats, bool vipPassWorks,
        double waterDepth, double waterTemperature, Zone? zone  = null)
        : base(name, height, maxSeats, vipPassWorks, zone)
    {
        WaterDepth = waterDepth;
        WaterTemperature = waterTemperature;
        _extent.Add(this);
    }
}