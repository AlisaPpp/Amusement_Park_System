using Amusement_Park_System;

namespace Amusement_Park_System_Tests
{
    public class ZoneReflexTests
    {
        [SetUp]
        public void Setup()
        {
            Zone.ClearExtent();
        }

        [Test]
        public void TestSetNextZoneValidZoneShouldSetNextZone()
        {
            var zone1 = new Zone("Zone A", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(18));
            var zone2 = new Zone("Zone B", "Fantasy", TimeSpan.FromHours(10), TimeSpan.FromHours(19));
            
            zone1.SetNextZone(zone2);
            
            Assert.That(zone1.NextZone, Is.EqualTo(zone2));
        }

        [Test]
        public void TestSetNextZoneNullZoneShouldThrowArgumentNullException()
        {
            var zone1 = new Zone("Zone A", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(18));
            
            Assert.Throws<ArgumentNullException>(() => zone1.SetNextZone(null));
        }

        [Test]
        public void TestSetNextZoneSelfReferenceShouldThrowArgumentException()
        {
            var zone = new Zone("Zone A", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(18));
            
            Assert.Throws<ArgumentException>(() => zone.SetNextZone(zone));
        }

        [Test]
        public void TestSetNextZoneCreatesCycleShouldThrowInvalidOperationException()
        {
            var zone1 = new Zone("Zone A", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(18));
            var zone2 = new Zone("Zone B", "Fantasy", TimeSpan.FromHours(10), TimeSpan.FromHours(19));
            var zone3 = new Zone("Zone C", "Sci-Fi", TimeSpan.FromHours(11), TimeSpan.FromHours(20));
            
            zone1.SetNextZone(zone2);
            zone2.SetNextZone(zone3);
            
            Assert.Throws<InvalidOperationException>(() => zone3.SetNextZone(zone1));
        }

        [Test]
        public void TestSetNextZoneNoCycleShouldAllowChain()
        {
            var zone1 = new Zone("Zone A", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(18));
            var zone2 = new Zone("Zone B", "Fantasy", TimeSpan.FromHours(10), TimeSpan.FromHours(19));
            var zone3 = new Zone("Zone C", "Sci-Fi", TimeSpan.FromHours(11), TimeSpan.FromHours(20));
            
            zone1.SetNextZone(zone2);
            zone2.SetNextZone(zone3);
            
            Assert.That(zone1.NextZone, Is.EqualTo(zone2));
            Assert.That(zone2.NextZone, Is.EqualTo(zone3));
            Assert.That(zone3.NextZone, Is.Null);
        }

        [Test]
        public void TestSetNextZoneReplacesExistingNextZone()
        {
            var zone1 = new Zone("Zone A", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(18));
            var zone2 = new Zone("Zone B", "Fantasy", TimeSpan.FromHours(10), TimeSpan.FromHours(19));
            var zone3 = new Zone("Zone C", "Sci-Fi", TimeSpan.FromHours(11), TimeSpan.FromHours(20));
            
            zone1.SetNextZone(zone2);
            zone1.SetNextZone(zone3);
            
            Assert.That(zone1.NextZone, Is.EqualTo(zone3));
        }

        [Test]
        public void TestClearNextZoneShouldSetNextZoneToNull()
        {
            var zone1 = new Zone("Zone A", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(18));
            var zone2 = new Zone("Zone B", "Fantasy", TimeSpan.FromHours(10), TimeSpan.FromHours(19));
            
            zone1.SetNextZone(zone2);
            zone1.ClearNextZone();
            
            Assert.That(zone1.NextZone, Is.Null);
        }

        [Test]
        public void TestDeleteZoneWithNextZoneShouldClearNextZone()
        {
            var zone1 = new Zone("Zone A", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(18));
            var zone2 = new Zone("Zone B", "Fantasy", TimeSpan.FromHours(10), TimeSpan.FromHours(19));
            
            zone1.SetNextZone(zone2);
            zone1.DeleteZone();
            
            Assert.That(Zone.Extent, Does.Not.Contain(zone1));
        }
        

        [Test]
        public void TestReflexAssociationIndependentOfComposition()
        {
            var parentZone = new Zone("Parent", "Theme", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            var childZone = new Zone("Child", "Theme", TimeSpan.FromHours(10), TimeSpan.FromHours(21));
            var nextZone = new Zone("Next", "Theme", TimeSpan.FromHours(11), TimeSpan.FromHours(20));
            
            parentZone.AddChild(childZone);
            parentZone.SetNextZone(nextZone);
            
            Assert.That(parentZone.NextZone, Is.EqualTo(nextZone));
        }

        [Test]
        public void TestReflexConnectionModification()
        {
            var zone1 = new Zone("Zone A", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(18));
            var zone2 = new Zone("Zone B", "Fantasy", TimeSpan.FromHours(10), TimeSpan.FromHours(19));
            var zone3 = new Zone("Zone C", "Sci-Fi", TimeSpan.FromHours(11), TimeSpan.FromHours(20));
            
            zone1.SetNextZone(zone2);
            zone1.SetNextZone(zone3);
            
            Assert.That(zone1.NextZone, Is.EqualTo(zone3));
        }

        [Test]
        public void TestReflexAssociationBidirectional()
        {
            var zone1 = new Zone("Zone A", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(18));
            var zone2 = new Zone("Zone B", "Fantasy", TimeSpan.FromHours(10), TimeSpan.FromHours(19));
            
            zone1.SetNextZone(zone2);
            
            Assert.Throws<InvalidOperationException>(() => zone2.SetNextZone(zone1));
        }

        [Test]
        public void TestExtentContainsAllZones()
        {
            var zone1 = new Zone("Zone A", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(18));
            var zone2 = new Zone("Zone B", "Fantasy", TimeSpan.FromHours(10), TimeSpan.FromHours(19));
            
            zone1.SetNextZone(zone2);
            
            Assert.That(Zone.Extent.Count, Is.EqualTo(2));
            Assert.That(Zone.Extent, Contains.Item(zone1));
            Assert.That(Zone.Extent, Contains.Item(zone2));
        }
    }
}