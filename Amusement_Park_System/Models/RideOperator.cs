namespace Amusement_Park_System.Models;

public class RideOperator : Employee
{
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