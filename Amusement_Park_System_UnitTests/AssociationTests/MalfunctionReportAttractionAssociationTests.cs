using Amusement_Park_System;
using Amusement_Park_System.Models;


namespace Amusement_Park_System_Tests
{
    public class MalfunctionReportAttractionAssociationTests
    {
        private RollerCoaster rollerCoaster;
        private WaterRide waterRide;
        private FourDRide fourDRide;
        private ExtremeAttraction extremeAttraction;
        private MediumAttraction mediumAttraction;
        private LightAttraction lightAttraction;
        private DateTime testDate;

        [SetUp]
        public void Setup()
        {
            MalfunctionReport.ClearExtent();
            RollerCoaster.ClearExtent();
            WaterRide.ClearExtent();
            FourDRide.ClearExtent();
            ExtremeAttraction.ClearExtent();
            MediumAttraction.ClearExtent();
            LightAttraction.ClearExtent();

            testDate = new DateTime(2025, 12, 12);

            rollerCoaster = new RollerCoaster("Thunderbolt", 140, 24, true, 1200.5, 85.5, 3);
            waterRide = new WaterRide("Splash Mountain", 120, 20, true, 2.5, 25.0);
            fourDRide = new FourDRide("4D Adventure", 100, 30, true, 15.5, new List<string> { "3D Glasses", "Motion Seats", "Water Spray" });
            extremeAttraction = new ExtremeAttraction("Extreme Drop", 150, 16, false,
                new List<string> { "No heart conditions", "Not for pregnant women" });
            mediumAttraction = new MediumAttraction("Family Coaster", 120, 20, true, true);
            lightAttraction = new LightAttraction("Kiddie Ride", 100, 15, true, true);
        }

        [Test]
        public void TestMalfunctionReport_AssignedToRollerCoaster()
        {
            var report = new MalfunctionReport("Mechanical", "Chain malfunction", testDate, rollerCoaster);

            Assert.That(report.Attraction, Is.EqualTo(rollerCoaster));
            Assert.That(rollerCoaster.Reports, Contains.Item(report));
        }

        [Test]
        public void TestMalfunctionReport_AssignedToAllAttractionTypes()
        {
            var attractions = new Attraction[] { rollerCoaster, waterRide, fourDRide, extremeAttraction, mediumAttraction, lightAttraction };

            foreach (var attraction in attractions)
            {
                var report = new MalfunctionReport("Electrical", "Issue detected", testDate, attraction);
                Assert.That(report.Attraction, Is.EqualTo(attraction));
                Assert.That(attraction.Reports, Contains.Item(report));
            }
        }

        [Test]
        public void TestMultipleReports_AssignedToSameAttraction()
        {
            var report1 = new MalfunctionReport("Mechanical", "Chain slipped", testDate, rollerCoaster);
            var report2 = new MalfunctionReport("Electrical", "Lights flickering", testDate, rollerCoaster);

            Assert.That(rollerCoaster.Reports.Count, Is.EqualTo(2));
            Assert.That(rollerCoaster.Reports, Contains.Item(report1));
            Assert.That(rollerCoaster.Reports, Contains.Item(report2));
        }

        [Test]
        public void TestMalfunctionReport_NullAttraction_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new MalfunctionReport("Safety", "Brake issue", testDate, null!)
            );
        }

        [Test]
        public void TestMalfunctionReportExtentContainsReports()
        {
            var report = new MalfunctionReport("Mechanical", "Gear stuck", testDate, waterRide);

            Assert.That(MalfunctionReport.Extent, Contains.Item(report));
        }

        [Test]
        public void TestAttractionReportsCollectionIsReadOnly()
        {
            var report = new MalfunctionReport("Electrical", "Panel failure", testDate, fourDRide);

            Assert.That(fourDRide.Reports, Is.InstanceOf<IReadOnlyCollection<MalfunctionReport>>());
        }
    }
}
