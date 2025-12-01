using Amusement_Park_System.Persistence;

namespace Amusement_Park_System.Models;

public class MalfunctionReport
{
    
    private static List<MalfunctionReport> _extent = new();
    public static IReadOnlyList<MalfunctionReport> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/malfunctionReports.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    public MalfunctionReport(string type, string description, DateTime date)
    {
        Type = type;
        Description = description;
        Date = date;
        _extent.Add(this);
    }

    private string _type = "";
    public string Type
    {
        get => _type;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Type cannot be empty.");
            _type = value.Trim();
        }
    }

    private string _description = "";
    public string Description
    {
        get => _description;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Description cannot be empty.");
            _description = value.Trim();
        }
    }

    private DateTime _date;
    public DateTime Date
    {
        get => _date;
        set
        {
            if (value > DateTime.Now)
                throw new ArgumentException("Date cannot be in the future.");
            _date = value;
        }
    }
    

}