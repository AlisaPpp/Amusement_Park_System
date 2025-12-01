using Amusement_Park_System.Persistence;

namespace Amusement_Park_System.Models;

public class Manager : Employee
{
    
    private static List<Manager> _extent = new();
    public static IReadOnlyList<Manager> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/managers.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    
    public static int MinYearsOfExperience = 3;

    public Manager(
        string name,
        string surname,
        string contactInfo,
        DateTime birthDate,
        int yearsOfExperience)
        : base(name, surname, contactInfo, birthDate, yearsOfExperience)
    {
        if (YearsOfExperience < MinYearsOfExperience)
            throw new ArgumentException($"Managers must have at least {MinYearsOfExperience} years of experience.");
        _extent.Add(this);
    }
}