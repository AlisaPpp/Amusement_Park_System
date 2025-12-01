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
    
    public Shift(DateTime date, DateTime startTime, DateTime endTime)
    {
        Date = date.Date;
        StartTime = startTime;
        EndTime = endTime; 
        
        _extent.Add(this);
    }

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
}