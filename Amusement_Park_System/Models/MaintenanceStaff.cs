using Amusement_Park_System.Persistence;

namespace Amusement_Park_System.Models;

public class MaintenanceStaff : Employee
{
    private static List<MaintenanceStaff> _extent = new();
    public static IReadOnlyList<MaintenanceStaff> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/maintenanceStaff.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    public MaintenanceStaff(
        string name,
        string surname,
        string contactInfo,
        DateTime birthDate,
        int yearsOfExperience,
        string specialization,
        List<string>? certifications )
        : base(name, surname, contactInfo, birthDate, yearsOfExperience)
    {
        Specialization = specialization;
        Certifications = certifications;
        _extent.Add(this);
    }

    private string _specialization = "";
    public string Specialization
    {
        get => _specialization;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Specialization cannot be empty.");
            _specialization = value.Trim();
        }
    }
    private List<string>? _certifications = new();

    public List<string>? Certifications
    {
        get => _certifications;
        set
        {
            if (value != null)
            {
                if (value.Any(x => string.IsNullOrWhiteSpace(x))) 
                    throw new ArgumentException("Certifications cannot be empty or whitespace.");
            }
            _certifications = value;
        }
    }

}