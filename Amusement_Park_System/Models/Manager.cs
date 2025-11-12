using Amusement_Park_System.Persistence;

namespace Amusement_Park_System.Models;

public class Manager : Employee
{
    
    public static List<Manager> Extent = new();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/managers.json"));
    public static void Save() => ExtentManager.Save(Extent, FilePath);
    public static void Load() => ExtentManager.Load(ref Extent, FilePath);
    
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
        Extent.Add(this);
    }
}