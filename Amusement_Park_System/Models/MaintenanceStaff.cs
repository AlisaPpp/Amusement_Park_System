using Amusement_Park_System.Persistence;

namespace Amusement_Park_System.Models;

public class MaintenanceStaff : Employee
{
    public static List<MaintenanceStaff> Extent = new();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/maintenanceStaff.json"));
    public static void Save() => ExtentManager.Save(Extent, FilePath);
    public static void Load() => ExtentManager.Load(ref Extent, FilePath);
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
        Extent.Add(this);
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
    
    public List<string>? Certifications { get; set; }

}