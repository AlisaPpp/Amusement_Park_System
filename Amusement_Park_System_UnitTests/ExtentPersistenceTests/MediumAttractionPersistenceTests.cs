
using Amusement_Park_System;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class MediumAttractionPersistenceTests
    {
        private string _filePath = MediumAttraction.FilePath;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            MediumAttraction.ClearExtent();
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            MediumAttraction.ClearExtent();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var attraction1 = new MediumAttraction("Haunted House", 130, 20, true, true);
            var attraction2 = new MediumAttraction("Spinning Cups", 125, 16, false, true);

            Assert.That(MediumAttraction.Extent.Count, Is.EqualTo(2));
            Assert.That(MediumAttraction.Extent, Does.Contain(attraction1));
            Assert.That(MediumAttraction.Extent, Does.Contain(attraction2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var attraction = new MediumAttraction("Haunted House", 130, 20, true, true);

            var fromExtent = MediumAttraction.Extent.Single(a => a.Name == "Haunted House");

            attraction.FamilyFriendly = false;

            Assert.That(fromExtent.FamilyFriendly, Is.EqualTo(false));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var attraction = new MediumAttraction("Haunted House", 130, 20, true, true);

            MediumAttraction.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var attraction1 = new MediumAttraction("Haunted House", 130, 20, true, true);
            var attraction2 = new MediumAttraction("Spinning Cups", 125, 16, false, true);

            MediumAttraction.Save();

            MediumAttraction.ClearExtent();
            MediumAttraction.Load();

            Assert.That(MediumAttraction.Extent.Count, Is.EqualTo(2));

            var haunted = MediumAttraction.Extent.Single(a => a.Name == "Haunted House");
            Assert.That(haunted.Height, Is.EqualTo(130));
            Assert.That(haunted.MaxSeats, Is.EqualTo(20));
            Assert.That(haunted.VipPassWorks, Is.True);
            Assert.That(haunted.FamilyFriendly, Is.True);

            var cups = MediumAttraction.Extent.Single(a => a.Name == "Spinning Cups");
            Assert.That(cups.Height, Is.EqualTo(125));
            Assert.That(cups.MaxSeats, Is.EqualTo(16));
            Assert.That(cups.VipPassWorks, Is.False);
            Assert.That(cups.FamilyFriendly, Is.True);
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var attraction = new MediumAttraction("Haunted House", 130, 20, true, true);

            Assert.That(MediumAttraction.Extent, Is.Not.Empty);

            MediumAttraction.Load();

            Assert.That(MediumAttraction.Extent, Is.Empty);
        }
    }
}
