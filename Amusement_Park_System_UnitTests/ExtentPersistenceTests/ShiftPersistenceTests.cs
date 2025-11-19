using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class ShiftPersistenceTests
    {
        private string _filePath = Shift.FilePath;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Shift.Extent = new List<Shift>();
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Shift.Extent = new List<Shift>();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var shift1 = new Shift(
                new DateTime(2026, 5, 1),
                new DateTime(2026, 5, 1, 9, 0, 0),
                new DateTime(2026, 5, 1, 17, 0, 0));

            var shift2 = new Shift(
                new DateTime(2026, 5, 2),
                new DateTime(2026, 5, 2, 10, 0, 0),
                new DateTime(2026, 5, 2, 18, 0, 0));

            Assert.That(Shift.Extent.Count, Is.EqualTo(2));
            Assert.That(Shift.Extent, Does.Contain(shift1));
            Assert.That(Shift.Extent, Does.Contain(shift2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var shift = new Shift(
                new DateTime(2026, 5, 1),
                new DateTime(2026, 5, 1, 9, 0, 0),
                new DateTime(2026, 5, 1, 17, 0, 0));

            var fromExtent = Shift.Extent.Single(s => s.Date == new DateTime(2026, 5, 1));

            shift.EndTime = new DateTime(2026, 5, 1, 18, 0, 0);

            Assert.That(fromExtent.EndTime, Is.EqualTo(new DateTime(2026, 5, 1, 18, 0, 0)));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var shift = new Shift(
                new DateTime(2026, 5, 1),
                new DateTime(2026, 5, 1, 9, 0, 0),
                new DateTime(2026, 5, 1, 17, 0, 0));

            Shift.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var shift1 = new Shift(
                new DateTime(2026, 5, 1),
                new DateTime(2026, 5, 1, 9, 0, 0),
                new DateTime(2026, 5, 1, 17, 0, 0));

            var shift2 = new Shift(
                new DateTime(2026, 5, 2),
                new DateTime(2026, 5, 2, 10, 0, 0),
                new DateTime(2026, 5, 2, 18, 0, 0));

            Shift.Save();

            Shift.Extent = new List<Shift>();
            Shift.Load();

            Assert.That(Shift.Extent.Count, Is.EqualTo(2));

            var first = Shift.Extent.Single(s => s.Date == new DateTime(2026, 5, 1));
            Assert.That(first.StartTime, Is.EqualTo(new DateTime(2026, 5, 1, 9, 0, 0)));
            Assert.That(first.EndTime, Is.EqualTo(new DateTime(2026, 5, 1, 17, 0, 0)));

            var second = Shift.Extent.Single(s => s.Date == new DateTime(2026, 5, 2));
            Assert.That(second.StartTime, Is.EqualTo(new DateTime(2026, 5, 2, 10, 0, 0)));
            Assert.That(second.EndTime, Is.EqualTo(new DateTime(2026, 5, 2, 18, 0, 0)));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var shift = new Shift(
                new DateTime(2026, 5, 1),
                new DateTime(2026, 5, 1, 9, 0, 0),
                new DateTime(2026, 5, 1, 17, 0, 0));

            Assert.That(Shift.Extent, Is.Not.Empty);

            Shift.Load();

            Assert.That(Shift.Extent, Is.Empty);
        }
    }
}
