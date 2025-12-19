using System;
using System.Collections.Generic;
using Amusement_Park_System;
using NUnit.Framework;

namespace Amusement_Park_System_Tests
{
    public class FourDRideTests
    {
        private List<string> effectTypes = null!;
        private FourDRide fourDRideType = null!;

        [SetUp]
        public void Setup()
        {
            effectTypes = new List<string> { "3D Glasses", "Motion Seats", "Water Spray" };
            fourDRideType = new FourDRide(15.5, new List<string>(effectTypes));
        }

        // ShowDuration Tests
        [Test]
        public void TestFourDRideShowDuration()
        {
            Assert.That(fourDRideType.ShowDuration, Is.EqualTo(15.5));
        }

        [Test]
        public void TestFourDRideNegativeShowDurationException()
        {
            Assert.Throws<ArgumentException>(() => new FourDRide(-5.0, effectTypes));
        }

        [Test]
        public void TestFourDRideShowDurationSetterNegativeException()
        {
            var ride = new FourDRide(15.5, effectTypes);
            Assert.Throws<ArgumentException>(() => ride.ShowDuration = -5.0);
        }

        [Test]
        public void TestFourDRideShowDurationSetter()
        {
            var ride = new FourDRide(15.5, effectTypes);
            ride.ShowDuration = 20.0;
            Assert.That(ride.ShowDuration, Is.EqualTo(20.0));
        }

        // EffectTypes Tests
        [Test]
        public void TestFourDRideEffectTypes()
        {
            CollectionAssert.AreEqual(effectTypes, fourDRideType.EffectTypes);
        }

        [Test]
        public void TestFourDRideNullEffectTypesException()
        {
            Assert.Throws<ArgumentException>(() => new FourDRide(15.5, null!));
        }

        [Test]
        public void TestFourDRideEmptyEffectTypesException()
        {
            Assert.Throws<ArgumentException>(() => new FourDRide(15.5, new List<string>()));
        }

        [Test]
        public void TestFourDRideEffectTypesSetterNullException()
        {
            var ride = new FourDRide(15.5, effectTypes);
            Assert.Throws<ArgumentException>(() => ride.EffectTypes = null!);
        }

        [Test]
        public void TestFourDRideEffectTypesSetterEmptyException()
        {
            var ride = new FourDRide(15.5, effectTypes);
            Assert.Throws<ArgumentException>(() => ride.EffectTypes = new List<string>());
        }

        [Test]
        public void TestFourDRideEffectTypesSetter()
        {
            var ride = new FourDRide(15.5, effectTypes);
            var newEffects = new List<string> { "Wind", "Scents" };
            ride.EffectTypes = newEffects;

            CollectionAssert.AreEqual(newEffects, ride.EffectTypes);
        }

        [Test]
        public void TestFourDRideEffectTypesReturnsCopy()
        {

            var ride = new FourDRide(15.5, effectTypes);

            var listFromGetter = ride.EffectTypes;
            listFromGetter.Add("Injected");

         
            CollectionAssert.DoesNotContain(ride.EffectTypes, "Injected");
        }
    }
}
