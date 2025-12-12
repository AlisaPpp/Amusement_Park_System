using Amusement_Park_System.Persistence;
namespace Amusement_Park_System.Models;

public class Ticket
{
    
    private static List<Ticket> _extent = new();
    public static IReadOnlyList<Ticket> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/tickets.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    
    private DateTime _startDate;
    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            if (value < DateTime.Now.Date)
                throw new ArgumentException("Start date cannot be in the past.");
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
                throw new ArgumentException("End date cannot be earlier than start date.");
            _endDate = value;
        }
    }

    private int? _personalDiscount;
    public int? PersonalDiscount
    {
        get => _personalDiscount;
        set
        {
            if (value != null)
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Personal discount must be between 0 and 100.");
            }
            _personalDiscount = value;
        }
    }
    private int _quantity;
    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value < 0)
                throw new ArgumentException("Quantity cannot be less than 0.");
            _quantity = value;
        }
    }
    private decimal _price;
    public decimal Price
    {
        get
        {
            decimal basePrice = _ticketType.InitialPrice;

            decimal promoPct = _ticketType.Promotion?.PromotionPercent ?? 0m;  
            decimal personalDiscount = PersonalDiscount ?? 0m;                

            decimal promoFactor = 1m - (promoPct / 100m);
            decimal personalFactor = 1m - (personalDiscount / 100m);

            return Math.Round(basePrice * promoFactor * personalFactor, 2);
        }
    }
    
    private TicketType _ticketType;
    private Order _order;

    public TicketType TicketType => _ticketType;
    public Order Order => _order;
    
    public Ticket(DateTime startDate, DateTime endDate, int? personalDiscount, int quantity, TicketType ticketType, Order order)
    {
        StartDate = startDate;
        EndDate = endDate;
        PersonalDiscount = personalDiscount;
        Quantity = quantity;
        AssignTicketType(ticketType);
        AssignOrder(order);
        _extent.Add(this);
        
    }

    private void AssignTicketType(TicketType ticketType)
    {
        if (ticketType == null)
            throw new ArgumentNullException(nameof(ticketType));
        
        if (_ticketType != null)
            _ticketType.RemoveTicketInternal(this);

        _ticketType = ticketType;
        ticketType.AddTicketInternal(this);
    }


    private void AssignOrder(Order order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));
        if (_order != null)
            _order.RemoveTicketInternal(this);
        _order = order;
        order.AddTicketInternal(this);
    }

    public void Delete()
    {
        _ticketType.RemoveTicketInternal(this);
        _order.RemoveTicketInternal(this);
        _extent.Remove(this);
    }
}