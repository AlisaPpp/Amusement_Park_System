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

    public decimal TotalPrice
    {
        get => _totalPrice;
        set
        {
            if (value < 0)
                throw new ArgumentException("Total Price cannot be negative.");
            _totalPrice = value;
        }
    }

    public Order(int id, string paymentMethod, decimal totalPrice)
    {
        Id = id;
        PaymentMethod = paymentMethod;
        TotalPrice = totalPrice;
        _extent.Add(this);
    }
}