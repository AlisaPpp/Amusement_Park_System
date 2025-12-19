using System;
using Amusement_Park_System;
using NUnit.Framework;

namespace Amusement_Park_System_Tests
{
    public class WaterRideTests
    {
        private readonly WaterRide waterRide =
            new WaterRide(2.5, 22.0);

        [Test]
        public void TestWaterRideWaterDepth()
        {
            Assert.That(waterRide.WaterDepth, Is.EqualTo(2.5));
        }

        [Test]
        public void TestWaterRideNegativeWaterDepthException()
        {
            Assert.Throws<ArgumentException>(() =>
                new WaterRide(-1.0, 22.0));
        }

        [Test]
        public void TestWaterRideWaterDepthSetterNegativeException()
        {
            Assert.Throws<ArgumentException>(() =>
                waterRide.WaterDepth = -1.0);
        }

        [Test]
        public void TestWaterRideWaterDepthSetter()
        {
            waterRide.WaterDepth = 3.0;
            Assert.That(waterRide.WaterDepth, Is.EqualTo(3.0));
        }

        [Test]
        public void TestWaterRideWaterTemperature()
        {
            Assert.That(waterRide.WaterTemperature, Is.EqualTo(22.0));
        }

        [Test]
        public void TestWaterRideWaterTemperatureSetter()
        {
            waterRide.WaterTemperature = 25.0;
            Assert.That(waterRide.WaterTemperature, Is.EqualTo(25.0));
        }
    }
}