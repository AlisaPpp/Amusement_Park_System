
using Amusement_Park_System;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class RollerCoasterPersistenceTests
    {
        private string _filePath = RollerCoaster.FilePath;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            RollerCoaster.ClearExtent();
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            RollerCoaster.ClearExtent();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var rc1 = new RollerCoaster("Thunder Loop", 130, 24, true, 900.0, 110.0, 3);
            var rc2 = new RollerCoaster("Dragon Twist", 150, 20, false, 1200.0, 95.0, 5);

            Assert.That(RollerCoaster.Extent.Count, Is.EqualTo(2));
            Assert.That(RollerCoaster.Extent, Does.Contain(rc1));
            Assert.That(RollerCoaster.Extent, Does.Contain(rc2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var rc = new RollerCoaster("Thunder Loop", 130, 24, true, 900.0, 110.0, 3);

            var fromExtent = RollerCoaster.Extent.Single(r => r.Name == "Thunder Loop");

            rc.MaxSpeed = 120.0;

            Assert.That(fromExtent.MaxSpeed, Is.EqualTo(120.0));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var rc = new RollerCoaster("Thunder Loop", 130, 24, true, 900.0, 110.0, 3);

            RollerCoaster.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var rc1 = new RollerCoaster("Thunder Loop", 130, 24, true, 900.0, 110.0, 3);
            var rc2 = new RollerCoaster("Dragon Twist", 150, 20, false, 1200.0, 95.0, 5);

            RollerCoaster.Save();

            RollerCoaster.ClearExtent();
            RollerCoaster.Load();

            Assert.That(RollerCoaster.Extent.Count, Is.EqualTo(2));

            var thunder = RollerCoaster.Extent.Single(r => r.Name == "Thunder Loop");
            Assert.That(thunder.Height, Is.EqualTo(130));
            Assert.That(thunder.MaxSeats, Is.EqualTo(24));
            Assert.That(thunder.VipPassWorks, Is.True);
            Assert.That(thunder.TrackLength, Is.EqualTo(900.0));
            Assert.That(thunder.MaxSpeed, Is.EqualTo(110.0));
            Assert.That(thunder.NumberOfLoops, Is.EqualTo(3));

            var dragon = RollerCoaster.Extent.Single(r => r.Name == "Dragon Twist");
            Assert.That(dragon.Height, Is.EqualTo(150));
            Assert.That(dragon.MaxSeats, Is.EqualTo(20));
            Assert.That(dragon.VipPassWorks, Is.False);
            Assert.That(dragon.TrackLength, Is.EqualTo(1200.0));
            Assert.That(dragon.MaxSpeed, Is.EqualTo(95.0));
            Assert.That(dragon.NumberOfLoops, Is.EqualTo(5));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var rc = new RollerCoaster("Thunder Loop", 130, 24, true, 900.0, 110.0, 3);

            Assert.That(RollerCoaster.Extent, Is.Not.Empty);

            RollerCoaster.Load();

            Assert.That(RollerCoaster.Extent, Is.Empty);
        }
    }
}
