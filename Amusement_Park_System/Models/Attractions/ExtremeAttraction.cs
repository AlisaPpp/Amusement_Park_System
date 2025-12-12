using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

[Serializable]
public class ExtremeAttraction : Attraction
{
    private static List<ExtremeAttraction> _extent = new();
    public static IReadOnlyList<ExtremeAttraction> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/extremeAttractions.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();

    public static int MinimumAge = 12;
    public static int MinimumHeightRequirement = 140;
    
    private List<string>? _safetyRestrictions;

    public List<string>? SafetyRestrictions 
    { 
        get => _safetyRestrictions;
        set
        {
            if (value != null)
            {
                if (value.Any(s => string.IsNullOrWhiteSpace(s)))
                    throw new ArgumentException("The safety restrictions cannot be empty or whitespace.");
            }
            _safetyRestrictions = value;
        }
    }


    public ExtremeAttraction(string name, int height, int maxSeats, bool vipPassWorks, Zone? zone, List<string>? safetyRestrictions )
        : base(name, height, maxSeats, vipPassWorks, zone)
    {
        SafetyRestrictions = safetyRestrictions;
        _extent.Add(this);
    }

}
