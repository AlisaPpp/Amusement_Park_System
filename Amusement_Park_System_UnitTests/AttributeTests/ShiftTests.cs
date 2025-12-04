using Amusement_Park_System;
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class ShiftTests
    {
        private RideOperator rideOperator;
        private RollerCoaster rollerCoaster;
        private Shift shift;
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

            // Create shift through Employee's public method
            shift = rideOperator.AssignShift(
                rollerCoaster,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
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

        // StartTime Tests
        [Test]
        public void TestShiftStartTime()
        {
            Assert.That(shift.StartTime, Is.EqualTo(new DateTime(2024, 1, 15, 9, 0, 0)));
        }

        [Test]
        public void TestShiftStartTimeSetter()
        {
            shift.StartTime = new DateTime(2024, 1, 15, 10, 0, 0);
            Assert.That(shift.StartTime, Is.EqualTo(new DateTime(2024, 1, 15, 10, 0, 0)));
        }

        // EndTime Tests
        [Test]
        public void TestShiftEndTime()
        {
            Assert.That(shift.EndTime, Is.EqualTo(new DateTime(2024, 1, 15, 17, 0, 0)));
        }

        [Test]
        public void TestShiftEndTimeBeforeStartTimeException()
        {
            // Create a new employee and attraction for this test
            var tempOperator = new RideOperator(
                "Test", "User", "test@example.com",
                new DateTime(1990, 1, 1), 3, "OP99999", true);

            var tempAttraction = new RollerCoaster(
                "Test Coaster", 140, 24, true, 1000, 80, 2);

            Assert.Throws<ArgumentException>(() => tempOperator.AssignShift(
                tempAttraction,
                testDate,
                new DateTime(2024, 1, 15, 17, 0, 0),
                new DateTime(2024, 1, 15, 9, 0, 0)));
        }

        [Test]
        public void TestShiftEndTimeEqualStartTimeException()
        {
            var tempOperator = new RideOperator(
                "Test", "User", "test@example.com",
                new DateTime(1990, 1, 1), 3, "OP99999", true);

            var tempAttraction = new RollerCoaster(
                "Test Coaster", 140, 24, true, 1000, 80, 2);

            Assert.Throws<ArgumentException>(() => tempOperator.AssignShift(
                tempAttraction,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 9, 0, 0)));
        }

        [Test]
        public void TestShiftEndTimeSetter()
        {
            shift.EndTime = new DateTime(2024, 1, 15, 18, 0, 0);
            Assert.That(shift.EndTime, Is.EqualTo(new DateTime(2024, 1, 15, 18, 0, 0)));
        }

        [Test]
        public void TestShiftEndTimeSetterBeforeStartTimeException()
        {
            Assert.Throws<ArgumentException>(() => shift.EndTime = new DateTime(2024, 1, 15, 8, 0, 0));
        }

        [Test]
        public void TestShiftEndTimeSetterEqualStartTimeException()
        {
            Assert.Throws<ArgumentException>(() => shift.EndTime = new DateTime(2024, 1, 15, 9, 0, 0));
        }
    }
}