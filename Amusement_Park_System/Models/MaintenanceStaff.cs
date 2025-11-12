namespace Amusement_Park_System;

public class MaintenanceStaff : Employee
{
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