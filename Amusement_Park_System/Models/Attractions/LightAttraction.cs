using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

public class LightAttraction : Attraction
{
    private static List<LightAttraction> _extent = new();
    public static IReadOnlyList<LightAttraction> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/lightAttractions.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    
    public static int MinimumHeightRequirement = 100;
    
    private bool _isParentSupervisionRequired;

    public bool IsParentSupervisionRequired 
    { 
        get => _isParentSupervisionRequired;
        set => _isParentSupervisionRequired = value;
    }
    

    public LightAttraction(string name, int height, int maxSeats, bool vipPassWorks, bool isParentSupervisionRequired )
        : base(name, height, maxSeats, vipPassWorks, null)
    {
       IsParentSupervisionRequired = isParentSupervisionRequired;
       _extent.Add(this);
    }
}