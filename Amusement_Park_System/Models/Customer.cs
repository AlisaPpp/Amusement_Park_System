using Amusement_Park_System.Persistence;

namespace Amusement_Park_System.Models;

public class Customer
{
    private static List<Customer> _extent = new();
    public static IReadOnlyList<Customer> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/customers.json"));
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
                throw new ArgumentException("Name cannot be empty");
            _name = value;
        }
    }
    private string _surname = "";

    public string Surname
    {
        get => _surname;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Surname cannot be empty");
            _surname = value;
        }
    }

    private string _contactInfo = "";

    public string ContactInfo
    {
        get => _contactInfo;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Contact Info cannot be empty");
            _contactInfo = value;
        }
    }

    public Customer(string name, string surname, string contactInfo)
    {
        Name = name;
        Surname = surname;
        ContactInfo = contactInfo;
        _extent.Add(this);
    }
    
    //Order association
    private readonly HashSet<Order> _orders = new();
    public IReadOnlyCollection<Order> Orders => _orders;

    public Order CreateOrder(int id, string paymentMethod)
    {
        return new Order(id, paymentMethod, this);
    }
    
    public void DeleteOrder(Order order)
    {
        if (!_orders.Contains(order))
            throw new InvalidOperationException("The order does not belong to this customer.");

        RemoveOrderInternal(order);
        order.RemoveCustomerInternal();
        Order.Delete(order);
    }
    
    internal void AddOrderInternal(Order order)
    {
        if (!_orders.Contains(order))
            _orders.Add(order);
    }

    internal void RemoveOrderInternal(Order order)
    {
        if (!_orders.Contains(order))
            _orders.Remove(order);
    }
    
}