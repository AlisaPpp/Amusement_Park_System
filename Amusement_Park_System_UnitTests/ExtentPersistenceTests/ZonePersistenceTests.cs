
using Amusement_Park_System;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class ZonePersistenceTests
    {
        private string _filePath = Zone.FilePath;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Zone.ClearExtent();
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Zone.ClearExtent();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var zone1 = new Zone(
                "Adventure Zone",
                "Adventure",
                new TimeSpan(9, 0, 0),
                new TimeSpan(18, 0, 0));

            var zone2 = new Zone(
                "Kids Zone",
                "Family",
                new TimeSpan(10, 0, 0),
                new TimeSpan(20, 0, 0));

            Assert.That(Zone.Extent.Count, Is.EqualTo(2));
            Assert.That(Zone.Extent, Does.Contain(zone1));
            Assert.That(Zone.Extent, Does.Contain(zone2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var zone = new Zone(
                "Adventure Zone",
                "Adventure",
                new TimeSpan(9, 0, 0),
                new TimeSpan(18, 0, 0));

            var fromExtent = Zone.Extent.Single(z => z.Name == "Adventure Zone");

            zone.Theme = "Extreme Adventure";

            Assert.That(fromExtent.Theme, Is.EqualTo("Extreme Adventure"));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var zone = new Zone(
                "Adventure Zone",
                "Adventure",
                new TimeSpan(9, 0, 0),
                new TimeSpan(18, 0, 0));

            Zone.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var zone1 = new Zone(
                "Adventure Zone",
                "Adventure",
                new TimeSpan(9, 0, 0),
                new TimeSpan(18, 0, 0));

            var zone2 = new Zone(
                "Kids Zone",
                "Family",
                new TimeSpan(10, 0, 0),
                new TimeSpan(20, 0, 0));

            Zone.Save();

            Zone.ClearExtent();
            Zone.Load();

            Assert.That(Zone.Extent.Count, Is.EqualTo(2));

            var adventure = Zone.Extent.Single(z => z.Name == "Adventure Zone");
            Assert.That(adventure.Theme, Is.EqualTo("Adventure"));
            Assert.That(adventure.OpeningTime, Is.EqualTo(new TimeSpan(9, 0, 0)));
            Assert.That(adventure.ClosingTime, Is.EqualTo(new TimeSpan(18, 0, 0)));

            var kids = Zone.Extent.Single(z => z.Name == "Kids Zone");
            Assert.That(kids.Theme, Is.EqualTo("Family"));
            Assert.That(kids.OpeningTime, Is.EqualTo(new TimeSpan(10, 0, 0)));
            Assert.That(kids.ClosingTime, Is.EqualTo(new TimeSpan(20, 0, 0)));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var zone = new Zone(
                "Adventure Zone",
                "Adventure",
                new TimeSpan(9, 0, 0),
                new TimeSpan(18, 0, 0));

            Assert.That(Zone.Extent, Is.Not.Empty);

            Zone.Load();

            Assert.That(Zone.Extent, Is.Empty);
        }
    }
}
