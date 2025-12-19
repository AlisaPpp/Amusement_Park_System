using System;
using System.Collections.Generic;
using Amusement_Park_System;
using Amusement_Park_System.Models;
using NUnit.Framework;

namespace Amusement_Park_System_Tests
{
    public class MalfunctionReportTests
    {
        private MalfunctionReport malfunctionReport = null!;
        private Attraction attraction = null!;

        [SetUp]
        public void Setup()
        {
            MalfunctionReport.ClearExtent();
            Attraction.ClearExtent();

            
            var rollerCoasterType = new RollerCoaster(
                trackLength: 1200.5,
                maxSpeed: 85.5,
                numberOfLoops: 3);

           
            var intensity = new MediumAttraction(familyFriendly: true);

           
            attraction = new Attraction(
                name: "Thunderbolt",
                height: 140,
                maxSeats: 24,
                vipPassWorks: true,
                zone: null,
                intensity: intensity,
                types: new List<IAttractionType> { rollerCoasterType });

            malfunctionReport = new MalfunctionReport(
                "Electrical",
                "Ride stopped working during operation",
                new DateTime(2024, 1, 15),
                attraction);
        }

        
        [Test]
        public void TestMalfunctionReportType()
        {
            Assert.That(malfunctionReport.Type, Is.EqualTo("Electrical"));
        }

        [Test]
        public void TestMalfunctionReportEmptyTypeException()
        {
            Assert.Throws<ArgumentException>(() => new MalfunctionReport(
                "", "Description", new DateTime(2024, 1, 15), attraction));
        }

        [Test]
        public void TestMalfunctionReportNullTypeException()
        {
            Assert.Throws<ArgumentException>(() => new MalfunctionReport(
                null!, "Description", new DateTime(2024, 1, 15), attraction));
        }

        [Test]
        public void TestMalfunctionReportTypeSetterEmptyException()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15), attraction);
            Assert.Throws<ArgumentException>(() => report.Type = "");
        }

        [Test]
        public void TestMalfunctionReportTypeSetterNullException()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15), attraction);
            Assert.Throws<ArgumentException>(() => report.Type = null!);
        }

        [Test]
        public void TestMalfunctionReportTypeSetter()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15), attraction);
            report.Type = "Mechanical";
            Assert.That(report.Type, Is.EqualTo("Mechanical"));
        }

       
        [Test]
        public void TestMalfunctionReportDescription()
        {
            Assert.That(malfunctionReport.Description, Is.EqualTo("Ride stopped working during operation"));
        }

        [Test]
        public void TestMalfunctionReportEmptyDescriptionException()
        {
            Assert.Throws<ArgumentException>(() => new MalfunctionReport(
                "Type", "", new DateTime(2024, 1, 15), attraction));
        }

        [Test]
        public void TestMalfunctionReportNullDescriptionException()
        {
            Assert.Throws<ArgumentException>(() => new MalfunctionReport(
                "Type", null!, new DateTime(2024, 1, 15), attraction));
        }

        [Test]
        public void TestMalfunctionReportDescriptionSetterEmptyException()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15), attraction);
            Assert.Throws<ArgumentException>(() => report.Description = "");
        }

        [Test]
        public void TestMalfunctionReportDescriptionSetterNullException()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15), attraction);
            Assert.Throws<ArgumentException>(() => report.Description = null!);
        }

        [Test]
        public void TestMalfunctionReportDescriptionSetter()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15), attraction);
            report.Description = "New description";
            Assert.That(report.Description, Is.EqualTo("New description"));
        }

      
        [Test]
        public void TestMalfunctionReportDate()
        {
            Assert.That(malfunctionReport.Date, Is.EqualTo(new DateTime(2024, 1, 15)));
        }

        [Test]
        public void TestMalfunctionReportFutureDateException()
        {
            Assert.Throws<ArgumentException>(() => new MalfunctionReport(
                "Type", "Description", DateTime.Now.AddDays(1), attraction));
        }

        [Test]
        public void TestMalfunctionReportDateSetterFutureException()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15), attraction);
            Assert.Throws<ArgumentException>(() => report.Date = DateTime.Now.AddDays(1));
        }

        [Test]
        public void TestMalfunctionReportDateSetter()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15), attraction);
            report.Date = new DateTime(2024, 2, 1);
            Assert.That(report.Date, Is.EqualTo(new DateTime(2024, 2, 1)));
        }

       
        [Test]
        public void TestMalfunctionReportIsAddedToAttractionReports()
        {
            Assert.That(attraction.Reports, Does.Contain(malfunctionReport));
        }
    }
}
