using Amusement_Park_System;

namespace Amusement_Park_System_Tests
{
    public class ZoneCompositionTests
    {
        [SetUp]
        public void Setup()
        {
            Zone.ClearExtent();
        }

        [Test]
        public void TestAddChildValidChildShouldSetParentChildRelationship()
        {
            var parentZone = new Zone("Main Park", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            var childZone = new Zone("Roller Coaster Area", "Thrill", TimeSpan.FromHours(10), TimeSpan.FromHours(20));
            
            parentZone.AddChild(childZone);
            
            Assert.That(parentZone.ChildZones, Contains.Item(childZone));
            Assert.That(childZone.ParentZone, Is.EqualTo(parentZone));
        }

        [Test]
        public void TestAddChildNullChildShouldThrowArgumentNullException()
        {
            var parentZone = new Zone("Main Park", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            
            Assert.Throws<ArgumentNullException>(() => parentZone.AddChild(null));
        }

        [Test]
        public void TestAddChildSelfReferenceShouldThrowArgumentException()
        {
            var zone = new Zone("Main Park", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            
            Assert.Throws<ArgumentException>(() => zone.AddChild(zone));
        }

        [Test]
        public void TestAddChildChildAlreadyHasParentShouldThrowArgumentException()
        {
            var parentZone1 = new Zone("Main Park", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            var parentZone2 = new Zone("Water Park", "Aquatic", TimeSpan.FromHours(10), TimeSpan.FromHours(20));
            var childZone = new Zone("Roller Coaster Area", "Thrill", TimeSpan.FromHours(11), TimeSpan.FromHours(19));
            
            parentZone1.AddChild(childZone);
            
            Assert.Throws<ArgumentException>(() => parentZone2.AddChild(childZone));
        }

        [Test]
        public void TestAddChildChildInheritsParentOperatingHours()
        {
            var parentZone = new Zone("Main Park", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            var childZone = new Zone("Roller Coaster Area", "Thrill", TimeSpan.FromHours(10), TimeSpan.FromHours(21));
            
            parentZone.AddChild(childZone);
            
            Assert.That(childZone.OpeningTime, Is.EqualTo(TimeSpan.FromHours(9)));
            Assert.That(childZone.ClosingTime, Is.EqualTo(TimeSpan.FromHours(22)));
        }

        [Test]
        public void TestRemoveChildValidChildShouldRemoveParentChildRelationship()
        {
            var parentZone = new Zone("Main Park", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            var childZone = new Zone("Roller Coaster Area", "Thrill", TimeSpan.FromHours(10), TimeSpan.FromHours(20));
            
            parentZone.AddChild(childZone);
            parentZone.RemoveChild(childZone);
            
            Assert.That(parentZone.ChildZones, Does.Not.Contain(childZone));
            Assert.That(childZone.ParentZone, Is.Null);
        }

        [Test]
        public void TestRemoveChildChildNotInCollectionShouldThrowInvalidOperationException()
        {
            var parentZone = new Zone("Main Park", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            var childZone = new Zone("Roller Coaster Area", "Thrill", TimeSpan.FromHours(11), TimeSpan.FromHours(19));
            
            Assert.Throws<InvalidOperationException>(() => parentZone.RemoveChild(childZone));
        }

        [Test]
        public void TestDeleteZoneChildZoneShouldRemoveFromParent()
        {
            var parentZone = new Zone("Main Park", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            var childZone = new Zone("Roller Coaster Area", "Thrill", TimeSpan.FromHours(10), TimeSpan.FromHours(20));
            
            parentZone.AddChild(childZone);
            childZone.DeleteZone();
            
            Assert.That(parentZone.ChildZones, Does.Not.Contain(childZone));
        }

        [Test]
        public void TestDeleteZoneParentWithChildrenShouldDeleteAllChildren()
        {
            var parentZone = new Zone("Main Park", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            var childZone1 = new Zone("Roller Coaster Area", "Thrill", TimeSpan.FromHours(10), TimeSpan.FromHours(20));
            var childZone2 = new Zone("Food Court", "Dining", TimeSpan.FromHours(11), TimeSpan.FromHours(21));
            var grandChildZone = new Zone("Pizza Stand", "Italian", TimeSpan.FromHours(11), TimeSpan.FromHours(21));
            
            parentZone.AddChild(childZone1);
            parentZone.AddChild(childZone2);
            childZone1.AddChild(grandChildZone);
            
            parentZone.DeleteZone();
            
            Assert.That(Zone.Extent.Count, Is.EqualTo(0));
        }
        

        [Test]
        public void TestAddChildDuplicateShouldNotBeAllowed()
        {
            var parentZone = new Zone("Main Park", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            var childZone = new Zone("Roller Coaster Area", "Thrill", TimeSpan.FromHours(10), TimeSpan.FromHours(20));
            
            parentZone.AddChild(childZone);
            
            Assert.Throws<ArgumentException>(() => parentZone.AddChild(childZone));
        }

        [Test]
        public void TestRemoveChildUpdatesReverseConnection()
        {
            var parentZone = new Zone("Main Park", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            var childZone = new Zone("Roller Coaster Area", "Thrill", TimeSpan.FromHours(10), TimeSpan.FromHours(20));
            
            parentZone.AddChild(childZone);
            parentZone.RemoveChild(childZone);
            
            Assert.That(childZone.ParentZone, Is.Null);
        }

        [Test]
        public void TestMultipleChildrenManagement()
        {
            var parentZone = new Zone("Main Park", "Adventure", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            var childZone1 = new Zone("Area 1", "Theme1", TimeSpan.FromHours(10), TimeSpan.FromHours(20));
            var childZone2 = new Zone("Area 2", "Theme2", TimeSpan.FromHours(11), TimeSpan.FromHours(21));
            var childZone3 = new Zone("Area 3", "Theme3", TimeSpan.FromHours(12), TimeSpan.FromHours(22));
            
            parentZone.AddChild(childZone1);
            parentZone.AddChild(childZone2);
            parentZone.AddChild(childZone3);
            
            Assert.That(parentZone.ChildZones.Count, Is.EqualTo(3));
        }

        [Test]
        public void TestHierarchicalDeletion()
        {
            var level1 = new Zone("Level1", "Theme", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            var level2 = new Zone("Level2", "Theme", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            var level3 = new Zone("Level3", "Theme", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
            
            level1.AddChild(level2);
            level2.AddChild(level3);
            level1.DeleteZone();
            
            Assert.That(Zone.Extent.Count, Is.EqualTo(0));
        }
    }
}