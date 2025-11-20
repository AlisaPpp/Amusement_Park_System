using Amusement_Park_System;

namespace Amusement_Park_System_Tests
{
    public class LightAttractionTests
    {
        private Attraction lightAttraction = new LightAttraction("Zadra",
            100, 20, true, true);

        // Name Tests
        [Test]
        public void TestLightAttractionName()
        {
            Assert.That(lightAttraction.Name, Is.EqualTo("Zadra"));
        }

        [Test]
        public void TestLightAttractionEmptyNameException()
        {
            Assert.Throws<ArgumentException>(() => new LightAttraction("", 100, 20, true, true));
        }

        [Test]
        public void TestLightAttractionNullNameException()
        {
            Assert.Throws<ArgumentException>(() => new LightAttraction(null, 100, 20, true, true));
        }

        [Test]
        public void TestLightAttractionNameSetterEmptyException()
        {
            var attraction = new LightAttraction("Test", 100, 20, true, true);
            Assert.Throws<ArgumentException>(() => attraction.Name = "");
        }

        [Test]
        public void TestLightAttractionNameSetterNullException()
        {
            var attraction = new LightAttraction("Test", 100, 20, true, true);
            Assert.Throws<ArgumentException>(() => attraction.Name = null);
        }

        // Height Tests
        [Test]
        public void TestLightAttractionHeight()
        {
            Assert.That(lightAttraction.Height, Is.EqualTo(100));
        }

        [Test]
        public void TestLightAttractionNegativeHeightException()
        {
            Assert.Throws<ArgumentException>(() => new LightAttraction("Test", -10, 20, true, true));
        }

        [Test]
        public void TestLightAttractionHeightSetterNegativeException()
        {
            var attraction = new LightAttraction("Test", 100, 20, true, true);
            Assert.Throws<ArgumentException>(() => attraction.Height = -10);
        }

        // MaxSeats Tests
        [Test]
        public void TestLightAttractionMaxSeats()
        {
            Assert.That(lightAttraction.MaxSeats, Is.EqualTo(20));
        }

        [Test]
        public void TestLightAttractionZeroMaxSeatsException()
        {
            Assert.Throws<ArgumentException>(() => new LightAttraction("Test", 100, 0, true, true));
        }

        [Test]
        public void TestLightAttractionNegativeMaxSeatsException()
        {
            Assert.Throws<ArgumentException>(() => new LightAttraction("Test", 100, -5, true, true));
        }

        [Test]
        public void TestLightAttractionMaxSeatsSetterZeroException()
        {
            var attraction = new LightAttraction("Test", 100, 20, true, true);
            Assert.Throws<ArgumentException>(() => attraction.MaxSeats = 0);
        }

        [Test]
        public void TestLightAttractionMaxSeatsSetterNegativeException()
        {
            var attraction = new LightAttraction("Test", 100, 20, true, true);
            Assert.Throws<ArgumentException>(() => attraction.MaxSeats = -5);
        }

        // VipPassWorks Tests
        [Test]
        public void TestLightAttractionVipPassWorks()
        {
            Assert.That(lightAttraction.VipPassWorks, Is.True);
        }

        // State Tests
        [Test]
        public void TestLightAttractionState()
        {
            Assert.That(lightAttraction.State, Is.EqualTo(AttractionState.Active));
        }

        // IsParentSupervisionRequired Tests
        [Test]
        public void TestLightAttractionIsParentSupervisionRequired()
        {
            var light = (LightAttraction)lightAttraction;
            Assert.That(light.IsParentSupervisionRequired, Is.True);
        }

        // MinimumHeightRequirement Tests 
        [Test]
        public void TestLightAttractionMinimumHeightRequirement()
        {
            Assert.That(LightAttraction.MinimumHeightRequirement, Is.EqualTo(100));
        }
    }
}