using System;
using NUnit.Framework;
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class TicketTicketTypeAssociationTests
    {
        private TicketType type1;
        private TicketType type2;
        private Order order;

        [SetUp]
        public void Setup()
        {
            Customer.ClearExtent();
            Order.ClearExtent();
            Ticket.ClearExtent();
            TicketType.ClearExtent();

            var customer = new Customer("John", "Doe", "john@mail.com");
            order = new Order(1, "Card", customer);
            type1 = new TicketType("Regular", false, 100m);
            type2 = new TicketType("VIP", true, 200m);
        }

        [Test]
        public void TestTicketConstructorCreatesBidirectionalAssociation()
        {
            var ticket = new Ticket(
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                10,
                2,
                type1,
                order);

            Assert.That(type1.Tickets, Contains.Item(ticket));
            Assert.That(ticket.TicketType, Is.EqualTo(type1));
        }

        [Test]
        public void TestTicketConstructorWithDifferentType()
        {
            var ticket = new Ticket(
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                null,
                1,
                type2,
                order);

            Assert.That(type2.Tickets, Contains.Item(ticket));
            Assert.That(ticket.TicketType, Is.EqualTo(type2));
            Assert.That(type1.Tickets, Does.Not.Contain(ticket));
        }

        [Test]
        public void TestTicketConstructorNullTypeThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Ticket(
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                null,
                1,
                null,
                order));
        }

        [Test]
        public void TestMultipleTicketsWithSameType()
        {
            var ticket1 = new Ticket(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), null, 1, type1, order);
            var ticket2 = new Ticket(DateTime.Now.AddDays(2), DateTime.Now.AddDays(3), 10, 2, type1, order);
            var ticket3 = new Ticket(DateTime.Now.AddDays(3), DateTime.Now.AddDays(4), null, 3, type1, order);

            Assert.That(type1.Tickets.Count, Is.EqualTo(3));
            Assert.That(type1.Tickets, Contains.Item(ticket1));
            Assert.That(type1.Tickets, Contains.Item(ticket2));
            Assert.That(type1.Tickets, Contains.Item(ticket3));
            Assert.That(ticket1.TicketType, Is.EqualTo(type1));
            Assert.That(ticket2.TicketType, Is.EqualTo(type1));
            Assert.That(ticket3.TicketType, Is.EqualTo(type1));
        }

        [Test]
        public void TestTicketDeleteRemovesFromType()
        {
            var ticket = new Ticket(
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                null,
                1,
                type1,
                order);

            ticket.Delete();

            Assert.That(type1.Tickets, Does.Not.Contain(ticket));
            Assert.That(Ticket.Extent, Does.Not.Contain(ticket));
        }
        
        [Test]
        public void TestPriceCalculationUsesTicketTypePrice()
        {
            var ticket = new Ticket(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 0, 1, type1, order);
            Assert.That(ticket.Price, Is.EqualTo(100m));

            var ticket2 = new Ticket(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 0, 1, type2, order);
            Assert.That(ticket2.Price, Is.EqualTo(200m));
        }
        

        [Test]
        public void TestTicketTypeProperty()
        {
            var ticket = new Ticket(
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                null,
                1,
                type1,
                order);

            Assert.That(ticket.TicketType, Is.EqualTo(type1));
        }

        [Test]
        public void TestTicketsCollectionIsReadOnly()
        {
            var ticket = new Ticket(
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                null,
                1,
                type1,
                order);

            var tickets = type1.Tickets;
            Assert.That(tickets, Is.InstanceOf<IReadOnlyCollection<Ticket>>());
        }

        [Test]
        public void TestMultipleTicketsWithDifferentTypes()
        {
            var ticket1 = new Ticket(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), null, 1, type1, order);
            var ticket2 = new Ticket(DateTime.Now.AddDays(2), DateTime.Now.AddDays(3), 10, 2, type2, order);

            Assert.That(type1.Tickets.Count, Is.EqualTo(1));
            Assert.That(type2.Tickets.Count, Is.EqualTo(1));
            Assert.That(type1.Tickets, Contains.Item(ticket1));
            Assert.That(type2.Tickets, Contains.Item(ticket2));
            Assert.That(ticket1.TicketType, Is.EqualTo(type1));
            Assert.That(ticket2.TicketType, Is.EqualTo(type2));
        }
        
        
    }
}