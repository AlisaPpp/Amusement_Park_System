using System;
using Amusement_Park_System;
using NUnit.Framework;

namespace Amusement_Park_System_Tests
{
    public class RollerCoasterTests
    {
        private readonly RollerCoaster rollerCoaster =
            new RollerCoaster(1200.5, 85.5, 3);

        [Test]
        public void TestRollerCoasterTrackLength()
        {
            Assert.That(rollerCoaster.TrackLength, Is.EqualTo(1200.5));
        }

        [Test]
        public void TestRollerCoasterNegativeTrackLengthException()
        {
            Assert.Throws<ArgumentException>(() =>
                new RollerCoaster(-100, 85.5, 3));
        }

        [Test]
        public void TestRollerCoasterTrackLengthSetterNegativeException()
        {
            Assert.Throws<ArgumentException>(() =>
                rollerCoaster.TrackLength = -100);
        }

        [Test]
        public void TestRollerCoasterTrackLengthSetter()
        {
            rollerCoaster.TrackLength = 1500.0;
            Assert.That(rollerCoaster.TrackLength, Is.EqualTo(1500.0));
        }

        [Test]
        public void TestRollerCoasterMaxSpeed()
        {
            Assert.That(rollerCoaster.MaxSpeed, Is.EqualTo(85.5));
        }

        [Test]
        public void TestRollerCoasterNegativeMaxSpeedException()
        {
            Assert.Throws<ArgumentException>(() =>
                new RollerCoaster(1200.5, -50, 3));
        }

        [Test]
        public void TestRollerCoasterMaxSpeedSetterNegativeException()
        {
            Assert.Throws<ArgumentException>(() =>
                rollerCoaster.MaxSpeed = -50);
        }

        [Test]
        public void TestRollerCoasterMaxSpeedSetter()
        {
            rollerCoaster.MaxSpeed = 90.0;
            Assert.That(rollerCoaster.MaxSpeed, Is.EqualTo(90.0));
        }

        [Test]
        public void TestRollerCoasterNumberOfLoops()
        {
            Assert.That(rollerCoaster.NumberOfLoops, Is.EqualTo(3));
        }

        [Test]
        public void TestRollerCoasterNegativeNumberOfLoopsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new RollerCoaster(1200.5, 85.5, -2));
        }

        [Test]
        public void TestRollerCoasterNumberOfLoopsSetterNegativeException()
        {
            Assert.Throws<ArgumentException>(() =>
                rollerCoaster.NumberOfLoops = -2);
        }

        [Test]
        public void TestRollerCoasterNumberOfLoopsSetter()
        {
            rollerCoaster.NumberOfLoops = 4;
            Assert.That(rollerCoaster.NumberOfLoops, Is.EqualTo(4));
        }
    }
}
