using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Amusement_Park_System;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class LightAttractionPersistenceTests
    {
        private string _filePath = LightAttraction.FilePath;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            LightAttraction.Extent = new List<LightAttraction>();
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            LightAttraction.Extent = new List<LightAttraction>();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var attr1 = new LightAttraction("Mini Carousel", 105, 12, true, false);
            var attr2 = new LightAttraction("Tiny Train", 102, 20, false, true);

            Assert.That(LightAttraction.Extent.Count, Is.EqualTo(2));
            Assert.That(LightAttraction.Extent, Does.Contain(attr1));
            Assert.That(LightAttraction.Extent, Does.Contain(attr2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var attr = new LightAttraction("Mini Carousel", 105, 12, true, false);

            var fromExtent = LightAttraction.Extent.Single(a => a.Name == "Mini Carousel");

            attr.IsParentSupervisionRequired = true;

            Assert.That(fromExtent.IsParentSupervisionRequired, Is.EqualTo(true));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var attr = new LightAttraction("Mini Carousel", 105, 12, true, false);

            LightAttraction.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var attr1 = new LightAttraction("Mini Carousel", 105, 12, true, false);
            var attr2 = new LightAttraction("Tiny Train", 102, 20, false, true);

            LightAttraction.Save();

            LightAttraction.Extent = new List<LightAttraction>();
            LightAttraction.Load();

            Assert.That(LightAttraction.Extent.Count, Is.EqualTo(2));

            var mini = LightAttraction.Extent.Single(a => a.Name == "Mini Carousel");
            Assert.That(mini.Height, Is.EqualTo(105));
            Assert.That(mini.MaxSeats, Is.EqualTo(12));
            Assert.That(mini.VipPassWorks, Is.True);
            Assert.That(mini.IsParentSupervisionRequired, Is.False);

            var train = LightAttraction.Extent.Single(a => a.Name == "Tiny Train");
            Assert.That(train.Height, Is.EqualTo(102));
            Assert.That(train.MaxSeats, Is.EqualTo(20));
            Assert.That(train.VipPassWorks, Is.False);
            Assert.That(train.IsParentSupervisionRequired, Is.True);
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var attr = new LightAttraction("Mini Carousel", 105, 12, true, false);

            Assert.That(LightAttraction.Extent, Is.Not.Empty);

            LightAttraction.Load();

            Assert.That(LightAttraction.Extent, Is.Empty);
        }
    }
}
