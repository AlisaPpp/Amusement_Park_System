namespace Amusement_Park_System.Models;

public class Promotion
{
    private string _name = "";
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty.");
            _name = value;
        }
    }
    
    private DateTime _startDate;

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            if (value < DateTime.Now.Date)
                throw new ArgumentException("Start date cannot be in the past");
            _startDate = value;
        }
    }
    
    private DateTime _endDate;

    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            if (value < StartDate)
                throw new ArgumentException("End date cannot be earlier than the start date");
            _endDate = value;
        }
    }
    
    private int _promotionPercent;

    public int PromotionPercent
    {
        get => _promotionPercent;
        set
        {
            if (value < 0 || value > 100)
                throw new ArgumentException("Promotion percent must be between 0 and 100");
            _promotionPercent = value;
        }
    }

    public Promotion(string name, DateTime startDate, DateTime endDate, int promotionPercent)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        PromotionPercent = promotionPercent;
    }
}