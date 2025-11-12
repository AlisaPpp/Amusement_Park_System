using Amusement_Park_System.Persistence;

namespace Amusement_Park_System.Models;

public class RideOperator : Employee
{
    public static List<RideOperator> Extent = new();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/rideOperators.json"));
    public static void Save() => ExtentManager.Save(Extent, FilePath);
    public static void Load() => ExtentManager.Load(ref Extent, FilePath);
    public RideOperator(
        string name,
        string surname,
        string contactInfo,
        DateTime birthDate,
        int yearsOfExperience,
        string operatorLicenceId,
        bool isFirstAidCertified)
        : base(name, surname, contactInfo, birthDate, yearsOfExperience)
    {
        OperatorLicenceId = operatorLicenceId;
        IsFirstAidCertified = isFirstAidCertified;
        Extent.Add(this);
    }

    private string _operatorLicenceId = "";
    public string OperatorLicenceId
    {
        get => _operatorLicenceId;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("OperatorLicenceId cannot be empty.");
            _operatorLicenceId = value.Trim();
        }
    }

    public bool IsFirstAidCertified { get; set; }

}