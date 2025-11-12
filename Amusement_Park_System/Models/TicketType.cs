using Amusement_Park_System.Persistence;

namespace Amusement_Park_System.Models;

public class TicketType
{
    public static List<TicketType> Extent = new();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/ticketTypes.json"));
    public static void Save() => ExtentManager.Save(Extent, FilePath);
    public static void Load() => ExtentManager.Load(ref Extent, FilePath);
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
        
        Extent.Add(this);
    }
}