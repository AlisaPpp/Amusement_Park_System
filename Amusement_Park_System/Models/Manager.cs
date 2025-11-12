namespace Amusement_Park_System.Models;

public class Manager : Employee
{
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
    }
}