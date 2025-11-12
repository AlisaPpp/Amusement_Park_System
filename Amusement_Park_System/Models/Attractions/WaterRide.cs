using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

public class WaterRide : Attraction
{
    
    public static List<WaterRide> Extent = new();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/waterRides.json"));
    public static void Save() => ExtentManager.Save(Extent, FilePath);
    public static void Load() => ExtentManager.Load(ref Extent, FilePath);
    
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
        double waterDepth, double waterTemperature)
        : base(name, height, maxSeats, vipPassWorks)
    {
        WaterDepth = waterDepth;
        WaterTemperature = waterTemperature;
        Extent.Add(this);
    }
}