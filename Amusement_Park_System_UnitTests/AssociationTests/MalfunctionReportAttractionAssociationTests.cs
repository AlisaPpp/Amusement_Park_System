using System;
using System.Collections.Generic;
using Amusement_Park_System;
using Amusement_Park_System.Models;
using NUnit.Framework;

namespace Amusement_Park_System_Tests
{
    public class MalfunctionReportAttractionAssociationTests
    {
        private Attraction rollerCoasterAttraction = null!;
        private Attraction waterRideAttraction = null!;
        private Attraction fourDRideAttraction = null!;
        private Attraction extremeAttraction = null!;
        private Attraction mediumAttraction = null!;
        private Attraction lightAttraction = null!;
        private DateTime testDate;

        [SetUp]
        public void Setup()
        {
            MalfunctionReport.ClearExtent();
            Attraction.ClearExtent();

            testDate = new DateTime(2025, 12, 12);

            rollerCoasterAttraction = new Attraction(
                "Thunderbolt",
                140,
                24,
                true,
                null,
                new ExtremeAttraction(new List<string> { "No heart conditions", "Not for pregnant women" }),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) });

            waterRideAttraction = new Attraction(
                "Splash Mountain",
                120,
                20,
                true,
                null,
                new MediumAttraction(true),
                new List<IAttractionType> { new WaterRide(2.5, 25.0) });

            fourDRideAttraction = new Attraction(
                "4D Adventure",
                100,
                30,
                true,
                null,
                new MediumAttraction(true),
                new List<IAttractionType> { new FourDRide(15.5, new List<string> { "3D Glasses", "Motion Seats", "Water Spray" }) });

            extremeAttraction = new Attraction(
                "Extreme Drop",
                150,
                16,
                false,
                null,
                new ExtremeAttraction(new List<string> { "No heart conditions", "Not for pregnant women" }),
                new List<IAttractionType> { new RollerCoaster(400, 80, 2) });

            mediumAttraction = new Attraction(
                "Family Coaster",
                120,
                20,
                true,
                null,
                new MediumAttraction(true),
                new List<IAttractionType> { new RollerCoaster(500, 75, 1) });

            lightAttraction = new Attraction(
                "Kiddie Ride",
                100,
                15,
                true,
                null,
                new LightAttraction(true),
                new List<IAttractionType> { new FourDRide(5.0, new List<string> { "Bubbles" }) });
        }

        [Test]
        public void TestMalfunctionReport_AssignedToRollerCoaster()
        {
            var report = new MalfunctionReport("Mechanical", "Chain malfunction", testDate, rollerCoasterAttraction);

            Assert.That(report.Attraction, Is.EqualTo(rollerCoasterAttraction));
            Assert.That(rollerCoasterAttraction.Reports, Contains.Item(report));
        }

        [Test]
        public void TestMalfunctionReport_AssignedToAllAttractionTypes()
        {
            var attractions = new[]
            {
                rollerCoasterAttraction,
                waterRideAttraction,
                fourDRideAttraction,
                extremeAttraction,
                mediumAttraction,
                lightAttraction
            };

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
            var report1 = new MalfunctionReport("Mechanical", "Chain slipped", testDate, rollerCoasterAttraction);
            var report2 = new MalfunctionReport("Electrical", "Lights flickering", testDate, rollerCoasterAttraction);

            Assert.That(rollerCoasterAttraction.Reports.Count, Is.EqualTo(2));
            Assert.That(rollerCoasterAttraction.Reports, Contains.Item(report1));
            Assert.That(rollerCoasterAttraction.Reports, Contains.Item(report2));
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
            var report = new MalfunctionReport("Mechanical", "Gear stuck", testDate, waterRideAttraction);

            Assert.That(MalfunctionReport.Extent, Contains.Item(report));
        }

        [Test]
        public void TestAttractionReportsCollectionIsReadOnly()
        {
            var report = new MalfunctionReport("Electrical", "Panel failure", testDate, fourDRideAttraction);

            Assert.That(fourDRideAttraction.Reports, Is.InstanceOf<IReadOnlyCollection<MalfunctionReport>>());
        }
    }
}
