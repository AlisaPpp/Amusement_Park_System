using Amusement_Park_System.Persistence;

namespace Amusement_Park_System.Models;

public class Customer
{
    public static List<Customer> Extent = new();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/customers.json"));
    public static void Save() => ExtentManager.Save(Extent, FilePath);
    public static void Load() => ExtentManager.Load(ref Extent, FilePath);
    private string _name = "";
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty");
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
        Extent.Add(this);
    }
}