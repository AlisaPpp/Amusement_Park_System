using Amusement_Park_System.Models;
using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

public class FourDRide : Attraction
{
    public static List<FourDRide> Extent = new();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/fourDRides.json"));
    public static void Save() => ExtentManager.Save(Extent, FilePath);
    public static void Load() => ExtentManager.Load(ref Extent, FilePath);
    private double _showDuration;
    private List<string> _effectTypes;

    public double ShowDuration 
    { 
        get => _showDuration;
        set
        {
            if (value < 0)
                throw new ArgumentException("Show duration cannot be negative.");
            _showDuration = value;
        }
    }

    public List<string> EffectTypes 
    { 
        get => new List<string>(_effectTypes);
        set
        {
            if (value == null || value.Count == 0)
                throw new ArgumentException("Effect types must contain at least one effect.");
            _effectTypes = new List<string>(value);
        }
    }

    public FourDRide(string name, int height, int maxSeats, bool vipPassWorks,
        double showDuration, List<string> effectTypes)
        : base(name, height, maxSeats, vipPassWorks)
    {
        ShowDuration = showDuration;
        EffectTypes = effectTypes;
        Extent.Add(this);
    }
}