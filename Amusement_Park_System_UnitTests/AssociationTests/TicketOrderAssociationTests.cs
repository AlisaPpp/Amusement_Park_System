using System;
using NUnit.Framework;
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class TicketOrderAssociationTests
    {
        private Customer customer;
        private Order order;
        private Order order2;
        private TicketType type;

        [SetUp]
        public void Setup()
        {
            Customer.ClearExtent();
            Order.ClearExtent();
            Ticket.ClearExtent();
            TicketType.ClearExtent();

            customer = new Customer("John", "Doe", "john@mail.com");
            order = new Order(1, "Card", customer);
            order2 = new Order(2, "Cash", customer);
            type = new TicketType("Regular", false, 100);
        }

        [Test]
        public void TestTicketConstructorCreatesBidirectionalAssociation()
        {
            var ticket = new Ticket(
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                10,
                2,
                type,
                order);

            Assert.That(order.Tickets, Contains.Item(ticket));
            Assert.That(ticket.Order, Is.EqualTo(order));
        }

        [Test]
        public void TestTicketConstructorWithOrder()
        {
            var ticket = new Ticket(
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                null,
                1,
                type,
                order2);

            Assert.That(order2.Tickets, Contains.Item(ticket));
            Assert.That(ticket.Order, Is.EqualTo(order2));
            Assert.That(order.Tickets, Does.Not.Contain(ticket));
        }

        [Test]
        public void TestTicketConstructorNullOrderThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Ticket(
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                null,
                1,
                type,
                null));
        }

        [Test]
        public void TestMultipleTicketsInOrder()
        {
            var ticket1 = new Ticket(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), null, 1, type, order);
            var ticket2 = new Ticket(DateTime.Now.AddDays(2), DateTime.Now.AddDays(3), 10, 2, type, order);
            var ticket3 = new Ticket(DateTime.Now.AddDays(3), DateTime.Now.AddDays(4), null, 3, type, order);

            Assert.That(order.Tickets.Count, Is.EqualTo(3));
            Assert.That(order.Tickets, Contains.Item(ticket1));
            Assert.That(order.Tickets, Contains.Item(ticket2));
            Assert.That(order.Tickets, Contains.Item(ticket3));
            Assert.That(ticket1.Order, Is.EqualTo(order));
            Assert.That(ticket2.Order, Is.EqualTo(order));
            Assert.That(ticket3.Order, Is.EqualTo(order));
        }

        [Test]
        public void TestTicketDeleteRemovesFromOrder()
        {
            var ticket = new Ticket(
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                null,
                1,
                type,
                order);

            ticket.Delete();

            Assert.That(order.Tickets, Does.Not.Contain(ticket));
            Assert.That(Ticket.Extent, Does.Not.Contain(ticket));
        }
        
        [Test]
        public void TestTotalPriceCalculatesFromTickets()
        {
            var ticket1 = new Ticket(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 0, 1, type, order);
            var ticket2 = new Ticket(DateTime.Now.AddDays(2), DateTime.Now.AddDays(3), 10, 2, type, order);

            decimal expectedTotal = (100m * 1) + (90m * 2);
            Assert.AreEqual(expectedTotal, order.TotalPrice);
        }
        

        [Test]
        public void TestTotalPriceZeroWhenNoTickets()
        {
            Assert.That(order.TotalPrice, Is.EqualTo(0));
        }

        [Test]
        public void TestTicketOrderPropertyIsCorrect()
        {
            var ticket = new Ticket(
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                null,
                1,
                type,
                order);

            Assert.That(ticket.Order, Is.EqualTo(order));
        }

        [Test]
        public void TestTicketsCollectionIsReadOnly()
        {
            var ticket = new Ticket(
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                null,
                1,
                type,
                order);

            var tickets = order.Tickets;
            Assert.That(tickets, Is.InstanceOf<IReadOnlyCollection<Ticket>>());
        }
    }
}