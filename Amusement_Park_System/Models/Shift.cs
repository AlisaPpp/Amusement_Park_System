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
    
    private TimeSpan _startTime;
    public TimeSpan StartTime 
    { 
        get => _startTime;
        set => _startTime = value;
    }
    
    private TimeSpan _endTime;
    public TimeSpan EndTime 
    { 
        get => _endTime;
        set
        {
            if (value <= StartTime)
                throw new ArgumentException("Shift EndTime must be after StartTime.");
            _endTime = value;
        }
    }
    
    
    private Employee _employee = null!;
    private Attraction _attraction = null!;
    private Manager _manager = null!;

    public Employee Employee => _employee;
    public Attraction Attraction => _attraction;
    public Manager Manager => _manager;
    
    
    public Shift(
        DateTime date,
        TimeSpan start,
        TimeSpan end,
        Employee employee,
        Attraction attraction,
        Manager manager)
    {
        Date = date;
        StartTime = start;
        EndTime = end;

        AssignEmployee(employee);
        AssignAttraction(attraction);
        AssignManager(manager);

        _extent.Add(this);
    }
    
    
    private void AssignEmployee(Employee employee)
    {
        _employee = employee;
        employee.AddShiftInternal(this);
    }

    private void AssignAttraction(Attraction attraction)
    {
        if (attraction == null)
            throw new ArgumentNullException(nameof(attraction));

        if (_attraction != null!)
        {
            _attraction.RemoveShiftInternal(this);
        }

        _attraction = attraction;
        attraction.AddShiftInternal(this);
    }

    private void AssignManager(Manager manager)
    {
        if (manager == null)
            throw new ArgumentNullException(nameof(manager));

        // Manager can only assign shifts to employees he manages
        if (!manager.ManagesEmployee(_employee))
            throw new InvalidOperationException("Manager cannot assign a shift to an employee he does not manage.");

        if (_manager != null!)
        {
            _manager.RemoveShiftInternal(this);
        }

        _manager = manager;
        manager.AddShiftInternal(this);
    }
    
    public void Delete()
    {
        Employee.RemoveShiftInternal(this);
        Attraction.RemoveShiftInternal(this);
        Manager.RemoveShiftInternal(this);

        _extent.Remove(this);
    }
    
    
}