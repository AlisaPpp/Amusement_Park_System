namespace Amusement_Park_System;

public class MalfunctionReport
{
    private string _type = "";
    public string Type
    {
        get => _type;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Malfunction report type cannot be empty.");
        }
    }
    
    private DateTime _date;

    public DateTime Date
    {
        get => _date;
        set
        {
            if (value < DateTime.Now.Date)
                throw new ArgumentException("Malfunction report date set in the past.");
            _date = value;
        }
    }
    
    private string _description = "";

    public string Description
    {
        get => _description;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Malfunction report description cannot be empty.");
            _description = value;
        }
    }
}