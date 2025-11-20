using Amusement_Park_System.Models;
using System;

namespace Amusement_Park_System_Tests
{
    public class TicketTests
    {
        private readonly Ticket ticket = new Ticket(
            DateTime.Now.Date.AddDays(1),
            DateTime.Now.Date.AddDays(2),
            10, 2, 50.0m);

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
                10, 2, 50.0m));
        }

        [Test]
        public void TestTicketStartDateSetterPastException()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(2),
                DateTime.Now.Date.AddDays(3),
                10, 2, 50.0m);
            Assert.Throws<ArgumentException>(() => testTicket.StartDate = DateTime.Now.Date.AddDays(-1));
        }

        [Test]
        public void TestTicketStartDateSetter()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, 50.0m);
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
                10, 2, 50.0m));
        }

        [Test]
        public void TestTicketEndDateSetterBeforeStartException()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(2),
                DateTime.Now.Date.AddDays(3),
                10, 2, 50.0m);
            Assert.Throws<ArgumentException>(() => testTicket.EndDate = DateTime.Now.Date.AddDays(1));
        }

        [Test]
        public void TestTicketEndDateSetter()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, 50.0m);
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
                -5, 2, 50.0m));
        }

        [Test]
        public void TestTicketPersonalDiscountOver100Exception()
        {
            Assert.Throws<ArgumentException>(() => new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                101, 2, 50.0m));
        }

        [Test]
        public void TestTicketPersonalDiscountSetterNegativeException()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, 50.0m);
            Assert.Throws<ArgumentException>(() => testTicket.PersonalDiscount = -5);
        }

        [Test]
        public void TestTicketPersonalDiscountSetterOver100Exception()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, 50.0m);
            Assert.Throws<ArgumentException>(() => testTicket.PersonalDiscount = 101);
        }

        [Test]
        public void TestTicketPersonalDiscountSetter()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, 50.0m);
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
                10, -1, 50.0m));
        }

        [Test]
        public void TestTicketQuantitySetterNegativeException()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, 50.0m);
            Assert.Throws<ArgumentException>(() => testTicket.Quantity = -1);
        }

        [Test]
        public void TestTicketQuantitySetter()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, 50.0m);
            testTicket.Quantity = 5;
            Assert.That(testTicket.Quantity, Is.EqualTo(5));
        }

        // Price Tests
        [Test]
        public void TestTicketPrice()
        {
            Assert.That(ticket.Price, Is.EqualTo(50.0m));
        }

        [Test]
        public void TestTicketNegativePriceException()
        {
            Assert.Throws<ArgumentException>(() => new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, -10.0m));
        }

        [Test]
        public void TestTicketPriceSetterNegativeException()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, 50.0m);
            Assert.Throws<ArgumentException>(() => testTicket.Price = -10.0m);
        }

        [Test]
        public void TestTicketPriceSetter()
        {
            var testTicket = new Ticket(
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2),
                10, 2, 50.0m);
            testTicket.Price = 75.0m;
            Assert.That(testTicket.Price, Is.EqualTo(75.0m));
        }
    }
}