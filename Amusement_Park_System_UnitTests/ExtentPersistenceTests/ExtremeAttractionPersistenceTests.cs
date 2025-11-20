using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Amusement_Park_System;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class ExtremeAttractionPersistenceTests
    {
        private string _filePath = ExtremeAttraction.FilePath;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            ExtremeAttraction.Extent = new List<ExtremeAttraction>();
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            ExtremeAttraction.Extent = new List<ExtremeAttraction>();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var attr1 = new ExtremeAttraction("Xtreme Drop", 150, 24, true, new List<string> { "No heart conditions" });
            var attr2 = new ExtremeAttraction("Sky Screamer", 160, 20, false, new List<string> { "No pregnancy" });

            Assert.That(ExtremeAttraction.Extent.Count, Is.EqualTo(2));
            Assert.That(ExtremeAttraction.Extent, Does.Contain(attr1));
            Assert.That(ExtremeAttraction.Extent, Does.Contain(attr2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var attr = new ExtremeAttraction("Xtreme Drop", 150, 24, true, new List<string> { "No heart conditions" });

            var fromExtent = ExtremeAttraction.Extent.Single(a => a.Name == "Xtreme Drop");

            attr.SafetyRestrictions = new List<string> { "No heart conditions", "No back problems" };

            Assert.That(fromExtent.SafetyRestrictions, Is.Not.Null);
            Assert.That(fromExtent.SafetyRestrictions!.SequenceEqual(new List<string> { "No heart conditions", "No back problems" }));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var attr = new ExtremeAttraction("Xtreme Drop", 150, 24, true, new List<string> { "No heart conditions" });

            ExtremeAttraction.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var attr1 = new ExtremeAttraction("Xtreme Drop", 150, 24, true, new List<string> { "No heart conditions" });
            var attr2 = new ExtremeAttraction("Sky Screamer", 160, 20, false, new List<string> { "No pregnancy", "No heart conditions" });

            ExtremeAttraction.Save();

            ExtremeAttraction.Extent = new List<ExtremeAttraction>();
            ExtremeAttraction.Load();

            Assert.That(ExtremeAttraction.Extent.Count, Is.EqualTo(2));

            var xtreme = ExtremeAttraction.Extent.Single(a => a.Name == "Xtreme Drop");
            Assert.That(xtreme.Height, Is.EqualTo(150));
            Assert.That(xtreme.MaxSeats, Is.EqualTo(24));
            Assert.That(xtreme.VipPassWorks, Is.True);
            Assert.That(xtreme.SafetyRestrictions, Is.Not.Null);
            Assert.That(xtreme.SafetyRestrictions!.SequenceEqual(new List<string> { "No heart conditions" }));

            var sky = ExtremeAttraction.Extent.Single(a => a.Name == "Sky Screamer");
            Assert.That(sky.Height, Is.EqualTo(160));
            Assert.That(sky.MaxSeats, Is.EqualTo(20));
            Assert.That(sky.VipPassWorks, Is.False);
            Assert.That(sky.SafetyRestrictions, Is.Not.Null);
            Assert.That(sky.SafetyRestrictions!.SequenceEqual(new List<string> { "No pregnancy", "No heart conditions" }));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var attr = new ExtremeAttraction("Xtreme Drop", 150, 24, true, new List<string> { "No heart conditions" });

            Assert.That(ExtremeAttraction.Extent, Is.Not.Empty);

            ExtremeAttraction.Load();

            Assert.That(ExtremeAttraction.Extent, Is.Empty);
        }
    }
}
