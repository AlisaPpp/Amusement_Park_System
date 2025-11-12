namespace Amusement_Park_System.Models;

public class Order
{
    public int Id { get; set; }
    
    private string _paymentMethod = "";
    public string PaymentMethod
    {
        get => _paymentMethod;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Payment Method cannot be empty.");
            _paymentMethod = value;
        }
    }
    private decimal _totalPrice;

    public decimal TotalPrice
    {
        get => _totalPrice;
        set
        {
            if (value < 0)
                throw new ArgumentException("Total Price cannot be negative.");
            _totalPrice = value;
        }
    }

    public Order(int id, string paymentMethod, decimal totalPrice)
    {
        Id = id;
        PaymentMethod = paymentMethod;
        TotalPrice = totalPrice;
    }
}