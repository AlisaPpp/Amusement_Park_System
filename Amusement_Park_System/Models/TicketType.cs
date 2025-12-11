using Amusement_Park_System.Persistence;

namespace Amusement_Park_System.Models;

public class TicketType
{
    private static List<TicketType> _extent = new();
    public static IReadOnlyList<TicketType> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/ticketTypes.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    
    private Dictionary<string, Zone> _accessibleZones = new();
    public IReadOnlyDictionary<string, Zone> AccessibleZones => _accessibleZones;
    
    
    private string _typeName = "";
    public string TypeName
    {
        get => _typeName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("TypeName should not be empty");
            _typeName = value;
        }
    }

    private bool _isVip;
    public bool IsVip
    {
        get => _isVip;
        set => _isVip = value;
        
    }

    private decimal _initialPrice;
    public decimal InitialPrice
    {
        get => _initialPrice;
        set
        {
            if (value < 0)
                throw new ArgumentException("IsVip should not be negative");
            _initialPrice = value;
        }
    }

    public TicketType(string typeName, bool isVip, decimal initialPrice)
    {
        TypeName = typeName;
        IsVip = isVip;
        InitialPrice = initialPrice;
        
        _extent.Add(this);
        if (IsVip)
        {
            IncludeAllZones(Zone.Extent);
        }
    }

    public void AddZone(Zone zone)
    {
        if (zone == null)
            throw new ArgumentNullException(nameof(zone));
        if (_accessibleZones.ContainsKey(zone.Name))
            throw new InvalidOperationException($"Zone '{zone.Name}' already added.");
        if (!IsVip && !zone.IsMainZone)
        {
            throw new InvalidOperationException($"Only main zones can be added to non-vip ticket types.");
        }
        _accessibleZones.Add(zone.Name, zone);
        zone.AddTicketType(this);
    }

    public void RemoveZone(string zoneName)
    {
        if (!_accessibleZones.ContainsKey(zoneName))
            throw new KeyNotFoundException($"Zone '{zoneName}' is not assigned to this ticket type.");
        Zone zone = _accessibleZones[zoneName];
        zone.RemoveTicketType(this);
        _accessibleZones.Remove(zoneName);
    }

    public void IncludeAllZones(IEnumerable<Zone> zones)
    {
        if (!IsVip)
            throw new InvalidOperationException($"Only VIP TicketTypes can include all zones");

        foreach (var zone in zones)
        {
            _accessibleZones[zone.Name] = zone;
            zone.AddTicketType(this);
        }
    }
    
    internal void AddZoneInternal(Zone zone)
    {
        _accessibleZones[zone.Name] = zone;
    }

    internal void RemoveZoneInternal(string zoneName)
    {
        _accessibleZones.Remove(zoneName);
    }
    
    //Ticket association
    private readonly HashSet<Ticket> _tickets = new();
    public IReadOnlyCollection<Ticket> Tickets => _tickets;
    internal void AddTicketInternal(Ticket ticket)
    {
        if (_tickets.Contains(ticket))
            return;
        _tickets.Add(ticket);
    }
    internal void RemoveTicketInternal(Ticket ticket) => _tickets.Remove(ticket);
    
    //Promotion association
    private Promotion? _promotion;
    public Promotion? Promotion
    {
        get
        {
            if (_promotion != null && _promotion.IsExpired)
            {
                _promotion.Delete();
                _promotion = null;
            }
            return _promotion;
        }
    }

    public void AssignPromotion(Promotion? promotion)
    {
        if (_promotion == promotion) return;
        _promotion?.RemoveTicketTypeInternal(this);
        _promotion = promotion;
        promotion?.AddTicketTypeInternal(this);
    }
    
    internal void AssignPromotionInternal(Promotion? promotion)
    {
        _promotion = promotion;
    }

    internal void RemovePromotionInternal()
    {
        _promotion = null;
    }
    
}