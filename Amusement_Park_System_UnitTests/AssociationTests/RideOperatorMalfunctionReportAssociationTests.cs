
using Amusement_Park_System;
using Amusement_Park_System.Models;


namespace Amusement_Park_System_Tests
{
    public class RideOperatorMalfunctionReportAssociationTests
    {
        private RideOperator rideOperator;
        private RollerCoaster rollerCoaster;
        private WaterRide waterRide;
        private DateTime testDate;

        [SetUp]
        public void Setup()
        {
            RideOperator.ClearExtent();
            MalfunctionReport.ClearExtent();
            RollerCoaster.ClearExtent();
            WaterRide.ClearExtent();

            testDate = new DateTime(2025, 12, 12);

            rideOperator = new RideOperator("John", "Doe", "john@example.com",
                new DateTime(1990, 1, 1), 5, "OP12345", true);

            rollerCoaster = new RollerCoaster("Thunderbolt", 140, 24, true, 1200.5, 85.5, 3);
            waterRide = new WaterRide("Splash Mountain", 120, 20, true, 2.5, 25.0);
        }

        [Test]
        public void TestRideOperatorCanMakeReport()
        {
            var report = new MalfunctionReport("Mechanical", "Chain issue", testDate, rollerCoaster);

            rideOperator.MakeReport(report);

            Assert.That(report.Operator, Is.EqualTo(rideOperator));
            Assert.That(rideOperator.ReportsMade, Contains.Item(report));
        }

        [Test]
        public void TestRideOperatorCannotAddSameReportTwice()
        {
            var report = new MalfunctionReport("Electrical", "Panel issue", testDate, waterRide);

            rideOperator.MakeReport(report);
            rideOperator.MakeReport(report); // second call

            Assert.That(rideOperator.ReportsMade.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestRemovingReportDisassociatesOperator()
        {
            var report = new MalfunctionReport("Mechanical", "Brake issue", testDate, rollerCoaster);

            rideOperator.MakeReport(report);
            rideOperator.RemoveReport(report);

            Assert.That(report.Operator, Is.Null);
            Assert.That(rideOperator.ReportsMade, Does.Not.Contain(report));
        }

        [Test]
        public void TestRemovingNonExistentReportDoesNothing()
        {
            var report = new MalfunctionReport("Electrical", "Fuse issue", testDate, waterRide);

            // Removing without adding first
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
            var report1 = new MalfunctionReport("Mechanical", "Gear issue", testDate, rollerCoaster);
            var report2 = new MalfunctionReport("Electrical", "Panel flicker", testDate, waterRide);

            rideOperator.MakeReport(report1);
            rideOperator.MakeReport(report2);

            Assert.That(rideOperator.ReportsMade.Count, Is.EqualTo(2));
            Assert.That(report1.Operator, Is.EqualTo(rideOperator));
            Assert.That(report2.Operator, Is.EqualTo(rideOperator));
        }

        [Test]
        public void TestReportsMadeCollectionIsReadOnly()
        {
            var report = new MalfunctionReport("Mechanical", "Brake failure", testDate, rollerCoaster);
            rideOperator.MakeReport(report);

            Assert.That(rideOperator.ReportsMade, Is.InstanceOf<IReadOnlyCollection<MalfunctionReport>>());
        }
    }
}
