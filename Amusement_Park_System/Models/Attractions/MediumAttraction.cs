using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

public class MediumAttraction : Attraction
{
    public static List<MediumAttraction> Extent = new();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/mediumAttractions.json"));
    public static void Save() => ExtentManager.Save(Extent, FilePath);
    public static void Load() => ExtentManager.Load(ref Extent, FilePath);
    public const int MinimumHeightRequirement = 120;
    public const int MinimumAge = 8;
    
    private bool _familyFriendly;

    public bool FamilyFriendly 
    { 
        get => _familyFriendly;
        set => _familyFriendly = value;
    }
    
    public MediumAttraction(string name, int height, int maxSeats, bool vipPassWorks, bool familyFriendly)
        : base(name, height, maxSeats, vipPassWorks)
    {
        FamilyFriendly = familyFriendly;
    }
}