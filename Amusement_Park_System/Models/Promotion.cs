using Amusement_Park_System.Persistence;
namespace Amusement_Park_System.Models;
public class Promotion
{
    private static List<Promotion> _extent = new();
    public static IReadOnlyList<Promotion> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/promotions.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    
    private string _name = "";
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty.");
            _name = value;
        }
    }
    
    private DateTime _startDate;

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            if (value < DateTime.Now.Date)
                throw new ArgumentException("Start date cannot be in the past");
            _startDate = value;
        }
    }
    
    private DateTime _endDate;

    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            if (value < StartDate)
                throw new ArgumentException("End date cannot be earlier than the start date");
            _endDate = value;
        }
    }
    
    private int _promotionPercent;

    public int PromotionPercent
    {
        get => _promotionPercent;
        set
        {
            if (value < 0 || value > 100)
                throw new ArgumentException("Promotion percent must be between 0 and 100");
            _promotionPercent = value;
        }
    }
    
    public Promotion(string name, DateTime startDate, DateTime endDate, int promotionPercent)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        PromotionPercent = promotionPercent;
        
        _extent.Add(this);
    }
    
    //TicketType association
    private HashSet<TicketType> _ticketTypes = new();
    public IReadOnlyCollection<TicketType> TicketTypes => _ticketTypes;
    public bool IsExpired => EndDate < DateTime.Now.Date;
    
    public void AddTicketType(TicketType ticketType)
    {
        if (ticketType == null)
            throw new ArgumentNullException(nameof(ticketType));
        if (_ticketTypes.Contains(ticketType)) 
            return;
        _ticketTypes.Add(ticketType);
        ticketType.AssignPromotionInternal(this);
    }

    public void RemoveTicketType(TicketType ticketType)
    {
        if (ticketType == null)
            throw new ArgumentNullException(nameof(ticketType));
        if (!_ticketTypes.Contains(ticketType))
            return;
        _ticketTypes.Remove(ticketType);
        ticketType.RemovePromotionInternal();
    }
    
    internal void AddTicketTypeInternal(TicketType ticketType)
    {
        _ticketTypes.Add(ticketType);
    }

    internal void RemoveTicketTypeInternal(TicketType ticketType)
    {
        _ticketTypes.Remove(ticketType);
    }
    
    public void Delete()
    {
        foreach (var ticketType in _ticketTypes.ToList())
            ticketType.RemovePromotionInternal();

        _ticketTypes.Clear();
        _extent.Remove(this);
    }
    
    public static void RemoveExpiredPromotions()
    {
        var expiredPromos = _extent.Where(p => p.IsExpired).ToList();

        foreach (var promo in expiredPromos)
            promo.Delete();
    }
    
}