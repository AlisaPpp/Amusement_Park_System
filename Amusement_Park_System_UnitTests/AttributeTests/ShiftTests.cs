using System;
using System.Collections.Generic;
using Amusement_Park_System;
using Amusement_Park_System.Models;
using NUnit.Framework;

namespace Amusement_Park_System_Tests
{
    public class ShiftTests
    {
        private RideOperator rideOperator = null!;
        private Attraction attraction = null!;
        private Shift shift = null!;
        private Manager manager = null!;
        private DateTime testDate;

        [SetUp]
        public void Setup()
        {
            Shift.ClearExtent();
            RideOperator.ClearExtent();
            Manager.ClearExtent();
            Attraction.ClearExtent();

            testDate = new DateTime(2024, 1, 15);

            rideOperator = new RideOperator(
                "John", "Doe", "john@example.com",
                new DateTime(1990, 1, 1), 3, "OP12345", true);

           
            var rollerCoasterType = new RollerCoaster(
                trackLength: 1200.5,
                maxSpeed: 85.5,
                numberOfLoops: 3);

            
            var intensity = new MediumAttraction(familyFriendly: true);

            
            attraction = new Attraction(
                name: "Thunderbolt",
                height: 140,
                maxSeats: 24,
                vipPassWorks: true,
                zone: null,
                intensity: intensity,
                types: new List<IAttractionType> { rollerCoasterType });

            manager = new Manager(
                "Boss", "Manager", "boss@example.com",
                new DateTime(1980, 1, 1), 10);

            manager.AddManagedEmployee(rideOperator);

            shift = new Shift(
                testDate,
                new TimeSpan(9, 0, 0),
                new TimeSpan(17, 0, 0),
                rideOperator,
                attraction,
                manager);
        }

        [Test]
        public void TestShiftDate()
        {
            Assert.That(shift.Date, Is.EqualTo(new DateTime(2024, 1, 15)));
        }

        [Test]
        public void TestShiftDateSetter()
        {
            shift.Date = new DateTime(2024, 1, 16);
            Assert.That(shift.Date, Is.EqualTo(new DateTime(2024, 1, 16)));
        }

        [Test]
        public void TestShiftStartTime()
        {
            Assert.That(shift.StartTime, Is.EqualTo(new TimeSpan(9, 0, 0)));
        }

        [Test]
        public void TestShiftStartTimeSetter()
        {
            shift.StartTime = new TimeSpan(10, 0, 0);
            Assert.That(shift.StartTime, Is.EqualTo(new TimeSpan(10, 0, 0)));
        }

        [Test]
        public void TestShiftEndTime()
        {
            Assert.That(shift.EndTime, Is.EqualTo(new TimeSpan(17, 0, 0)));
        }

        [Test]
        public void TestShiftEndTimeBeforeStartTimeException()
        {
            var tempOperator = new RideOperator(
                "Test", "User", "test@example.com",
                new DateTime(1990, 1, 1), 3, "OP99999", true);

            manager.AddManagedEmployee(tempOperator);

            var tempType = new RollerCoaster(trackLength: 1000, maxSpeed: 80, numberOfLoops: 2);
            var tempIntensity = new LightAttraction(isParentSupervisionRequired: false);

            var tempAttraction = new Attraction(
                name: "Test Coaster",
                height: 140,
                maxSeats: 24,
                vipPassWorks: true,
                zone: null,
                intensity: tempIntensity,
                types: new List<IAttractionType> { tempType });

            Assert.Throws<ArgumentException>(() => new Shift(
                testDate,
                new TimeSpan(17, 0, 0),
                new TimeSpan(9, 0, 0),
                tempOperator,
                tempAttraction,
                manager));
        }

        [Test]
        public void TestShiftEndTimeEqualStartTimeException()
        {
            var tempOperator = new RideOperator(
                "Test", "User", "test@example.com",
                new DateTime(1990, 1, 1), 3, "OP99999", true);

            manager.AddManagedEmployee(tempOperator);

            var tempType = new RollerCoaster(trackLength: 1000, maxSpeed: 80, numberOfLoops: 2);
            var tempIntensity = new LightAttraction(isParentSupervisionRequired: false);

            var tempAttraction = new Attraction(
                name: "Test Coaster",
                height: 140,
                maxSeats: 24,
                vipPassWorks: true,
                zone: null,
                intensity: tempIntensity,
                types: new List<IAttractionType> { tempType });

            Assert.Throws<ArgumentException>(() => new Shift(
                testDate,
                new TimeSpan(9, 0, 0),
                new TimeSpan(9, 0, 0),
                tempOperator,
                tempAttraction,
                manager));
        }

        [Test]
        public void TestShiftEndTimeSetter()
        {
            shift.EndTime = new TimeSpan(18, 0, 0);
            Assert.That(shift.EndTime, Is.EqualTo(new TimeSpan(18, 0, 0)));
        }

        [Test]
        public void TestShiftEndTimeSetterBeforeStartTimeException()
        {
            Assert.Throws<ArgumentException>(() => shift.EndTime = new TimeSpan(8, 0, 0));
        }

        [Test]
        public void TestShiftEndTimeSetterEqualStartTimeException()
        {
            Assert.Throws<ArgumentException>(() => shift.EndTime = new TimeSpan(9, 0, 0));
        }
    }
}
