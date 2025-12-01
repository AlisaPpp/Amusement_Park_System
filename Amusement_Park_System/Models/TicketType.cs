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
    }
}