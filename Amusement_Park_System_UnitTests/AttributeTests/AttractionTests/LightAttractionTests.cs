using System;
using System.Collections.Generic;
using Amusement_Park_System;
using NUnit.Framework;

namespace Amusement_Park_System_Tests
{
    public class LightAttractionTests
    {
        private Attraction lightAttraction = null!;

        [SetUp]
        public void Setup()
        {
            Attraction.ClearExtent();
            lightAttraction = new Attraction(
                "Zadra",
                100,
                20,
                true,
                null,
                new LightAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) });
        }

        [Test]
        public void TestLightAttractionName()
        {
            Assert.That(lightAttraction.Name, Is.EqualTo("Zadra"));
        }

        [Test]
        public void TestLightAttractionEmptyNameException()
        {
            Assert.Throws<ArgumentException>(() => new Attraction(
                "",
                100,
                20,
                true,
                null,
                new LightAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) }));
        }

        [Test]
        public void TestLightAttractionNullNameException()
        {
            Assert.Throws<ArgumentException>(() => new Attraction(
                null!,
                100,
                20,
                true,
                null,
                new LightAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) }));
        }

        [Test]
        public void TestLightAttractionNameSetterEmptyException()
        {
            var attraction = new Attraction(
                "Test",
                100,
                20,
                true,
                null,
                new LightAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) });

            Assert.Throws<ArgumentException>(() => attraction.Name = "");
        }

        [Test]
        public void TestLightAttractionNameSetterNullException()
        {
            var attraction = new Attraction(
                "Test",
                100,
                20,
                true,
                null,
                new LightAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) });

            Assert.Throws<ArgumentException>(() => attraction.Name = null!);
        }

        [Test]
        public void TestLightAttractionHeight()
        {
            Assert.That(lightAttraction.Height, Is.EqualTo(100));
        }

        [Test]
        public void TestLightAttractionNegativeHeightException()
        {
            Assert.Throws<ArgumentException>(() => new Attraction(
                "Test",
                -10,
                20,
                true,
                null,
                new LightAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) }));
        }

        [Test]
        public void TestLightAttractionHeightSetterNegativeException()
        {
            var attraction = new Attraction(
                "Test",
                100,
                20,
                true,
                null,
                new LightAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) });

            Assert.Throws<ArgumentException>(() => attraction.Height = -10);
        }

        [Test]
        public void TestLightAttractionMaxSeats()
        {
            Assert.That(lightAttraction.MaxSeats, Is.EqualTo(20));
        }

        [Test]
        public void TestLightAttractionZeroMaxSeatsException()
        {
            Assert.Throws<ArgumentException>(() => new Attraction(
                "Test",
                100,
                0,
                true,
                null,
                new LightAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) }));
        }

        [Test]
        public void TestLightAttractionNegativeMaxSeatsException()
        {
            Assert.Throws<ArgumentException>(() => new Attraction(
                "Test",
                100,
                -5,
                true,
                null,
                new LightAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) }));
        }

        [Test]
        public void TestLightAttractionMaxSeatsSetterZeroException()
        {
            var attraction = new Attraction(
                "Test",
                100,
                20,
                true,
                null,
                new LightAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) });

            Assert.Throws<ArgumentException>(() => attraction.MaxSeats = 0);
        }

        [Test]
        public void TestLightAttractionMaxSeatsSetterNegativeException()
        {
            var attraction = new Attraction(
                "Test",
                100,
                20,
                true,
                null,
                new LightAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) });

            Assert.Throws<ArgumentException>(() => attraction.MaxSeats = -5);
        }

        [Test]
        public void TestLightAttractionVipPassWorks()
        {
            Assert.That(lightAttraction.VipPassWorks, Is.True);
        }

        [Test]
        public void TestLightAttractionState()
        {
            Assert.That(lightAttraction.State, Is.EqualTo(AttractionState.Active));
        }

        [Test]
        public void TestLightAttractionIsParentSupervisionRequired()
        {
            Assert.That(((LightAttraction)lightAttraction.Intensity).IsParentSupervisionRequired, Is.True);
        }

        [Test]
        public void TestLightAttractionMinimumHeightRequirement()
        {
            Assert.That(lightAttraction.Intensity.MinimumHeightRequirement, Is.EqualTo(100));
        }
    }
}
