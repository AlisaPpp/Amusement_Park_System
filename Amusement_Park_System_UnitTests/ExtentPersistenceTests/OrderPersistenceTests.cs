using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class OrderPersistenceTests
    {
        private string _filePath = Order.FilePath;

        // helper to access private static Extent field via reflection
        private static List<Order> GetExtent()
        {
            var field = typeof(Order).GetField("Extent",
                BindingFlags.NonPublic | BindingFlags.Static);
            return (List<Order>)field!.GetValue(null)!;
        }

        private static void SetExtent(List<Order> newExtent)
        {
            var field = typeof(Order).GetField("Extent",
                BindingFlags.NonPublic | BindingFlags.Static);
            field!.SetValue(null, newExtent);
        }

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            SetExtent(new List<Order>());
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            SetExtent(new List<Order>());
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var order1 = new Order(1, "Card", 50.0m);
            var order2 = new Order(2, "Cash", 30.5m);

            var extent = GetExtent();

            Assert.That(extent.Count, Is.EqualTo(2));
            Assert.That(extent, Does.Contain(order1));
            Assert.That(extent, Does.Contain(order2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var order = new Order(1, "Card", 50.0m);

            var extent = GetExtent();
            var fromExtent = extent.Single(o => o.Id == 1);

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

            SetExtent(new List<Order>());
            Order.Load();

            var extent = GetExtent();

            Assert.That(extent.Count, Is.EqualTo(2));

            var first = extent.Single(o => o.Id == 1);
            Assert.That(first.PaymentMethod, Is.EqualTo("Card"));
            Assert.That(first.TotalPrice, Is.EqualTo(50.0m));

            var second = extent.Single(o => o.Id == 2);
            Assert.That(second.PaymentMethod, Is.EqualTo("Cash"));
            Assert.That(second.TotalPrice, Is.EqualTo(30.5m));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var order = new Order(1, "Card", 50.0m);

            Assert.That(GetExtent(), Is.Not.Empty);

            Order.Load();

            Assert.That(GetExtent(), Is.Empty);
        }
    }
}
