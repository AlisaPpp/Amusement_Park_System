using Amusement_Park_System;
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class ShopPersistenceTests
    {
        private string _filePath = Shop.FilePath;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Shop.Extent = new List<Shop>();
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Shop.Extent = new List<Shop>();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var shop1 = new Shop("Pirate Shop", ShopType.Merchandise, "Near Roller Coaster");
            var shop2 = new Shop("Snack Bar", ShopType.Food, "Central Plaza");

            Assert.That(Shop.Extent.Count, Is.EqualTo(2));
            Assert.That(Shop.Extent, Does.Contain(shop1));
            Assert.That(Shop.Extent, Does.Contain(shop2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var shop = new Shop("Pirate Shop", ShopType.Merchandise, "Near Roller Coaster");

            var fromExtent = Shop.Extent.Single(s => s.Name == "Pirate Shop");

            shop.Location = "Near Ferris Wheel";

            Assert.That(fromExtent.Location, Is.EqualTo("Near Ferris Wheel"));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var shop = new Shop("Pirate Shop", ShopType.Merchandise, "Near Roller Coaster");

            Shop.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var shop1 = new Shop("Pirate Shop", ShopType.Merchandise, "Near Roller Coaster");
            var shop2 = new Shop("Snack Bar", ShopType.Food, "Central Plaza");

            Shop.Save();

            Shop.Extent = new List<Shop>();
            Shop.Load();

            Assert.That(Shop.Extent.Count, Is.EqualTo(2));

            var pirate = Shop.Extent.Single(s => s.Name == "Pirate Shop");
            Assert.That(pirate.Type, Is.EqualTo(ShopType.Merchandise));
            Assert.That(pirate.Location, Is.EqualTo("Near Roller Coaster"));

            var snack = Shop.Extent.Single(s => s.Name == "Snack Bar");
            Assert.That(snack.Type, Is.EqualTo(ShopType.Food));
            Assert.That(snack.Location, Is.EqualTo("Central Plaza"));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var shop = new Shop("Pirate Shop", ShopType.Merchandise, "Near Roller Coaster");

            Assert.That(Shop.Extent, Is.Not.Empty);

            Shop.Load();

            Assert.That(Shop.Extent, Is.Empty);
        }
    }
}
