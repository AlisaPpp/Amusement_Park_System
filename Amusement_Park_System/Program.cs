using System.Data;
using Amusement_Park_System.Models;

Promotion promo = new Promotion("ChristmasSale", DateTime.Parse("2026-01-08"), DateTime.Parse("2026-09-02"), 50);
Promotion promo2 = new Promotion("HalloweenSale", DateTime.Parse("2026-10-15"), DateTime.Parse("2026-11-03"), 30);
Promotion promo3 = new Promotion("EasterSale", DateTime.Parse("2026-01-08"), DateTime.Parse("2026-09-02"), 50);

Promotion.Save();

Order order = new Order(3, "someMethod", 40);
Order order2 = new Order(2, "someMethod", 30);

Order.Save();

Ticket ticket1 = new Ticket(DateTime.Parse("2026-11-15"), DateTime.Parse("2026-12-21"), 25, 2, 1500);

Ticket.Save();
Promotion.Load();

foreach (Promotion promotion in Promotion.Extent)
{
    Console.WriteLine(promotion.Name);
}

Manager.Load();
