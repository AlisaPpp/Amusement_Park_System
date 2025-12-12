using Amusement_Park_System;
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class ShiftTests
    {
        private RideOperator rideOperator;
        private RollerCoaster rollerCoaster;
        private Shift shift;
        private Manager manager;
        private DateTime testDate;

        [SetUp]
        public void Setup()
        {
            Shift.ClearExtent();
            RideOperator.ClearExtent();
            RollerCoaster.ClearExtent();

            testDate = new DateTime(2024, 1, 15);

            // Create required objects first
            rideOperator = new RideOperator(
                "John", "Doe", "john@example.com",
                new DateTime(1990, 1, 1), 3, "OP12345", true);

            rollerCoaster = new RollerCoaster(
                "Thunderbolt", 140, 24, true, 1200.5, 85.5, 3);
            
            manager = new Manager("Boss", "Manager", "boss@example.com", 
                new DateTime(1980, 1, 1), 10);

            shift = new Shift(testDate, 
                new TimeSpan(9,0,0), new TimeSpan(17,0,0),
                rideOperator, rollerCoaster, manager);
        }

        // Date Tests
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
            Assert.That(shift.StartTime, Is.EqualTo(new TimeSpan(9,0,0)));
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
            Assert.That(shift.EndTime, Is.EqualTo(new TimeSpan(17,0,0)));
        }

        [Test]
        public void TestShiftEndTimeBeforeStartTimeException()
        {
            var tempOperator = new RideOperator(
                "Test", "User", "test@example.com",
                new DateTime(1990, 1, 1), 3, "OP99999", true);

            var tempAttraction = new RollerCoaster(
                "Test Coaster", 140, 24, true, 1000, 80, 2);

            Assert.Throws<ArgumentException>(() => new Shift(testDate, 
                new TimeSpan(17,0,0), new TimeSpan(9,0,0),
                tempOperator, tempAttraction, manager));
        }

        [Test]
        public void TestShiftEndTimeEqualStartTimeException()
        {
            var tempOperator = new RideOperator(
                "Test", "User", "test@example.com",
                new DateTime(1990, 1, 1), 3, "OP99999", true);

            var tempAttraction = new RollerCoaster(
                "Test Coaster", 140, 24, true, 1000, 80, 2);

            Assert.Throws<ArgumentException>(() => new Shift(testDate, 
                new TimeSpan(9,0,0), new TimeSpan(9,0,0),
                tempOperator, tempAttraction, manager));
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