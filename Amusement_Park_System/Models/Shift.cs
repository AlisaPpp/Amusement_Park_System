using Amusement_Park_System.Persistence;
namespace Amusement_Park_System.Models;

public class Shift
{
    private static List<Shift> _extent = new();
    public static IReadOnlyList<Shift> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/shifts.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();

    public DateTime Date { get; set; }
    
    private DateTime _startTime;
    public DateTime StartTime 
    { 
        get => _startTime;
        set => _startTime = value;
    }
    
    private DateTime _endTime;
    public DateTime EndTime 
    { 
        get => _endTime;
        set
        {
            if (value <= StartTime)
                throw new ArgumentException("Shift EndTime must be after StartTime.");
            _endTime = value;
        }
    }
    
    
    public Employee? Employee { get; private set; }
    public Attraction? Attraction { get; private set; }
    
    private Shift(Employee employee, Attraction attraction,
        DateTime date, DateTime startTime, DateTime endTime)
    {
        if (employee == null) throw new ArgumentNullException(nameof(employee));
        if (attraction == null) throw new ArgumentNullException(nameof(attraction));

        Employee   = employee;
        Attraction = attraction;
        Date       = date.Date;
        StartTime  = startTime;
        EndTime    = endTime;

        _extent.Add(this);
    }
    
    
    internal static Shift Create(Employee employee, Attraction attraction,
        DateTime date, DateTime startTime, DateTime endTime)
        => new(employee, attraction, date, startTime, endTime);
    internal void Delete()
    {
        Employee   = null;
        Attraction = null;
        _extent.Remove(this);
    }
    
}