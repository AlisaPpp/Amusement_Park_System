using Amusement_Park_System.Models;
using System;

namespace Amusement_Park_System_Tests
{
    public class OrderTests
    {
        private readonly Order order = new Order(1, "Credit Card", 150.0m);

        // Id Tests
        [Test]
        public void TestOrderId()
        {
            Assert.That(order.Id, Is.EqualTo(1));
        }

        [Test]
        public void TestOrderIdSetter()
        {
            var testOrder = new Order(1, "Credit Card", 150.0m);
            testOrder.Id = 2;
            Assert.That(testOrder.Id, Is.EqualTo(2));
        }

        // PaymentMethod Tests
        [Test]
        public void TestOrderPaymentMethod()
        {
            Assert.That(order.PaymentMethod, Is.EqualTo("Credit Card"));
        }

        [Test]
        public void TestOrderEmptyPaymentMethodException()
        {
            Assert.Throws<ArgumentException>(() => new Order(1, "", 150.0m));
        }

        [Test]
        public void TestOrderNullPaymentMethodException()
        {
            Assert.Throws<ArgumentException>(() => new Order(1, null, 150.0m));
        }

        [Test]
        public void TestOrderPaymentMethodSetterEmptyException()
        {
            var testOrder = new Order(1, "Credit Card", 150.0m);
            Assert.Throws<ArgumentException>(() => testOrder.PaymentMethod = "");
        }

        [Test]
        public void TestOrderPaymentMethodSetterNullException()
        {
            var testOrder = new Order(1, "Credit Card", 150.0m);
            Assert.Throws<ArgumentException>(() => testOrder.PaymentMethod = null);
        }

        [Test]
        public void TestOrderPaymentMethodSetter()
        {
            var testOrder = new Order(1, "Credit Card", 150.0m);
            testOrder.PaymentMethod = "PayPal";
            Assert.That(testOrder.PaymentMethod, Is.EqualTo("PayPal"));
        }

        // TotalPrice Tests
        [Test]
        public void TestOrderTotalPrice()
        {
            Assert.That(order.TotalPrice, Is.EqualTo(150.0m));
        }

        [Test]
        public void TestOrderNegativeTotalPriceException()
        {
            Assert.Throws<ArgumentException>(() => new Order(1, "Credit Card", -50.0m));
        }

        [Test]
        public void TestOrderTotalPriceSetterNegativeException()
        {
            var testOrder = new Order(1, "Credit Card", 150.0m);
            Assert.Throws<ArgumentException>(() => testOrder.TotalPrice = -50.0m);
        }

        [Test]
        public void TestOrderTotalPriceSetter()
        {
            var testOrder = new Order(1, "Credit Card", 150.0m);
            testOrder.TotalPrice = 200.0m;
            Assert.That(testOrder.TotalPrice, Is.EqualTo(200.0m));
        }
    }
}