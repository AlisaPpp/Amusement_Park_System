using Amusement_Park_System.Persistence;
namespace Amusement_Park_System.Models;

public class Order
{
    private static List<Order> _extent = new();
    public static IReadOnlyList<Order> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/orders.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    
    public Order(int id, string paymentMethod, Customer customer)
    {
        Id = id;
        PaymentMethod = paymentMethod;
        _extent.Add(this);
        
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        customer.AddOrderInternal(this);
    }
    public int Id { get; set; }
    
    private string _paymentMethod = "";
    public string PaymentMethod
    {
        get => _paymentMethod;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Payment Method cannot be empty.");
            _paymentMethod = value;
        }
    }
    private decimal _totalPrice;

    public decimal TotalPrice => Tickets.Sum(ticket => ticket.Price * ticket.Quantity);
    
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
    
    //Customer association
    public Customer Customer { get; private set; }
    internal void RemoveCustomerInternal()
    {
        Customer = null!;   
    }

    internal static void Delete(Order order)
    {
        _extent.Remove(order);
    }
    
}
