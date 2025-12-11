using Amusement_Park_System.Models;
using NUnit.Framework;

namespace Amusement_Park_System_Tests
{
    public class CustomerOrderAssociationTests
    {
        private Customer customer;

        [SetUp]
        public void Setup()
        {
            Customer.ClearExtent();
            Order.ClearExtent();

            customer = new Customer("John", "Doe", "john@example.com");
        }

        [Test]
        public void TestCustomerCreateOrderCreatesAssociation()
        {
            var order = customer.CreateOrder(1, "Card");

            Assert.That(customer.Orders, Contains.Item(order));
            Assert.That(order.Customer, Is.EqualTo(customer));
            Assert.That(Order.Extent, Contains.Item(order));
        }

        [Test]
        public void TestOrderConstructorCreatesAssociation()
        {
            var order = new Order(2, "Cash", customer);

            Assert.That(customer.Orders, Contains.Item(order));
            Assert.That(order.Customer, Is.EqualTo(customer));
        }

        [Test]
        public void TestCustomerOrdersCollectionUpdatesWhenAddingOrder()
        {
            var order = customer.CreateOrder(1, "Card");

            Assert.That(customer.Orders.Count, Is.EqualTo(1));
            Assert.That(customer.Orders, Contains.Item(order));
        }

        [Test]
        public void TestOrderAndCustomer()
        {
            var order = customer.CreateOrder(1, "Cash");

            Assert.That(order.Customer, Is.EqualTo(customer));
        }

        [Test]
        public void TestCustomerCreatesTwoOrders()
        {
            var order1 = new Order(3, "Card", customer);
            var order2 = new Order(4, "Cash", customer);

            Assert.That(customer.Orders.Count, Is.EqualTo(2));
            Assert.That(customer.Orders, Contains.Item(order1));
            Assert.That(customer.Orders, Contains.Item(order2));
        }


        [Test]
        public void TestOrderConstructorNullCustomer()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Order(5, "Card", null));
        }

        [Test]
        public void TestPaymentMethodCannotBeEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                new Order(10, "", customer));
        }

        [Test]
        public void TestCustomerCanHaveMultipleOrders()
        {
            var o1 = customer.CreateOrder(1, "Card");
            var o2 = customer.CreateOrder(2, "Cash");
            var o3 = customer.CreateOrder(3, "Blik");

            Assert.That(customer.Orders.Count, Is.EqualTo(3));
            Assert.That(customer.Orders, Contains.Item(o1));
            Assert.That(customer.Orders, Contains.Item(o2));
            Assert.That(customer.Orders, Contains.Item(o3));
        }

        [Test]
        public void TestCustomerDeleteOrderRemovesAssociations()
        {
            var order = customer.CreateOrder(1, "Card");

            customer.DeleteOrder(order);

            Assert.That(customer.Orders, Does.Not.Contain(order));
            Assert.That(Order.Extent, Does.Not.Contain(order));
            Assert.That(order.Customer, Is.Null);
        }



        [Test]
        public void TestDeletingOrderNotInCustomerThrowsException()
        {
            var otherCustomer = new Customer("Alice", "Smith", "alice@mail.com");
            var order = otherCustomer.CreateOrder(99, "Card");

            Assert.Throws<InvalidOperationException>(() =>
                customer.DeleteOrder(order));
        }

        [Test]
        public void TestCustomerOrdersCollectionIsReadOnly()
        {
            var order = customer.CreateOrder(1, "Card");

            var orders = customer.Orders;

            Assert.That(orders, Is.InstanceOf<IReadOnlyCollection<Order>>());
        }

        [Test]
        public void TestDeleteOrderClearsReverseCustomerConnection()
        {
            var order = customer.CreateOrder(7, "Card");

            customer.DeleteOrder(order); // instead of Order.Delete(order)

            Assert.That(order.Customer, Is.Null);
            Assert.That(customer.Orders, Does.Not.Contain(order));
        }

        [Test]
        public void TestOrderExtentUpdatesCorrectly()
        {
            var o1 = customer.CreateOrder(1, "Card");
            var o2 = customer.CreateOrder(2, "Cash");

            Assert.That(Order.Extent.Count, Is.EqualTo(2));

            customer.DeleteOrder(o1); 

            Assert.That(Order.Extent.Count, Is.EqualTo(1));
            Assert.That(Order.Extent, Does.Not.Contain(o1));
            Assert.That(Order.Extent, Contains.Item(o2));
        }
    }
}
