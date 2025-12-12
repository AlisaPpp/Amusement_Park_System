using Amusement_Park_System.Models;
using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;
using System;

[Serializable]
public class Shop
{
    private static List<Shop> _extent = new();
    public static IReadOnlyList<Shop> Extent => _extent.AsReadOnly();
    
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/shops.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    
    private string _name;
    private ShopType _type;
    private string _location;
    
    private Zone? _zone;
    public Zone? Zone => _zone;

    public string Name 
    { 
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Shop name cannot be empty.");
            _name = value;
        }
    }

    public ShopType Type 
    { 
        get => _type;
        set => _type = value;
    }

    public string Location 
    { 
        get => _location;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Shop location cannot be empty.");
            _location = value;
        }
    }

    public Shop(string name, ShopType type, string location, Zone? zone = null)
    {
        Name = name;
        Type = type;
        Location = location;
        if (zone is not null)
        {
             AssignZone(zone);
        }
        
        _extent.Add(this);
    }
    
    //zone association
    
    public void AssignZone(Zone newZone)
    {
        if (newZone == null)
            throw new ArgumentNullException(nameof(newZone));

        if (_zone != null)
            throw new InvalidOperationException("Shop already has a zone");

        _zone = newZone;
        newZone.AddShopInternal(this);
    }

    public void ClearZone()
    {
        if (_zone == null)
            return;

        _zone.RemoveShopInternal(this);
        _zone = null;
    }
    
}