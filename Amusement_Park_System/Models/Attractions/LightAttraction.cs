using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

public class LightAttraction : Attraction
{
    public static List<LightAttraction> Extent = new();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/lightAttractions.json"));
    public static void Save() => ExtentManager.Save(Extent, FilePath);
    public static void Load() => ExtentManager.Load(ref Extent, FilePath);
    public static int MinimumHeightRequirement = 100;
    
    private bool _isParentSupervisionRequired;

    public bool IsParentSupervisionRequired 
    { 
        get => _isParentSupervisionRequired;
        set => _isParentSupervisionRequired = value;
    }
    

    public LightAttraction(string name, int height, int maxSeats, bool vipPassWorks, bool isParentSupervisionRequired )
        : base(name, height, maxSeats, vipPassWorks)
    {
       IsParentSupervisionRequired = isParentSupervisionRequired;
       Extent.Add(this);
    }
}