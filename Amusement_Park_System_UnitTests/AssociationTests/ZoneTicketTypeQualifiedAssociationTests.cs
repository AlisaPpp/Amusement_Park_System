using System;
using Amusement_Park_System;
using NUnit.Framework;
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class ZoneTicketTypeQualifiedAssociationTests
    {
        private Zone zone1;
        private Zone zone2;
        private Zone childZone;
        private TicketType ticketType1;
        private TicketType ticketType2;

        [SetUp]
        public void Setup()
        {
            Zone.ClearExtent();
            TicketType.ClearExtent();
            
            zone1 = new Zone("Main Zone", "Adventure", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            zone2 = new Zone("Kids Zone", "Children", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            childZone = new Zone("Child Zone", "Small", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            
            ticketType1 = new TicketType("Regular", false, 100m);
            ticketType2 = new TicketType("VIP", true, 200m);
        }

        [Test]
        public void TestZoneAddTicketTypeCreatesAssociation()
        {
            zone1.AddTicketType(ticketType1);
            
            Assert.That(zone1.TicketTypes, Contains.Item(ticketType1));
        }

        [Test]
        public void TestTicketTypeAddZoneCreatesAssociation()
        {
            ticketType1.AddZone(zone1);
            
            Assert.That(ticketType1.AccessibleZones.Keys, Contains.Item(zone1.Name));
        }

        [Test]
        public void TestTicketTypeAddZoneOnMainZoneWorks()
        {
            zone1.AddChild(childZone);
            
            ticketType1.AddZone(zone1);
            
            Assert.That(ticketType1.AccessibleZones.Keys, Contains.Item(zone1.Name));
        }

        [Test]
        public void TestTicketTypeAddZoneOnNonMainZoneNonVIPThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => ticketType1.AddZone(childZone));
        }
        

        [Test]
        public void TestZoneRemoveTicketTypeClearsAssociation()
        {
            zone1.AddTicketType(ticketType1);
            zone1.AddTicketType(ticketType2);
            
            ticketType1.RemoveZone(zone1.Name);
            
            Assert.That(zone1.TicketTypes, Does.Not.Contain(ticketType1));
            Assert.That(zone1.TicketTypes, Contains.Item(ticketType2));
        }

        [Test]
        public void TestTicketTypeRemoveZoneClearsAssociation()
        {
            ticketType1.AddZone(zone1);
            ticketType1.AddZone(zone2);
            
            ticketType1.RemoveZone(zone1.Name);
            
            Assert.That(ticketType1.AccessibleZones.Keys, Does.Not.Contain(zone1.Name));
            Assert.That(ticketType1.AccessibleZones.Keys, Contains.Item(zone2.Name));
        }

        [Test]
        public void TestTicketTypeRemoveZoneThrowsException()
        {
            Assert.Throws<KeyNotFoundException>(() => ticketType1.RemoveZone("NonExistent"));
        }

        [Test]
        public void TestVipTicketTypeIncludesAllZones()
        {
            Assert.That(ticketType2.AccessibleZones.Keys, Contains.Item(zone1.Name));
            Assert.That(ticketType2.AccessibleZones.Keys, Contains.Item(zone2.Name));
            Assert.That(ticketType2.AccessibleZones.Keys, Contains.Item(childZone.Name));
        }

        [Test]
        public void TestVipTicketTypeAutoAddedToNewZone()
        {
            var newZone = new Zone("New Zone", "Fun", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            
            Assert.That(ticketType2.AccessibleZones.Keys, Contains.Item(newZone.Name));
        }

        [Test]
        public void TestMultipleZonesWithSameTicketType()
        {
            ticketType1.AddZone(zone1);
            ticketType1.AddZone(zone2);
            
            Assert.That(ticketType1.AccessibleZones.Keys, Contains.Item(zone1.Name));
            Assert.That(ticketType1.AccessibleZones.Keys, Contains.Item(zone2.Name));
        }

        [Test]
        public void TestMultipleTicketTypesWithSameZone()
        {
            zone1.AddTicketType(ticketType1);
            zone1.AddTicketType(ticketType2);
            
            Assert.That(zone1.TicketTypes, Contains.Item(ticketType1));
            Assert.That(zone1.TicketTypes, Contains.Item(ticketType2));
        }

        [Test]
        public void TestTicketTypeAddZoneDuplicateThrowsException()
        {
            ticketType1.AddZone(zone1);
            
            Assert.Throws<InvalidOperationException>(() => ticketType1.AddZone(zone1));
        }

        [Test]
        public void TestAccessibleZonesIsReadOnly()
        {
            var zones = ticketType1.AccessibleZones;
            Assert.That(zones, Is.InstanceOf<IReadOnlyDictionary<string, Zone>>());
        }

        [Test]
        public void TestTicketTypesCollectionIsReadOnly()
        {
            var ticketTypes = zone1.TicketTypes;
            Assert.That(ticketTypes, Is.InstanceOf<IReadOnlyCollection<TicketType>>());
        }
    }
}