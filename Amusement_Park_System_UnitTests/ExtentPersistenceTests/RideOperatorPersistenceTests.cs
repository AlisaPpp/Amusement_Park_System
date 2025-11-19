using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class RideOperatorPersistenceTests
    {
        private string _filePath = RideOperator.FilePath;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            RideOperator.Extent = new List<RideOperator>();
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            RideOperator.Extent = new List<RideOperator>();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var op1 = new RideOperator(
                "John",
                "Doe",
                "john@example.com",
                new DateTime(1990, 1, 1),
                5,
                "LIC-1234",
                true);

            var op2 = new RideOperator(
                "Anna",
                "Smith",
                "anna@example.com",
                new DateTime(1988, 4, 12),
                7,
                "LIC-5678",
                false);

            Assert.That(RideOperator.Extent.Count, Is.EqualTo(2));
            Assert.That(RideOperator.Extent, Does.Contain(op1));
            Assert.That(RideOperator.Extent, Does.Contain(op2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var op = new RideOperator(
                "John",
                "Doe",
                "john@example.com",
                new DateTime(1990, 1, 1),
                5,
                "LIC-1234",
                true);

            var fromExtent = RideOperator.Extent.Single(o => o.OperatorLicenceId == "LIC-1234");

            op.OperatorLicenceId = "LIC-9999";

            Assert.That(fromExtent.OperatorLicenceId, Is.EqualTo("LIC-9999"));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var op = new RideOperator(
                "John",
                "Doe",
                "john@example.com",
                new DateTime(1990, 1, 1),
                5,
                "LIC-1234",
                true);

            RideOperator.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var op1 = new RideOperator(
                "John",
                "Doe",
                "john@example.com",
                new DateTime(1990, 1, 1),
                5,
                "LIC-1234",
                true);

            var op2 = new RideOperator(
                "Anna",
                "Smith",
                "anna@example.com",
                new DateTime(1988, 4, 12),
                7,
                "LIC-5678",
                false);

            RideOperator.Save();

            RideOperator.Extent = new List<RideOperator>();
            RideOperator.Load();

            Assert.That(RideOperator.Extent.Count, Is.EqualTo(2));

            var john = RideOperator.Extent.Single(o => o.OperatorLicenceId == "LIC-1234");
            Assert.That(john.Name, Is.EqualTo("John"));
            Assert.That(john.Surname, Is.EqualTo("Doe"));
            Assert.That(john.ContactInfo, Is.EqualTo("john@example.com"));
            Assert.That(john.IsFirstAidCertified, Is.True);

            var anna = RideOperator.Extent.Single(o => o.OperatorLicenceId == "LIC-5678");
            Assert.That(anna.Name, Is.EqualTo("Anna"));
            Assert.That(anna.Surname, Is.EqualTo("Smith"));
            Assert.That(anna.ContactInfo, Is.EqualTo("anna@example.com"));
            Assert.That(anna.IsFirstAidCertified, Is.False);
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var op = new RideOperator(
                "John",
                "Doe",
                "john@example.com",
                new DateTime(1990, 1, 1),
                5,
                "LIC-1234",
                true);

            Assert.That(RideOperator.Extent, Is.Not.Empty);

            RideOperator.Load();

            Assert.That(RideOperator.Extent, Is.Empty);
        }
    }
}
