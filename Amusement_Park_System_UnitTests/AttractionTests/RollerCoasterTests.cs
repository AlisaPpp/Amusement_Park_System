using Amusement_Park_System;

namespace Amusement_Park_System_Tests
{
    public class RollerCoasterTests
    {
        private readonly Attraction rollerCoaster = new RollerCoaster("Thunder storm", 140, 24, true, 1200.5, 85.5, 3);

        // TrackLength Tests
        [Test]
        public void TestRollerCoasterTrackLength()
        {
            var coaster = (RollerCoaster)rollerCoaster;
            Assert.That(coaster.TrackLength, Is.EqualTo(1200.5));
        }

        [Test]
        public void TestRollerCoasterNegativeTrackLengthException()
        {
            Assert.Throws<ArgumentException>(() => new RollerCoaster("Test", 140, 24, true, -100, 85.5, 3));
        }

        [Test]
        public void TestRollerCoasterTrackLengthSetterNegativeException()
        {
            var coaster = new RollerCoaster("Test", 140, 24, true, 1200.5, 85.5, 3);
            Assert.Throws<ArgumentException>(() => coaster.TrackLength = -100);
        }

        [Test]
        public void TestRollerCoasterTrackLengthSetter()
        {
            var coaster = new RollerCoaster("Test", 140, 24, true, 1200.5, 85.5, 3);
            coaster.TrackLength = 1500.0;
            Assert.That(coaster.TrackLength, Is.EqualTo(1500.0));
        }

        // MaxSpeed Tests
        [Test]
        public void TestRollerCoasterMaxSpeed()
        {
            var coaster = (RollerCoaster)rollerCoaster;
            Assert.That(coaster.MaxSpeed, Is.EqualTo(85.5));
        }

        [Test]
        public void TestRollerCoasterNegativeMaxSpeedException()
        {
            Assert.Throws<ArgumentException>(() => new RollerCoaster("Test", 140, 24, true, 1200.5, -50, 3));
        }

        [Test]
        public void TestRollerCoasterMaxSpeedSetterNegativeException()
        {
            var coaster = new RollerCoaster("Test", 140, 24, true, 1200.5, 85.5, 3);
            Assert.Throws<ArgumentException>(() => coaster.MaxSpeed = -50);
        }

        [Test]
        public void TestRollerCoasterMaxSpeedSetter()
        {
            var coaster = new RollerCoaster("Test", 140, 24, true, 1200.5, 85.5, 3);
            coaster.MaxSpeed = 90.0;
            Assert.That(coaster.MaxSpeed, Is.EqualTo(90.0));
        }

        // NumberOfLoops Tests
        [Test]
        public void TestRollerCoasterNumberOfLoops()
        {
            var coaster = (RollerCoaster)rollerCoaster;
            Assert.That(coaster.NumberOfLoops, Is.EqualTo(3));
        }

        [Test]
        public void TestRollerCoasterNegativeNumberOfLoopsException()
        {
            Assert.Throws<ArgumentException>(() => new RollerCoaster("Test", 140, 24, true, 1200.5, 85.5, -2));
        }

        [Test]
        public void TestRollerCoasterNumberOfLoopsSetterNegativeException()
        {
            var coaster = new RollerCoaster("Test", 140, 24, true, 1200.5, 85.5, 3);
            Assert.Throws<ArgumentException>(() => coaster.NumberOfLoops = -2);
        }

        [Test]
        public void TestRollerCoasterNumberOfLoopsSetter()
        {
            var coaster = new RollerCoaster("Test", 140, 24, true, 1200.5, 85.5, 3);
            coaster.NumberOfLoops = 4;
            Assert.That(coaster.NumberOfLoops, Is.EqualTo(4));
        }
    }
}