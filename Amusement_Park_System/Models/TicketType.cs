namespace Amusement_Park_System;

public class TicketType
{
    private string _typeName = "";
    public string TypeName
    {
        get => _typeName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("TypeName should not be empty");
            _typeName = value;
        }
    }
    private bool _isVip { get; set; }
    public bool IsVip
    {
        get => _isVip;
        set => _isVip = value;
        
    }
    private decimal _initialPrice { get; set; }
    public decimal InitialPrice
    {
        get => _initialPrice;
        set
        {
            if (value < 0)
                throw new ArgumentException("IsVip should not be negative");
            _initialPrice = value;
        }
    }

    public TicketType(string typeName, bool isVip, decimal initialPrice)
    {
        TypeName = typeName;
        IsVip = isVip;
        InitialPrice = initialPrice;
    }
}