
using Amusement_Park_System;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class FourDRidePersistenceTests
    {
        private string _filePath = FourDRide.FilePath;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            FourDRide.ClearExtent();
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            FourDRide.ClearExtent();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var ride1 = new FourDRide("Galaxy Adventure", 110, 25, true, 12.5, new List<string> { "Wind", "Water" });
            var ride2 = new FourDRide("Space Warp", 120, 20, false, 15.0, new List<string> { "Smoke" });

            Assert.That(FourDRide.Extent.Count, Is.EqualTo(2));
            Assert.That(FourDRide.Extent, Does.Contain(ride1));
            Assert.That(FourDRide.Extent, Does.Contain(ride2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var ride = new FourDRide("Galaxy Adventure", 110, 25, true, 12.5, new List<string> { "Wind", "Water" });

            var fromExtent = FourDRide.Extent.Single(r => r.Name == "Galaxy Adventure");

            ride.ShowDuration = 20.0;

            Assert.That(fromExtent.ShowDuration, Is.EqualTo(20.0));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var ride = new FourDRide("Galaxy Adventure", 110, 25, true, 12.5, new List<string> { "Wind" });

            FourDRide.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var ride1 = new FourDRide("Galaxy Adventure", 110, 25, true, 12.5, new List<string> { "Wind", "Water" });
            var ride2 = new FourDRide("Space Warp", 120, 20, false, 15.0, new List<string> { "Smoke" });

            FourDRide.Save();

            FourDRide.ClearExtent();
            FourDRide.Load();

            Assert.That(FourDRide.Extent.Count, Is.EqualTo(2));

            var galaxy = FourDRide.Extent.Single(r => r.Name == "Galaxy Adventure");
            Assert.That(galaxy.Height, Is.EqualTo(110));
            Assert.That(galaxy.MaxSeats, Is.EqualTo(25));
            Assert.That(galaxy.VipPassWorks, Is.True);
            Assert.That(galaxy.ShowDuration, Is.EqualTo(12.5));
            Assert.That(galaxy.EffectTypes.SequenceEqual(new List<string> { "Wind", "Water" }));

            var space = FourDRide.Extent.Single(r => r.Name == "Space Warp");
            Assert.That(space.Height, Is.EqualTo(120));
            Assert.That(space.MaxSeats, Is.EqualTo(20));
            Assert.That(space.VipPassWorks, Is.False);
            Assert.That(space.ShowDuration, Is.EqualTo(15.0));
            Assert.That(space.EffectTypes.SequenceEqual(new List<string> { "Smoke" }));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var ride = new FourDRide("Galaxy Adventure", 110, 25, true, 12.5, new List<string> { "Wind" });

            Assert.That(FourDRide.Extent, Is.Not.Empty);

            FourDRide.Load();

            Assert.That(FourDRide.Extent, Is.Empty);
        }
    }
}
