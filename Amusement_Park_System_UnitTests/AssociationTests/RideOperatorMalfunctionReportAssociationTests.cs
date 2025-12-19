using System;
using System.Collections.Generic;
using Amusement_Park_System;
using Amusement_Park_System.Models;
using NUnit.Framework;

namespace Amusement_Park_System_Tests
{
    public class RideOperatorMalfunctionReportAssociationTests
    {
        private RideOperator rideOperator = null!;
        private Attraction rollerCoasterAttraction = null!;
        private Attraction waterRideAttraction = null!;
        private DateTime testDate;

        [SetUp]
        public void Setup()
        {
            RideOperator.ClearExtent();
            MalfunctionReport.ClearExtent();
            Attraction.ClearExtent();

            testDate = new DateTime(2025, 12, 12);

            rideOperator = new RideOperator(
                "John",
                "Doe",
                "john@example.com",
                new DateTime(1990, 1, 1),
                5,
                "OP12345",
                true);

            rollerCoasterAttraction = new Attraction(
                "Thunderbolt",
                140,
                24,
                true,
                null,
                new ExtremeAttraction(null),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) });

            waterRideAttraction = new Attraction(
                "Splash Mountain",
                120,
                20,
                true,
                null,
                new MediumAttraction(true),
                new List<IAttractionType> { new WaterRide(2.5, 25.0) });
        }

        [Test]
        public void TestRideOperatorCanMakeReport()
        {
            var report = new MalfunctionReport("Mechanical", "Chain issue", testDate, rollerCoasterAttraction);

            rideOperator.MakeReport(report);

            Assert.That(report.Operator, Is.EqualTo(rideOperator));
            Assert.That(rideOperator.ReportsMade, Contains.Item(report));
        }

        [Test]
        public void TestRideOperatorCannotAddSameReportTwice()
        {
            var report = new MalfunctionReport("Electrical", "Panel issue", testDate, waterRideAttraction);

            rideOperator.MakeReport(report);
            rideOperator.MakeReport(report);

            Assert.That(rideOperator.ReportsMade.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestRemovingReportDisassociatesOperator()
        {
            var report = new MalfunctionReport("Mechanical", "Brake issue", testDate, rollerCoasterAttraction);

            rideOperator.MakeReport(report);
            rideOperator.RemoveReport(report);

            Assert.That(report.Operator, Is.Null);
            Assert.That(rideOperator.ReportsMade, Does.Not.Contain(report));
        }

        [Test]
        public void TestRemovingNonExistentReportDoesNothing()
        {
            var report = new MalfunctionReport("Electrical", "Fuse issue", testDate, waterRideAttraction);

            rideOperator.RemoveReport(report);

            Assert.That(report.Operator, Is.Null);
            Assert.That(rideOperator.ReportsMade.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestMakeReportNullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => rideOperator.MakeReport(null!));
        }

        [Test]
        public void TestRemoveReportNullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => rideOperator.RemoveReport(null!));
        }

        [Test]
        public void TestMultipleReportsMadeByOperator()
        {
            var report1 = new MalfunctionReport("Mechanical", "Gear issue", testDate, rollerCoasterAttraction);
            var report2 = new MalfunctionReport("Electrical", "Panel flicker", testDate, waterRideAttraction);

            rideOperator.MakeReport(report1);
            rideOperator.MakeReport(report2);

            Assert.That(rideOperator.ReportsMade.Count, Is.EqualTo(2));
            Assert.That(report1.Operator, Is.EqualTo(rideOperator));
            Assert.That(report2.Operator, Is.EqualTo(rideOperator));
        }

        [Test]
        public void TestReportsMadeCollectionIsReadOnly()
        {
            var report = new MalfunctionReport("Mechanical", "Brake failure", testDate, rollerCoasterAttraction);
            rideOperator.MakeReport(report);

            Assert.That(rideOperator.ReportsMade, Is.InstanceOf<IReadOnlyCollection<MalfunctionReport>>());
        }
    }
}
