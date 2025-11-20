
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class MalfunctionReportPersistenceTests
    {
        private string _filePath = MalfunctionReport.FilePath;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            MalfunctionReport.Extent = new List<MalfunctionReport>();
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            MalfunctionReport.Extent = new List<MalfunctionReport>();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var report1 = new MalfunctionReport(
                "Mechanical",
                "Roller coaster chain broke.",
                new DateTime(2024, 1, 1));

            var report2 = new MalfunctionReport(
                "Electrical",
                "Power outage in Zone A.",
                new DateTime(2024, 2, 15));

            Assert.That(MalfunctionReport.Extent.Count, Is.EqualTo(2));
            Assert.That(MalfunctionReport.Extent, Does.Contain(report1));
            Assert.That(MalfunctionReport.Extent, Does.Contain(report2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var report = new MalfunctionReport(
                "Mechanical",
                "Roller coaster chain broke.",
                new DateTime(2024, 1, 1));

            var fromExtent = MalfunctionReport.Extent.Single(r => r.Type == "Mechanical");

            report.Description = "Roller coaster chain replaced after break.";

            Assert.That(fromExtent.Description, Is.EqualTo("Roller coaster chain replaced after break."));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var report = new MalfunctionReport(
                "Mechanical",
                "Roller coaster chain broke.",
                new DateTime(2024, 1, 1));

            MalfunctionReport.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var report1 = new MalfunctionReport(
                "Mechanical",
                "Roller coaster chain broke.",
                new DateTime(2024, 1, 1));

            var report2 = new MalfunctionReport(
                "Electrical",
                "Power outage in Zone A.",
                new DateTime(2024, 2, 15));

            MalfunctionReport.Save();

            MalfunctionReport.Extent = new List<MalfunctionReport>();
            MalfunctionReport.Load();

            Assert.That(MalfunctionReport.Extent.Count, Is.EqualTo(2));

            var mechanical = MalfunctionReport.Extent.Single(r => r.Type == "Mechanical");
            Assert.That(mechanical.Description, Is.EqualTo("Roller coaster chain broke."));
            Assert.That(mechanical.Date, Is.EqualTo(new DateTime(2024, 1, 1)));

            var electrical = MalfunctionReport.Extent.Single(r => r.Type == "Electrical");
            Assert.That(electrical.Description, Is.EqualTo("Power outage in Zone A."));
            Assert.That(electrical.Date, Is.EqualTo(new DateTime(2024, 2, 15)));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var report = new MalfunctionReport(
                "Mechanical",
                "Roller coaster chain broke.",
                new DateTime(2024, 1, 1));

            Assert.That(MalfunctionReport.Extent, Is.Not.Empty);

            MalfunctionReport.Load();

            Assert.That(MalfunctionReport.Extent, Is.Empty);
        }
    }
}
