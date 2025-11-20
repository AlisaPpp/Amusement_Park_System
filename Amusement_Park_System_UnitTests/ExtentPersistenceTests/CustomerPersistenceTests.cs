
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class CustomerPersistenceTests
    {
        private string _filePath = Customer.FilePath;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Customer.Extent = new List<Customer>();
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Customer.Extent = new List<Customer>();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var customer1 = new Customer("John", "Doe", "john@example.com");
            var customer2 = new Customer("Anna", "Smith", "anna@example.com");

            Assert.That(Customer.Extent.Count, Is.EqualTo(2));
            Assert.That(Customer.Extent, Does.Contain(customer1));
            Assert.That(Customer.Extent, Does.Contain(customer2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var customer = new Customer("John", "Doe", "john@example.com");

            var fromExtent = Customer.Extent.Single(c => c.Surname == "Doe");

            customer.ContactInfo = "john.new@example.com";

            Assert.That(fromExtent.ContactInfo, Is.EqualTo("john.new@example.com"));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var customer = new Customer("John", "Doe", "john@example.com");

            Customer.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var customer1 = new Customer("John", "Doe", "john@example.com");
            var customer2 = new Customer("Anna", "Smith", "anna@example.com");

            Customer.Save();

            Customer.Extent = new List<Customer>();
            Customer.Load();

            Assert.That(Customer.Extent.Count, Is.EqualTo(2));

            var john = Customer.Extent.Single(c => c.ContactInfo == "john@example.com");
            Assert.That(john.Name, Is.EqualTo("John"));
            Assert.That(john.Surname, Is.EqualTo("Doe"));

            var anna = Customer.Extent.Single(c => c.ContactInfo == "anna@example.com");
            Assert.That(anna.Name, Is.EqualTo("Anna"));
            Assert.That(anna.Surname, Is.EqualTo("Smith"));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var customer = new Customer("John", "Doe", "john@example.com");

            Assert.That(Customer.Extent, Is.Not.Empty);

            Customer.Load();

            Assert.That(Customer.Extent, Is.Empty);
        }
    }
}
