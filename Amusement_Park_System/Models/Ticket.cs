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

    private int _personalDiscount;
    public int PersonalDiscount
    {
        get => _personalDiscount;
        set
        {
            if (value < 0 || value > 100)
                throw new ArgumentException("Personal discount must be between 0 and 100.");
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
        get => _price;
        set
        {
            if (value < 0)
                throw new ArgumentException("Price cannot be less than 0.");
            _price = value;
        }
    }
    
    public Ticket(DateTime startDate, DateTime endDate, int personalDiscount, int quantity, decimal price)
    {
        StartDate = startDate;
        EndDate = endDate;
        PersonalDiscount = personalDiscount;
        Quantity = quantity;
        Price = price;
        _extent.Add(this);
    }
}