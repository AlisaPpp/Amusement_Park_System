using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

public class MediumAttraction : Attraction
{
    private static List<MediumAttraction> _extent = new();
    public static IReadOnlyList<MediumAttraction> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/mediumAttractions.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    
    public const int MinimumHeightRequirement = 120;
    public const int MinimumAge = 8;
    
    private bool _familyFriendly;

    public bool FamilyFriendly 
    { 
        get => _familyFriendly;
        set => _familyFriendly = value;
    }
    
    public MediumAttraction(string name, int height, int maxSeats, bool vipPassWorks, bool familyFriendly, Zone? zone = null)
        : base(name, height, maxSeats, vipPassWorks, zone)
    {
        FamilyFriendly = familyFriendly;
        _extent.Add(this);
    }
}