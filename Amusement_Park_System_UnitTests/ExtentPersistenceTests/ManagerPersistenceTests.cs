
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class ManagerPersistenceTests
    {
        private string _filePath = Manager.FilePath;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Manager.ClearExtent();
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Manager.ClearExtent();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var manager1 = new Manager(
                "Alice",
                "Johnson",
                "alice@example.com",
                new DateTime(1985, 5, 12),
                5);

            var manager2 = new Manager(
                "Bob",
                "Smith",
                "bob@example.com",
                new DateTime(1980, 3, 22),
                10);

            Assert.That(Manager.Extent.Count, Is.EqualTo(2));
            Assert.That(Manager.Extent, Does.Contain(manager1));
            Assert.That(Manager.Extent, Does.Contain(manager2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var manager = new Manager(
                "Alice",
                "Johnson",
                "alice@example.com",
                new DateTime(1985, 5, 12),
                5);

            var fromExtent = Manager.Extent.Single(m => m.Name == "Alice");

            manager.YearsOfExperience = 8;

            Assert.That(fromExtent.YearsOfExperience, Is.EqualTo(8));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var manager = new Manager(
                "Alice",
                "Johnson",
                "alice@example.com",
                new DateTime(1985, 5, 12),
                5);

            Manager.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var manager1 = new Manager(
                "Alice",
                "Johnson",
                "alice@example.com",
                new DateTime(1985, 5, 12),
                5);

            var manager2 = new Manager(
                "Bob",
                "Smith",
                "bob@example.com",
                new DateTime(1980, 3, 22),
                10);

            Manager.Save();

            Manager.ClearExtent();
            Manager.Load();

            Assert.That(Manager.Extent.Count, Is.EqualTo(2));

            var alice = Manager.Extent.Single(m => m.Name == "Alice");
            Assert.That(alice.Surname, Is.EqualTo("Johnson"));
            Assert.That(alice.ContactInfo, Is.EqualTo("alice@example.com"));
            Assert.That(alice.YearsOfExperience, Is.EqualTo(5));

            var bob = Manager.Extent.Single(m => m.Name == "Bob");
            Assert.That(bob.Surname, Is.EqualTo("Smith"));
            Assert.That(bob.ContactInfo, Is.EqualTo("bob@example.com"));
            Assert.That(bob.YearsOfExperience, Is.EqualTo(10));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var manager = new Manager(
                "Alice",
                "Johnson",
                "alice@example.com",
                new DateTime(1985, 5, 12),
                5);

            Assert.That(Manager.Extent, Is.Not.Empty);

            Manager.Load();

            Assert.That(Manager.Extent, Is.Empty);
        }
    }
}
