using Amusement_Park_System.Models;
using System;

namespace Amusement_Park_System_Tests
{
    public class MalfunctionReportTests
    {
        private readonly MalfunctionReport malfunctionReport = new MalfunctionReport(
            "Electrical", "Ride stopped working during operation", new DateTime(2024, 1, 15));

        [Test]
        public void TestMalfunctionReportType()
        {
            Assert.That(malfunctionReport.Type, Is.EqualTo("Electrical"));
        }

        [Test]
        public void TestMalfunctionReportDescription()
        {
            Assert.That(malfunctionReport.Description, Is.EqualTo("Ride stopped working during operation"));
        }

        [Test]
        public void TestMalfunctionReportDate()
        {
            Assert.That(malfunctionReport.Date, Is.EqualTo(new DateTime(2024, 1, 15)));
        }

        [Test]
        public void TestMalfunctionReportEmptyTypeException()
        {
            Assert.Throws<ArgumentException>(() => new MalfunctionReport(
                "", "Description", new DateTime(2024, 1, 15)));
        }

        [Test]
        public void TestMalfunctionReportNullTypeException()
        {
            Assert.Throws<ArgumentException>(() => new MalfunctionReport(
                null, "Description", new DateTime(2024, 1, 15)));
        }

        [Test]
        public void TestMalfunctionReportEmptyDescriptionException()
        {
            Assert.Throws<ArgumentException>(() => new MalfunctionReport(
                "Type", "", new DateTime(2024, 1, 15)));
        }

        [Test]
        public void TestMalfunctionReportNullDescriptionException()
        {
            Assert.Throws<ArgumentException>(() => new MalfunctionReport(
                "Type", null, new DateTime(2024, 1, 15)));
        }

        [Test]
        public void TestMalfunctionReportFutureDateException()
        {
            Assert.Throws<ArgumentException>(() => new MalfunctionReport(
                "Type", "Description", DateTime.Now.AddDays(1)));
        }

        [Test]
        public void TestMalfunctionReportTypeSetterEmptyException()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15));
            Assert.Throws<ArgumentException>(() => report.Type = "");
        }

        [Test]
        public void TestMalfunctionReportTypeSetterNullException()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15));
            Assert.Throws<ArgumentException>(() => report.Type = null);
        }

        [Test]
        public void TestMalfunctionReportDescriptionSetterEmptyException()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15));
            Assert.Throws<ArgumentException>(() => report.Description = "");
        }

        [Test]
        public void TestMalfunctionReportDescriptionSetterNullException()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15));
            Assert.Throws<ArgumentException>(() => report.Description = null);
        }

        [Test]
        public void TestMalfunctionReportDateSetterFutureException()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15));
            Assert.Throws<ArgumentException>(() => report.Date = DateTime.Now.AddDays(1));
        }

        [Test]
        public void TestMalfunctionReportTypeSetter()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15));
            report.Type = "Mechanical";
            Assert.That(report.Type, Is.EqualTo("Mechanical"));
        }

        [Test]
        public void TestMalfunctionReportDescriptionSetter()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15));
            report.Description = "New description";
            Assert.That(report.Description, Is.EqualTo("New description"));
        }

        [Test]
        public void TestMalfunctionReportDateSetter()
        {
            var report = new MalfunctionReport("Type", "Description", new DateTime(2024, 1, 15));
            report.Date = new DateTime(2024, 2, 1);
            Assert.That(report.Date, Is.EqualTo(new DateTime(2024, 2, 1)));
        }
    }
}