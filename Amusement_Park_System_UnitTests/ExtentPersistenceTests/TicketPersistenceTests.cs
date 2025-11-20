
using Amusement_Park_System.Models;


namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class TicketPersistenceTests
    {
        private string _filePath = Ticket.FilePath;

        // before each test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Ticket.Extent = new List<Ticket>();
        }

        // after each test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Ticket.Extent = new List<Ticket>();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var t1 = new Ticket(DateTime.Parse("2026-01-08"), DateTime.Parse("2026-01-09"), 10, 2, 50m);
            var t2 = new Ticket(DateTime.Parse("2026-01-08"), DateTime.Parse("2026-01-09"), 20, 5, 120m);

            Assert.That(Ticket.Extent.Count, Is.EqualTo(2));
            Assert.That(Ticket.Extent, Does.Contain(t1));
            Assert.That(Ticket.Extent, Does.Contain(t2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var t = new Ticket(DateTime.Parse("2026-01-08"), DateTime.Parse("2026-01-09"), 10, 2, 50m);

            var fromExtent = Ticket.Extent.Single(x => x.StartDate == t.StartDate);

            t.Quantity = 10;

            Assert.That(fromExtent.Quantity, Is.EqualTo(10));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var t = new Ticket(DateTime.Parse("2026-01-08"), DateTime.Parse("2026-01-09"), 10, 2, 50m);

            Ticket.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var t1 = new Ticket(DateTime.Parse("2026-01-08"), DateTime.Parse("2026-01-09"),10, 2, 50m);
            var t2 = new Ticket(DateTime.Parse("2026-01-08"), DateTime.Parse("2026-01-09"), 20, 4, 100m);

            Ticket.Save();

            Ticket.Extent = new List<Ticket>();
            Ticket.Load();

            Assert.That(Ticket.Extent.Count, Is.EqualTo(2));

            var loaded1 = Ticket.Extent.Single(x => x.PersonalDiscount == 10);
            Assert.That(loaded1.Quantity, Is.EqualTo(2));
            Assert.That(loaded1.Price, Is.EqualTo(50m));
            Assert.That(loaded1.StartDate, Is.EqualTo(DateTime.Parse("2026-01-08")));
            Assert.That(loaded1.EndDate, Is.EqualTo(DateTime.Parse("2026-01-09")));

            var loaded2 = Ticket.Extent.Single(x => x.PersonalDiscount == 20);
            Assert.That(loaded2.Quantity, Is.EqualTo(4));
            Assert.That(loaded2.Price, Is.EqualTo(100m));
            Assert.That(loaded2.StartDate, Is.EqualTo(DateTime.Parse("2026-01-08")));
            Assert.That(loaded2.EndDate, Is.EqualTo(DateTime.Parse("2026-01-09")));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var t = new Ticket(DateTime.Parse("2026-01-08"), DateTime.Parse("2026-01-09"), 10, 2, 50m);

            Assert.That(Ticket.Extent, Is.Not.Empty);

            Ticket.Load();

            Assert.That(Ticket.Extent, Is.Empty);
        }
    }
}
