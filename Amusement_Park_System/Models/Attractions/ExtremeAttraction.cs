using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

[Serializable]
public class ExtremeAttraction : Attraction
{
    public static List<ExtremeAttraction> Extent = new();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/extremeAttractions.json"));
    public static void Save() => ExtentManager.Save(Extent, FilePath);
    public static void Load() => ExtentManager.Load(ref Extent, FilePath);
    public static int MinimumAge = 12;
    public static int MinimumHeightRequirement = 140;
    
    private List<string>? _safetyRestrictions;

    public List<string>? SafetyRestrictions 
    { 
        get => _safetyRestrictions;
        set => _safetyRestrictions = value;
    }


    public ExtremeAttraction(string name, int height, int maxSeats, bool vipPassWorks, string safetyRestrictions)
        : base(name, height, maxSeats, vipPassWorks)
    {
        SafetyRestrictions = new List<string>();
        Extent.Add(this);
    }

}
