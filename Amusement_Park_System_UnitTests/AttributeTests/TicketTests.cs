using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class TicketTests
    {
        private Ticket ticket;
        private TicketType ticketType;
        private Order order;
        private Customer customer;
        
        [SetUp]
        public void SetUp()
        {
            customer = new Customer("John", "Doe", "john@example.com");

            order = new Order(1, "Credit Card", customer);

            ticketType = new TicketType("Regular", false, 50.0m);

            ticket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, ticketType, order);
        }


        // StartDate Tests
        [Test]
        public void TestTicketStartDate()
        {
            Assert.That(ticket.StartDate, Is.EqualTo(DateTime.Now.Date.AddDays(1)));
        }

        [Test]
        public void TestTicketPastStartDateException()
        {
            Assert.Throws<ArgumentException>(() => new Ticket(
                DateTime.Now.Date.AddDays(-1),
                DateTime.Now.Date.AddDays(1),
                10, 2, ticketType, order));
        }

        [Test]
        public void TestTicketStartDateSetterPastException()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(2),
                DateTime.Now.Date.AddDays(3),
                10, 2, ticketType, order);
            Assert.Throws<ArgumentException>(() => testTicket.StartDate = DateTime.Now.Date.AddDays(-1));
        }

        [Test]
        public void TestTicketStartDateSetter()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, ticketType, order);
            testTicket.StartDate = DateTime.Now.Date.AddDays(3);
            Assert.That(testTicket.StartDate, Is.EqualTo(DateTime.Now.Date.AddDays(3)));
        }

        // EndDate Tests
        [Test]
        public void TestTicketEndDate()
        {
            Assert.That(ticket.EndDate, Is.EqualTo(DateTime.Now.Date.AddDays(2)));
        }

        [Test]
        public void TestTicketEndDateBeforeStartDateException()
        {
            Assert.Throws<ArgumentException>(() => new Ticket(
                DateTime.Now.Date.AddDays(2),
                DateTime.Now.Date.AddDays(1),
                10, 2, ticketType, order));
        }

        [Test]
        public void TestTicketEndDateSetterBeforeStartException()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(2),
                DateTime.Now.Date.AddDays(3),
                10, 2, ticketType, order);
            Assert.Throws<ArgumentException>(() => testTicket.EndDate = DateTime.Now.Date.AddDays(1));
        }

        [Test]
        public void TestTicketEndDateSetter()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, ticketType, order);
            testTicket.EndDate = DateTime.Now.Date.AddDays(4);
            Assert.That(testTicket.EndDate, Is.EqualTo(DateTime.Now.Date.AddDays(4)));
        }

        // PersonalDiscount Tests
        [Test]
        public void TestTicketPersonalDiscount()
        {
            Assert.That(ticket.PersonalDiscount, Is.EqualTo(10));
        }

        [Test]
        public void TestTicketNegativePersonalDiscountException()
        {
            Assert.Throws<ArgumentException>(() => new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                -5, 2, ticketType, order));
        }

        [Test]
        public void TestTicketPersonalDiscountOver100Exception()
        {
            Assert.Throws<ArgumentException>(() => new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                101, 2, ticketType, order));
        }

        [Test]
        public void TestTicketPersonalDiscountSetterNegativeException()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, ticketType, order);
            Assert.Throws<ArgumentException>(() => testTicket.PersonalDiscount = -5);
        }

        [Test]
        public void TestTicketPersonalDiscountSetterOver100Exception()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, ticketType, order);
            Assert.Throws<ArgumentException>(() => testTicket.PersonalDiscount = 101);
        }

        [Test]
        public void TestTicketPersonalDiscountSetter()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, ticketType, order);
            testTicket.PersonalDiscount = 20;
            Assert.That(testTicket.PersonalDiscount, Is.EqualTo(20));
        }

        // Quantity Tests
        [Test]
        public void TestTicketQuantity()
        {
            Assert.That(ticket.Quantity, Is.EqualTo(2));
        }

        [Test]
        public void TestTicketNegativeQuantityException()
        {
            Assert.Throws<ArgumentException>(() => new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, -1, ticketType, order));
        }

        [Test]
        public void TestTicketQuantitySetterNegativeException()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, ticketType, order);
            Assert.Throws<ArgumentException>(() => testTicket.Quantity = -1);
        }

        [Test]
        public void TestTicketQuantitySetter()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, ticketType, order);
            testTicket.Quantity = 5;
            Assert.That(testTicket.Quantity, Is.EqualTo(5));
        }

        // Price Tests
        [Test]
        public void TestTicketPrice()
        {
            decimal expectedPrice = Math.Round(ticketType.InitialPrice * (1 - 0.10m), 2);
            Assert.That(ticket.Price, Is.EqualTo(expectedPrice));
        }

        [Test]
        public void TestTicketPriceWithNoDiscount()
        {
            var t = new Ticket(DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2), null, 1, ticketType, order);
            Assert.That(t.Price, Is.EqualTo(ticketType.InitialPrice));
        }
    }
}