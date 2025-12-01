
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class MaintenanceStaffPersistenceTests
    {
        private string _filePath = MaintenanceStaff.FilePath;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            MaintenanceStaff.ClearExtent();
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            MaintenanceStaff.ClearExtent();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var staff1 = new MaintenanceStaff(
                "Lucas",
                "Brown",
                "lucas@example.com",
                new DateTime(1987, 6, 12),
                4,
                "Mechanical",
                new List<string> { "Welding" });

            var staff2 = new MaintenanceStaff(
                "Emma",
                "Green",
                "emma@example.com",
                new DateTime(1990, 11, 3),
                6,
                "Electrical",
                new List<string> { "HV Certification" });

            Assert.That(MaintenanceStaff.Extent.Count, Is.EqualTo(2));
            Assert.That(MaintenanceStaff.Extent, Does.Contain(staff1));
            Assert.That(MaintenanceStaff.Extent, Does.Contain(staff2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var staff = new MaintenanceStaff(
                "Lucas",
                "Brown",
                "lucas@example.com",
                new DateTime(1987, 6, 12),
                4,
                "Mechanical",
                new List<string> { "Welding" });

            var fromExtent = MaintenanceStaff.Extent.Single(s => s.Name == "Lucas");

            staff.Specialization = "Hydraulics";

            Assert.That(fromExtent.Specialization, Is.EqualTo("Hydraulics"));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var staff = new MaintenanceStaff(
                "Lucas",
                "Brown",
                "lucas@example.com",
                new DateTime(1987, 6, 12),
                4,
                "Mechanical",
                new List<string> { "Welding" });

            MaintenanceStaff.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var staff1 = new MaintenanceStaff(
                "Lucas",
                "Brown",
                "lucas@example.com",
                new DateTime(1987, 6, 12),
                4,
                "Mechanical",
                new List<string> { "Welding", "Safety Training" });

            var staff2 = new MaintenanceStaff(
                "Emma",
                "Green",
                "emma@example.com",
                new DateTime(1990, 11, 3),
                6,
                "Electrical",
                new List<string> { "HV Certification" });

            MaintenanceStaff.Save();

            MaintenanceStaff.ClearExtent();
            MaintenanceStaff.Load();

            Assert.That(MaintenanceStaff.Extent.Count, Is.EqualTo(2));

            var lucas = MaintenanceStaff.Extent.Single(s => s.Name == "Lucas");
            Assert.That(lucas.Surname, Is.EqualTo("Brown"));
            Assert.That(lucas.Specialization, Is.EqualTo("Mechanical"));
            Assert.That(lucas.Certifications!.SequenceEqual(new List<string> { "Welding", "Safety Training" }));

            var emma = MaintenanceStaff.Extent.Single(s => s.Name == "Emma");
            Assert.That(emma.Surname, Is.EqualTo("Green"));
            Assert.That(emma.Specialization, Is.EqualTo("Electrical"));
            Assert.That(emma.Certifications!.SequenceEqual(new List<string> { "HV Certification" }));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var staff = new MaintenanceStaff(
                "Lucas",
                "Brown",
                "lucas@example.com",
                new DateTime(1987, 6, 12),
                4,
                "Mechanical",
                new List<string> { "Welding" });

            Assert.That(MaintenanceStaff.Extent, Is.Not.Empty);

            MaintenanceStaff.Load();

            Assert.That(MaintenanceStaff.Extent, Is.Empty);
        }
    }
}
