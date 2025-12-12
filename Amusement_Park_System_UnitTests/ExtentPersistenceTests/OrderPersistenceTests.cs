using Amusement_Park_System.Models;

namespace Amusement_Park_System_UnitTests.ExtentPersistenceTests
{
    [TestFixture]
    public class OrderPersistenceTests
    {
        private string _filePath = Order.FilePath;
        private Customer customer;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Order.ClearExtent();
            Customer.ClearExtent();
            
            customer = new Customer("Alice", "Smith", "alice@example.com");
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Order.ClearExtent();
            Customer.ClearExtent();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var order1 = new Order(1, "Card", customer);
            var order2 = new Order(2, "Cash", customer);

            Assert.That(Order.Extent.Count, Is.EqualTo(2));
            Assert.That(Order.Extent, Does.Contain(order1));
            Assert.That(Order.Extent, Does.Contain(order2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var order = new Order(1, "Card", customer);

            var fromExtent = Order.Extent.Single(o => o.Id == 1);

            order.PaymentMethod = "Cash";

            Assert.That(fromExtent.PaymentMethod, Is.EqualTo("Cash"));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var order = new Order(1, "Card", customer);

            Order.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var order1 = new Order(1, "Card", customer);
            var order2 = new Order(2, "Cash", customer);

            Order.Save();

            Order.ClearExtent();
            Order.Load();

            Assert.That(Order.Extent.Count, Is.EqualTo(2));

            var first = Order.Extent.Single(o => o.Id == 1);
            Assert.That(first.PaymentMethod, Is.EqualTo("Card"));
            Assert.That(first.TotalPrice, Is.EqualTo(0));

            var second = Order.Extent.Single(o => o.Id == 2);
            Assert.That(second.PaymentMethod, Is.EqualTo("Cash"));
            Assert.That(second.TotalPrice, Is.EqualTo(30.5m));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var order = new Order(1, "Card", customer);

            Assert.That(Order.Extent, Is.Not.Empty);

            Order.Load();

            Assert.That(Order.Extent, Is.Empty);
        }
    }
}