using Amusement_Park_System.Persistence;
namespace Amusement_Park_System.Models;
public class Promotion
{
    public static List<Promotion> _extent = new();
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
}