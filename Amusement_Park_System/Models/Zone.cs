using Amusement_Park_System.Models;
using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;
using System;
using System.Collections.Generic;

public class Zone
{
    private static List<Zone> _extent = new();
    public static IReadOnlyList<Zone> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/zones.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    public bool IsMainZone => ChildZones.Count > 0;
    
    private string _name;
    private string _theme;
    private TimeSpan _openingTime;
    private TimeSpan _closingTime;

    //composition
    private Zone? _parentZone;
    private HashSet<Zone> _childZones = new();
    
    //reflex
    private Zone? _nextZone;
    
    //TicketType qualified
    private HashSet<TicketType> _ticketTypes = new();

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Zone name cannot be empty.");
            _name = value;
        }
    }

    public string Theme
    {
        get => _theme;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Zone theme cannot be empty.");
            _theme = value;
        }
    }

    public TimeSpan OpeningTime
    {
        get => _openingTime;
        set => _openingTime = value;
    }
    public TimeSpan ClosingTime
    {
        get => _closingTime;
        set
        {
            if (value <= _openingTime)
                throw new ArgumentException("Closing time must be after opening time.");
            _closingTime = value;
        }
    }
    
    public Zone? ParentZone => _parentZone;
    public IReadOnlyCollection<Zone> ChildZones => _childZones;
    public Zone? NextZone => _nextZone;
    public IReadOnlyCollection<TicketType> TicketTypes => _ticketTypes;
    

    public Zone(string name, string theme, TimeSpan openingTime, TimeSpan closingTime)
    {
        Name = name;
        Theme = theme;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        
        _extent.Add(this);
    }

    //COMPOSITION METHODS
    public void AddChild(Zone childZone)
    {
        if (childZone == null)
            throw new ArgumentNullException(nameof(childZone));
        
        if (ReferenceEquals(childZone, this))
            throw new ArgumentException("A zone cannot be a child of itself.");
        
        if (childZone._parentZone != null)
            throw new ArgumentException("This zone is already a child of another zone.");
        
        childZone.OpeningTime = this.OpeningTime;
        childZone.ClosingTime = this.ClosingTime;
        
        childZone._parentZone = this;
        _childZones.Add(childZone);
    }

    public void RemoveChild(Zone childZone)
    {
        if (!_childZones.Contains(childZone))
            throw new InvalidOperationException("This zone is not a child of the current one.");
        childZone.DeleteZone();
        _childZones.Remove(childZone);
    }

    public void DeleteZone()
    {
        foreach (var childZone in _childZones.ToList())
        {
            childZone.DeleteZone();
        }
        
        _childZones.Clear();
        _nextZone = null;
        _parentZone?._childZones.Remove(this);
        _parentZone = null;

        _extent.Remove(this);
    }
    
    //REFLEX METHODS
    public void SetNextZone(Zone nextZone)
    {
        if (nextZone == null)
            throw new ArgumentNullException(nameof(nextZone));
        
        if (ReferenceEquals(nextZone, this))
            throw new ArgumentException("A zone cannot reference itself.");
        
        if (CreatesCycle(nextZone))
            throw new InvalidOperationException("Cannot create a cycle in the zone chain.");
        
        _nextZone = nextZone;
    }

    private bool CreatesCycle(Zone zone)
    {
        Zone? currentZone = zone;
        while (currentZone != null)
        {
            if (ReferenceEquals(currentZone, this))
                return true;
            currentZone = currentZone._nextZone;
        }
        return false;
    }

    public void ClearNextZone()
    {
        _nextZone = null;
    }
    
    //TicketType qualified association methods
    public void AddTicketType(TicketType ticketType)
    {
        if (ticketType == null)
            throw new ArgumentNullException(nameof(ticketType));
        if(_ticketTypes.Contains(ticketType))
            return;
        _ticketTypes.Add(ticketType);
        if (!ticketType.AccessibleZones.ContainsKey(Name))
            ticketType.AddZoneInternal(this);
    }

    internal void RemoveTicketType(TicketType ticketType)
    {
        if (ticketType == null)
            throw new ArgumentNullException(nameof(ticketType));

        if (!_ticketTypes.Contains(ticketType))
            return;

        _ticketTypes.Remove(ticketType);

        if (ticketType.AccessibleZones.ContainsKey(Name))
            ticketType.RemoveZoneInternal(Name);
    }
    
    //shop association
    
    private readonly HashSet<Shop> _shops = new();
    public IReadOnlyCollection<Shop> Shops => _shops.ToList().AsReadOnly();
    
    public void AddShop(Shop shop)
    {
        if (shop == null)
            throw new ArgumentNullException(nameof(shop));

        if (_shops.Contains(shop))
            return;
        
        shop.AssignZone(this);
    }
    
    internal void AddShopInternal(Shop shop)
    {
        _shops.Add(shop);
    }

    internal void RemoveShopInternal(Shop shop)
    {
        _shops.Remove(shop);
    }
}