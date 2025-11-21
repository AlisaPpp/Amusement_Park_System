using Amusement_Park_System.Models;

namespace Amusement_Park_System_UnitTests.ExtentPersistenceTests
{
    [TestFixture]
    public class OrderPersistenceTests
    {
        private string _filePath = Order.FilePath;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Order.Extent = new List<Order>();
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Order.Extent = new List<Order>();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var order1 = new Order(1, "Card", 50.0m);
            var order2 = new Order(2, "Cash", 30.5m);

            Assert.That(Order.Extent.Count, Is.EqualTo(2));
            Assert.That(Order.Extent, Does.Contain(order1));
            Assert.That(Order.Extent, Does.Contain(order2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var order = new Order(1, "Card", 50.0m);

            var fromExtent = Order.Extent.Single(o => o.Id == 1);

            order.TotalPrice = 75.0m;

            Assert.That(fromExtent.TotalPrice, Is.EqualTo(75.0m));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var order = new Order(1, "Card", 50.0m);

            Order.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var order1 = new Order(1, "Card", 50.0m);
            var order2 = new Order(2, "Cash", 30.5m);

            Order.Save();

            Order.Extent = new List<Order>();
            Order.Load();

            Assert.That(Order.Extent.Count, Is.EqualTo(2));

            var first = Order.Extent.Single(o => o.Id == 1);
            Assert.That(first.PaymentMethod, Is.EqualTo("Card"));
            Assert.That(first.TotalPrice, Is.EqualTo(50.0m));

            var second = Order.Extent.Single(o => o.Id == 2);
            Assert.That(second.PaymentMethod, Is.EqualTo("Cash"));
            Assert.That(second.TotalPrice, Is.EqualTo(30.5m));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var order = new Order(1, "Card", 50.0m);

            Assert.That(Order.Extent, Is.Not.Empty);

            Order.Load();

            Assert.That(Order.Extent, Is.Empty);
        }
    }
}