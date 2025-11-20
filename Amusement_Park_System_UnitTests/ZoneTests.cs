using Amusement_Park_System;

namespace Amusement_Park_System_Tests
{
    public class ZoneTests
    {
        private Zone zone = new Zone("Aqualantis", "Water theme", 
            new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));

        // Name Tests
        [Test]
        public void TestZoneName()
        {
            Assert.That(zone.Name, Is.EqualTo("Aqualantis"));
        }

        [Test]
        public void TestZoneEmptyNameException()
        {
            Assert.Throws<ArgumentException>(() => new Zone("", "Theme", 
                new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)));
        }

        [Test]
        public void TestZoneNullNameException()
        {
            Assert.Throws<ArgumentException>(() => new Zone(null, "Theme", 
                new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)));
        }

        [Test]
        public void TestZoneNameSetterEmptyException()
        {
            var testZone = new Zone("Test", "Theme", 
                new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            Assert.Throws<ArgumentException>(() => testZone.Name = "");
        }

        [Test]
        public void TestZoneNameSetterNullException()
        {
            var testZone = new Zone("Test", "Theme", 
                new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            Assert.Throws<ArgumentException>(() => testZone.Name = null);
        }

        // Theme Tests
        [Test]
        public void TestZoneTheme()
        {
            Assert.That(zone.Theme, Is.EqualTo("Water theme"));
        }

        [Test]
        public void TestZoneEmptyThemeException()
        {
            Assert.Throws<ArgumentException>(() => new Zone("Adventure Land", "", 
                new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)));
        }

        [Test]
        public void TestZoneNullThemeException()
        {
            Assert.Throws<ArgumentException>(() => new Zone("Adventure Land", null, 
                new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)));
        }

        [Test]
        public void TestZoneThemeSetterEmptyException()
        {
            var testZone = new Zone("Test", "Theme", 
                new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            Assert.Throws<ArgumentException>(() => testZone.Theme = "");
        }

        [Test]
        public void TestZoneThemeSetterNullException()
        {
            var testZone = new Zone("Test", "Theme", 
                new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            Assert.Throws<ArgumentException>(() => testZone.Theme = null);
        }

        // OpeningTime Tests
        [Test]
        public void TestZoneOpeningTime()
        {
            Assert.That(zone.OpeningTime, Is.EqualTo(new TimeSpan(9, 0, 0)));
        }

        // ClosingTime Tests
        [Test]
        public void TestZoneClosingTime()
        {
            Assert.That(zone.ClosingTime, Is.EqualTo(new TimeSpan(18, 0, 0)));
        }

        [Test]
        public void TestZoneClosingTimeBeforeOpeningException()
        {
            Assert.Throws<ArgumentException>(() => new Zone("Adventure Land", "Theme", 
                new TimeSpan(18, 0, 0), new TimeSpan(9, 0, 0)));
        }

        [Test]
        public void TestZoneClosingTimeEqualOpeningException()
        {
            Assert.Throws<ArgumentException>(() => new Zone("Adventure Land", "Theme", 
                new TimeSpan(9, 0, 0), new TimeSpan(9, 0, 0)));
        }

        [Test]
        public void TestZoneClosingTimeSetterBeforeOpeningException()
        {
            var testZone = new Zone("Test", "Theme", 
                new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            Assert.Throws<ArgumentException>(() => testZone.ClosingTime = new TimeSpan(8, 0, 0));
        }

        [Test]
        public void TestZoneClosingTimeSetterEqualOpeningException()
        {
            var testZone = new Zone("Test", "Theme", 
                new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            Assert.Throws<ArgumentException>(() => testZone.ClosingTime = new TimeSpan(9, 0, 0));
        }
    }
}