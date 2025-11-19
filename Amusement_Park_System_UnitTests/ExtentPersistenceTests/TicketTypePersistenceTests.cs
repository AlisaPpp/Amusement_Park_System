
using Amusement_Park_System.Models;


namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class TicketTypePersistenceTests
    {
        private string _filePath = TicketType.FilePath;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            TicketType.Extent = new List<TicketType>();
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            TicketType.Extent = new List<TicketType>();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var standard = new TicketType("Standard", false, 50m);
            var vip = new TicketType("VIP", true, 120m);

            Assert.That(TicketType.Extent.Count, Is.EqualTo(2));
            Assert.That(TicketType.Extent, Does.Contain(standard));
            Assert.That(TicketType.Extent, Does.Contain(vip));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var standard = new TicketType("Standard", false, 50m);

            var fromExtent = TicketType.Extent.Single(t => t.TypeName == "Standard");

            standard.InitialPrice = 60m;

            Assert.That(fromExtent.InitialPrice, Is.EqualTo(60m));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var standard = new TicketType("Standard", false, 50m);

            TicketType.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var standard = new TicketType("Standard", false, 50m);
            var vip = new TicketType("VIP", true, 120m);

            TicketType.Save();

            TicketType.Extent = new List<TicketType>();
            TicketType.Load();

            Assert.That(TicketType.Extent.Count, Is.EqualTo(2));

            var loadedStandard = TicketType.Extent.Single(t => t.TypeName == "Standard");
            Assert.That(loadedStandard.IsVip, Is.False);
            Assert.That(loadedStandard.InitialPrice, Is.EqualTo(50m));

            var loadedVip = TicketType.Extent.Single(t => t.TypeName == "VIP");
            Assert.That(loadedVip.IsVip, Is.True);
            Assert.That(loadedVip.InitialPrice, Is.EqualTo(120m));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var standard = new TicketType("Standard", false, 50m);

            Assert.That(TicketType.Extent, Is.Not.Empty);

            TicketType.Load();

            Assert.That(TicketType.Extent, Is.Empty);
        }
    }
}
