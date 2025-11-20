using Amusement_Park_System.Models;
using System;

namespace Amusement_Park_System_Tests
{
    public class TicketTypeTests
    {
        private readonly TicketType ticketType = new TicketType("Family Pass", true, 100.0m);

        // TypeName Tests
        [Test]
        public void TestTicketTypeTypeName()
        {
            Assert.That(ticketType.TypeName, Is.EqualTo("Family Pass"));
        }

        [Test]
        public void TestTicketTypeEmptyTypeNameException()
        {
            Assert.Throws<ArgumentException>(() => new TicketType("", true, 100.0m));
        }

        [Test]
        public void TestTicketTypeNullTypeNameException()
        {
            Assert.Throws<ArgumentException>(() => new TicketType(null, true, 100.0m));
        }

        [Test]
        public void TestTicketTypeTypeNameSetterEmptyException()
        {
            var testTicketType = new TicketType("Family Pass", true, 100.0m);
            Assert.Throws<ArgumentException>(() => testTicketType.TypeName = "");
        }

        [Test]
        public void TestTicketTypeTypeNameSetterNullException()
        {
            var testTicketType = new TicketType("Family Pass", true, 100.0m);
            Assert.Throws<ArgumentException>(() => testTicketType.TypeName = null);
        }

        [Test]
        public void TestTicketTypeTypeNameSetter()
        {
            var testTicketType = new TicketType("Family Pass", true, 100.0m);
            testTicketType.TypeName = "VIP Pass";
            Assert.That(testTicketType.TypeName, Is.EqualTo("VIP Pass"));
        }

        // IsVip Tests
        [Test]
        public void TestTicketTypeIsVip()
        {
            Assert.That(ticketType.IsVip, Is.True);
        }

        [Test]
        public void TestTicketTypeIsVipSetter()
        {
            var testTicketType = new TicketType("Family Pass", true, 100.0m);
            testTicketType.IsVip = false;
            Assert.That(testTicketType.IsVip, Is.False);
        }

        // InitialPrice Tests
        [Test]
        public void TestTicketTypeInitialPrice()
        {
            Assert.That(ticketType.InitialPrice, Is.EqualTo(100.0m));
        }

        [Test]
        public void TestTicketTypeNegativeInitialPriceException()
        {
            Assert.Throws<ArgumentException>(() => new TicketType("Family Pass", true, -50.0m));
        }

        [Test]
        public void TestTicketTypeInitialPriceSetterNegativeException()
        {
            var testTicketType = new TicketType("Family Pass", true, 100.0m);
            Assert.Throws<ArgumentException>(() => testTicketType.InitialPrice = -50.0m);
        }

        [Test]
        public void TestTicketTypeInitialPriceSetter()
        {
            var testTicketType = new TicketType("Family Pass", true, 100.0m);
            testTicketType.InitialPrice = 150.0m;
            Assert.That(testTicketType.InitialPrice, Is.EqualTo(150.0m));
        }
    }
}