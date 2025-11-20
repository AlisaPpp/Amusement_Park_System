using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Amusement_Park_System;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class WaterRidePersistenceTests
    {
        private string _filePath = WaterRide.FilePath;

        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);
            WaterRide.Extent = new List<WaterRide>();
        }

        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);
            WaterRide.Extent = new List<WaterRide>();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var ride1 = new WaterRide("Splash Mountain", 120, 20, true, 2.5, 22.0);
            var ride2 = new WaterRide("River Rapids", 100, 15, false, 3.0, 18.5);

            Assert.That(WaterRide.Extent.Count, Is.EqualTo(2));
            Assert.That(WaterRide.Extent, Does.Contain(ride1));
            Assert.That(WaterRide.Extent, Does.Contain(ride2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var ride = new WaterRide("Splash Mountain", 120, 20, true, 2.5, 22.0);

            var fromExtent = WaterRide.Extent.Single(r => r.Name == "Splash Mountain");

            ride.WaterDepth = 3.5;

            Assert.That(fromExtent.WaterDepth, Is.EqualTo(3.5));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var ride = new WaterRide("Splash Mountain", 120, 20, true, 2.5, 22.0);

            WaterRide.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var ride1 = new WaterRide("Splash Mountain", 120, 20, true, 2.5, 22.0);
            var ride2 = new WaterRide("River Rapids", 100, 15, false, 3.0, 18.5);

            WaterRide.Save();

            WaterRide.Extent = new List<WaterRide>();
            WaterRide.Load();

            Assert.That(WaterRide.Extent.Count, Is.EqualTo(2));

            var splash = WaterRide.Extent.Single(r => r.Name == "Splash Mountain");
            Assert.That(splash.Height, Is.EqualTo(120));
            Assert.That(splash.MaxSeats, Is.EqualTo(20));
            Assert.That(splash.VipPassWorks, Is.True);
            Assert.That(splash.WaterDepth, Is.EqualTo(2.5));
            Assert.That(splash.WaterTemperature, Is.EqualTo(22.0));

            var river = WaterRide.Extent.Single(r => r.Name == "River Rapids");
            Assert.That(river.Height, Is.EqualTo(100));
            Assert.That(river.MaxSeats, Is.EqualTo(15));
            Assert.That(river.VipPassWorks, Is.False);
            Assert.That(river.WaterDepth, Is.EqualTo(3.0));
            Assert.That(river.WaterTemperature, Is.EqualTo(18.5));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var ride = new WaterRide("Splash Mountain", 120, 20, true, 2.5, 22.0);

            Assert.That(WaterRide.Extent, Is.Not.Empty);

            WaterRide.Load();

            Assert.That(WaterRide.Extent, Is.Empty);
        }
    }
}
