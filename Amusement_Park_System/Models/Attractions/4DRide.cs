using Amusement_Park_System.Models;
using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

public class FourDRide : Attraction
{
    private static List<FourDRide> _extent = new();
    public static IReadOnlyList<FourDRide> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/fourDRides.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    
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
        : base(name, height, maxSeats, vipPassWorks, null)
    {
        ShowDuration = showDuration;
        EffectTypes = effectTypes;
        _extent.Add(this);
    }
}