using Amusement_Park_System;

namespace Amusement_Park_System_Tests
{
    public class FourDRideTests
    {
        private List<string> effectTypes = new List<string> { "3D Glasses", "Motion Seats", "Water Spray" };
        private Attraction fourDRide = new FourDRide("4D Adventure", 100, 40, true, 15.5, 
            new List<string> { "3D Glasses", "Motion Seats", "Water Spray" });

        // ShowDuration Tests
        [Test]
        public void TestFourDRideShowDuration()
        {
            var ride = (FourDRide)fourDRide;
            Assert.That(ride.ShowDuration, Is.EqualTo(15.5));
        }

        [Test]
        public void TestFourDRideNegativeShowDurationException()
        {
            Assert.Throws<ArgumentException>(() => new FourDRide("Test", 100, 40, true, -5.0, effectTypes));
        }

        [Test]
        public void TestFourDRideShowDurationSetterNegativeException()
        {
            var ride = new FourDRide("Test", 100, 40, true, 15.5, effectTypes);
            Assert.Throws<ArgumentException>(() => ride.ShowDuration = -5.0);
        }

        [Test]
        public void TestFourDRideShowDurationSetter()
        {
            var ride = new FourDRide("Test", 100, 40, true, 15.5, effectTypes);
            ride.ShowDuration = 20.0;
            Assert.That(ride.ShowDuration, Is.EqualTo(20.0));
        }

        // EffectTypes Tests
        [Test]
        public void TestFourDRideEffectTypes()
        {
            var ride = (FourDRide)fourDRide;
            Assert.That(ride.EffectTypes, Is.EqualTo(effectTypes));
        }

        [Test]
        public void TestFourDRideNullEffectTypesException()
        {
            Assert.Throws<ArgumentException>(() => new FourDRide("Test", 100, 40, true, 15.5, null));
        }

        [Test]
        public void TestFourDRideEmptyEffectTypesException()
        {
            Assert.Throws<ArgumentException>(() => new FourDRide("Test", 100, 40, true, 15.5, new List<string>()));
        }

        [Test]
        public void TestFourDRideEffectTypesSetterNullException()
        {
            var ride = new FourDRide("Test", 100, 40, true, 15.5, effectTypes);
            Assert.Throws<ArgumentException>(() => ride.EffectTypes = null);
        }

        [Test]
        public void TestFourDRideEffectTypesSetterEmptyException()
        {
            var ride = new FourDRide("Test", 100, 40, true, 15.5, effectTypes);
            Assert.Throws<ArgumentException>(() => ride.EffectTypes = new List<string>());
        }

        [Test]
        public void TestFourDRideEffectTypesSetter()
        {
            var ride = new FourDRide("Test", 100, 40, true, 15.5, effectTypes);
            var newEffects = new List<string> { "Wind", "Scents" };
            ride.EffectTypes = newEffects;
            Assert.That(ride.EffectTypes, Is.EqualTo(newEffects));
        }
    }
}