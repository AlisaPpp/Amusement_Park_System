using System;
using System.Collections.Generic;
using Amusement_Park_System;
using NUnit.Framework;

namespace Amusement_Park_System_Tests
{
    public class MediumAttractionTests
    {
        private Attraction mediumAttraction = null!;

        [SetUp]
        public void Setup()
        {
            Attraction.ClearExtent();
            mediumAttraction = new Attraction(
                "Pepsy hyperion",
                120,
                24,
                false,
                null,
                new MediumAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) });
        }

        [Test]
        public void TestMediumAttractionName()
        {
            Assert.That(mediumAttraction.Name, Is.EqualTo("Pepsy hyperion"));
        }

        [Test]
        public void TestMediumAttractionEmptyNameException()
        {
            Assert.Throws<ArgumentException>(() => new Attraction(
                "",
                120,
                24,
                false,
                null,
                new MediumAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) }));
        }

        [Test]
        public void TestMediumAttractionNullNameException()
        {
            Assert.Throws<ArgumentException>(() => new Attraction(
                null!,
                120,
                24,
                false,
                null,
                new MediumAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) }));
        }

        [Test]
        public void TestMediumAttractionNameSetterEmptyException()
        {
            var attraction = new Attraction(
                "Test",
                120,
                24,
                false,
                null,
                new MediumAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) });

            Assert.Throws<ArgumentException>(() => attraction.Name = "");
        }

        [Test]
        public void TestMediumAttractionNameSetterNullException()
        {
            var attraction = new Attraction(
                "Test",
                120,
                24,
                false,
                null,
                new MediumAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) });

            Assert.Throws<ArgumentException>(() => attraction.Name = null!);
        }

        [Test]
        public void TestMediumAttractionHeight()
        {
            Assert.That(mediumAttraction.Height, Is.EqualTo(120));
        }

        [Test]
        public void TestMediumAttractionNegativeHeightException()
        {
            Assert.Throws<ArgumentException>(() => new Attraction(
                "Test",
                -10,
                24,
                false,
                null,
                new MediumAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) }));
        }

        [Test]
        public void TestMediumAttractionHeightSetterNegativeException()
        {
            var attraction = new Attraction(
                "Test",
                120,
                24,
                false,
                null,
                new MediumAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) });

            Assert.Throws<ArgumentException>(() => attraction.Height = -10);
        }

        [Test]
        public void TestMediumAttractionMaxSeats()
        {
            Assert.That(mediumAttraction.MaxSeats, Is.EqualTo(24));
        }

        [Test]
        public void TestMediumAttractionZeroMaxSeatsException()
        {
            Assert.Throws<ArgumentException>(() => new Attraction(
                "Test",
                120,
                0,
                false,
                null,
                new MediumAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) }));
        }

        [Test]
        public void TestMediumAttractionNegativeMaxSeatsException()
        {
            Assert.Throws<ArgumentException>(() => new Attraction(
                "Test",
                120,
                -5,
                false,
                null,
                new MediumAttraction(true),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) }));
        }

        [Test]
        public void TestMediumAttractionVipPassWorks()
        {
            Assert.That(mediumAttraction.VipPassWorks, Is.False);
        }

        [Test]
        public void TestMediumAttractionState()
        {
            Assert.That(mediumAttraction.State, Is.EqualTo(AttractionState.Active));
        }

        [Test]
        public void TestMediumAttractionFamilyFriendly()
        {
            Assert.That(((MediumAttraction)mediumAttraction.Intensity).FamilyFriendly, Is.True);
        }

        [Test]
        public void TestMediumAttractionMinimumAge()
        {
            Assert.That(mediumAttraction.Intensity.MinimumAge, Is.EqualTo(8));
        }

        [Test]
        public void TestMediumAttractionMinimumHeightRequirement()
        {
            Assert.That(mediumAttraction.Intensity.MinimumHeightRequirement, Is.EqualTo(120));
        }
    }
}
