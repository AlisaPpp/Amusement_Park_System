using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class OrderTests
    {
        private Customer customer;
        private Order order;

        [SetUp]
        public void Setup()
        {
            Customer.ClearExtent();
            Order.ClearExtent();

            customer = new Customer("John", "Doe", "123456");
            order = new Order(1, "Credit Card", customer);
        }

        [Test]
        public void TestOrderId()
        {
            Assert.That(order.Id, Is.EqualTo(1));
        }

        [Test]
        public void TestOrderIdSetter()
        {
            var testOrder = new Order(1, "Credit Card", customer);
            testOrder.Id = 2;
            Assert.That(testOrder.Id, Is.EqualTo(2));
        }

        [Test]
        public void TestOrderPaymentMethod()
        {
            Assert.That(order.PaymentMethod, Is.EqualTo("Credit Card"));
        }

        [Test]
        public void TestOrderEmptyPaymentMethodException()
        {
            Assert.Throws<ArgumentException>(() => new Order(1, "", customer));
        }

        [Test]
        public void TestOrderNullPaymentMethodException()
        {
            Assert.Throws<ArgumentException>(() => new Order(1, null, customer));
        }

        [Test]
        public void TestOrderPaymentMethodSetterEmptyException()
        {
            var testOrder = new Order(1, "Credit Card", customer);
            Assert.Throws<ArgumentException>(() => testOrder.PaymentMethod = "");
        }

        [Test]
        public void TestOrderPaymentMethodSetterNullException()
        {
            var testOrder = new Order(1, "Credit Card", customer);
            Assert.Throws<ArgumentException>(() => testOrder.PaymentMethod = null);
        }

        [Test]
        public void TestOrderPaymentMethodSetter()
        {
            var testOrder = new Order(1, "Credit Card", customer);
            testOrder.PaymentMethod = "PayPal";
            Assert.That(testOrder.PaymentMethod, Is.EqualTo("PayPal"));
        }

        [Test]
        public void TestOrderTotalPrice_InitiallyZero()
        {
            Assert.That(order.TotalPrice, Is.EqualTo(0m));
        }
    }
}