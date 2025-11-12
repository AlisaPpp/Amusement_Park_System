using Amusement_Park_System;
using System;

namespace Amusement_Park_System_Tests
{
    public class ShopTests
    {
        private Shop shop = new Shop("Pirate Shop", ShopType.Merchandise, "Near Roller Coaster");

        [Test]
        public void TestShopName()
        {
            Assert.That(shop.Name, Is.EqualTo("Pirate Shop"));
        }

        [Test]
        public void TestShopType()
        {
            Assert.That(shop.Type, Is.EqualTo(ShopType.Merchandise));
        }

        [Test]
        public void TestShopLocation()
        {
            Assert.That(shop.Location, Is.EqualTo("Near Roller Coaster"));
        }

        [Test]
        public void TestShopEmptyNameException()
        {
            Assert.Throws<ArgumentException>(() => new Shop("", ShopType.Food, "Location"));
        }

        [Test]
        public void TestShopNullNameException()
        {
            Assert.Throws<ArgumentException>(() => new Shop(null, ShopType.Food, "Location"));
        }

        [Test]
        public void TestShopEmptyLocationException()
        {
            Assert.Throws<ArgumentException>(() => new Shop("Shop Name", ShopType.Food, ""));
        }

        [Test]
        public void TestShopNullLocationException()
        {
            Assert.Throws<ArgumentException>(() => new Shop("Shop Name", ShopType.Food, null));
        }

        [Test]
        public void TestShopNameSetterEmptyException()
        {
            var testShop = new Shop("Test Shop", ShopType.Food, "Location");
            Assert.Throws<ArgumentException>(() => testShop.Name = "");
        }

        [Test]
        public void TestShopNameSetterNullException()
        {
            var testShop = new Shop("Test Shop", ShopType.Food, "Location");
            Assert.Throws<ArgumentException>(() => testShop.Name = null);
        }

        [Test]
        public void TestShopLocationSetterEmptyException()
        {
            var testShop = new Shop("Test Shop", ShopType.Food, "Location");
            Assert.Throws<ArgumentException>(() => testShop.Location = "");
        }

        [Test]
        public void TestShopLocationSetterNullException()
        {
            var testShop = new Shop("Test Shop", ShopType.Food, "Location");
            Assert.Throws<ArgumentException>(() => testShop.Location = null);
        }

        [Test]
        public void TestShopTypeSetter()
        {
            var testShop = new Shop("Test Shop", ShopType.Food, "Location");
            testShop.Type = ShopType.Beverage;
            Assert.That(testShop.Type, Is.EqualTo(ShopType.Beverage));
        }
    }
}