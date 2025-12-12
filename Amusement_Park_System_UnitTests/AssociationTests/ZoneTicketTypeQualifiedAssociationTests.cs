using Amusement_Park_System;
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class ZoneTicketTypeQualifiedAssociationTests
    {
        private Zone mainZone1;
        private Zone mainZone2;
        private Zone childZone;
        private TicketType regularTicket;
        private TicketType vipTicket;

        [SetUp]
        public void Setup()
        {
            Zone.ClearExtent();
            TicketType.ClearExtent();
            
            mainZone1 = new Zone("Main Zone 1", "Adventure", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            mainZone1.AddChild(new Zone("Dummy Child 1", "Adventure", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)));
            
            mainZone2 = new Zone("Main Zone 2", "Children", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            mainZone2.AddChild(new Zone("Dummy Child 2", "Children", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)));
            
            childZone = new Zone("Child Zone", "Small", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            
            regularTicket = new TicketType("Regular", false, 100m);
            vipTicket = new TicketType("VIP", true, 200m);
        }

        [Test]
        public void TestZoneAddTicketTypeCreatesAssociation()
        {
            mainZone1.AddTicketType(regularTicket);
            Assert.That(mainZone1.TicketTypes, Contains.Item(regularTicket));
        }

        [Test]
        public void TestTicketTypeAddZoneCreatesAssociation()
        {
            regularTicket.AddZone(mainZone1);
            Assert.That(regularTicket.AccessibleZones.Keys, Contains.Item(mainZone1.Name));
            Assert.That(mainZone1.TicketTypes, Contains.Item(regularTicket));
        }

        [Test]
        public void TestTicketTypeAddZoneOnNonMainZoneNonVIPThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => regularTicket.AddZone(childZone));
        }

        [Test]
        public void TestZoneRemoveTicketTypeClearsAssociation()
        {
            mainZone1.AddTicketType(regularTicket);
            mainZone1.AddTicketType(vipTicket);

            regularTicket.RemoveZone(mainZone1.Name);

            Assert.That(mainZone1.TicketTypes, Does.Not.Contain(regularTicket));
            Assert.That(mainZone1.TicketTypes, Contains.Item(vipTicket));
        }

        [Test]
        public void TestTicketTypeRemoveZoneClearsAssociation()
        {
            regularTicket.AddZone(mainZone1);
            regularTicket.AddZone(mainZone2);

            regularTicket.RemoveZone(mainZone1.Name);

            Assert.That(regularTicket.AccessibleZones.Keys, Does.Not.Contain(mainZone1.Name));
            Assert.That(regularTicket.AccessibleZones.Keys, Contains.Item(mainZone2.Name));
        }

        [Test]
        public void TestTicketTypeRemoveZoneThrowsException()
        {
            Assert.Throws<KeyNotFoundException>(() => regularTicket.RemoveZone("NonExistent"));
        }

        [Test]
        public void TestVipTicketTypeIncludesAllZones()
        {
            Assert.That(vipTicket.AccessibleZones.Keys, Contains.Item(mainZone1.Name));
            Assert.That(vipTicket.AccessibleZones.Keys, Contains.Item(mainZone2.Name));
            Assert.That(vipTicket.AccessibleZones.Keys, Contains.Item(childZone.Name));
        }

        [Test]
        public void TestVipTicketTypeIncludesExistingZonesOnly()
        {
            Assert.That(vipTicket.AccessibleZones.Keys, Contains.Item(mainZone1.Name));
            Assert.That(vipTicket.AccessibleZones.Keys, Contains.Item(mainZone2.Name));
            Assert.That(vipTicket.AccessibleZones.Keys, Contains.Item(childZone.Name));
        }

        [Test]
        public void TestNewZonesDoNotAutoAddToExistingVipTickets()
        {
            var newZone = new Zone("New Zone", "Fun", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            Assert.That(vipTicket.AccessibleZones.Keys, Does.Not.Contain(newZone.Name));
        }


        [Test]
        public void TestMultipleZonesWithSameTicketType()
        {
            regularTicket.AddZone(mainZone1);
            regularTicket.AddZone(mainZone2);

            Assert.That(regularTicket.AccessibleZones.Keys, Contains.Item(mainZone1.Name));
            Assert.That(regularTicket.AccessibleZones.Keys, Contains.Item(mainZone2.Name));
        }

        [Test]
        public void TestMultipleTicketTypesWithSameZone()
        {
            mainZone1.AddTicketType(regularTicket);
            mainZone1.AddTicketType(vipTicket);

            Assert.That(mainZone1.TicketTypes, Contains.Item(regularTicket));
            Assert.That(mainZone1.TicketTypes, Contains.Item(vipTicket));
        }

        [Test]
        public void TestTicketTypeAddZoneDuplicateThrowsException()
        {
            regularTicket.AddZone(mainZone1);
            Assert.Throws<InvalidOperationException>(() => regularTicket.AddZone(mainZone1));
        }

        [Test]
        public void TestAccessibleZonesIsReadOnly()
        {
            var zones = regularTicket.AccessibleZones;
            Assert.That(zones, Is.InstanceOf<IReadOnlyDictionary<string, Zone>>());
        }

        [Test]
        public void TestTicketTypesCollectionIsReadOnly()
        {
            var ticketTypes = mainZone1.TicketTypes;
            Assert.That(ticketTypes, Is.InstanceOf<IReadOnlyCollection<TicketType>>());
        }
    }
}
