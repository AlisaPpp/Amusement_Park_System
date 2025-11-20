using Amusement_Park_System;

namespace Amusement_Park_System_Tests
{
    public class WaterRideTests
    {
        private readonly Attraction waterRide = new WaterRide("Abyssus", 120, 30, false, 2.5, 22.0);

        // WaterDepth Tests
        [Test]
        public void TestWaterRideWaterDepth()
        {
            var ride = (WaterRide)waterRide;
            Assert.That(ride.WaterDepth, Is.EqualTo(2.5));
        }

        [Test]
        public void TestWaterRideNegativeWaterDepthException()
        {
            Assert.Throws<ArgumentException>(() => new WaterRide("Test", 120, 30, false, -1.0, 22.0));
        }

        [Test]
        public void TestWaterRideWaterDepthSetterNegativeException()
        {
            var ride = new WaterRide("Test", 120, 30, false, 2.5, 22.0);
            Assert.Throws<ArgumentException>(() => ride.WaterDepth = -1.0);
        }

        [Test]
        public void TestWaterRideWaterDepthSetter()
        {
            var ride = new WaterRide("Test", 120, 30, false, 2.5, 22.0);
            ride.WaterDepth = 3.0;
            Assert.That(ride.WaterDepth, Is.EqualTo(3.0));
        }

        // WaterTemperature Tests
        [Test]
        public void TestWaterRideWaterTemperature()
        {
            var ride = (WaterRide)waterRide;
            Assert.That(ride.WaterTemperature, Is.EqualTo(22.0));
        }

        [Test]
        public void TestWaterRideWaterTemperatureSetter()
        {
            var ride = new WaterRide("Test", 120, 30, false, 2.5, 22.0);
            ride.WaterTemperature = 25.0;
            Assert.That(ride.WaterTemperature, Is.EqualTo(25.0));
        }
    }
}