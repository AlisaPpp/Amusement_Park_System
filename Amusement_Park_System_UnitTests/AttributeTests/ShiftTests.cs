using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class ShiftTests
    {
        private readonly Shift shift = new Shift(
            new DateTime(2024, 1, 15),
            new DateTime(2024, 1, 15, 9, 0, 0),
            new DateTime(2024, 1, 15, 17, 0, 0));

        // Date Tests
        [Test]
        public void TestShiftDate()
        {
            Assert.That(shift.Date, Is.EqualTo(new DateTime(2024, 1, 15)));
        }

        [Test]
        public void TestShiftDateSetter()
        {
            var testShift = new Shift(
                new DateTime(2024, 1, 15),
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            testShift.Date = new DateTime(2024, 1, 16);
            Assert.That(testShift.Date, Is.EqualTo(new DateTime(2024, 1, 16)));
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
            var testShift = new Shift(
                new DateTime(2024, 1, 15),
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            testShift.StartTime = new DateTime(2024, 1, 15, 10, 0, 0);
            Assert.That(testShift.StartTime, Is.EqualTo(new DateTime(2024, 1, 15, 10, 0, 0)));
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
            Assert.Throws<ArgumentException>(() => new Shift(
                new DateTime(2024, 1, 15),
                new DateTime(2024, 1, 15, 17, 0, 0),
                new DateTime(2024, 1, 15, 9, 0, 0)));
        }

        [Test]
        public void TestShiftEndTimeEqualStartTimeException()
        {
            Assert.Throws<ArgumentException>(() => new Shift(
                new DateTime(2024, 1, 15),
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 9, 0, 0)));
        }

        [Test]
        public void TestShiftEndTimeSetter()
        {
            var testShift = new Shift(
                new DateTime(2024, 1, 15),
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            testShift.EndTime = new DateTime(2024, 1, 15, 18, 0, 0);
            Assert.That(testShift.EndTime, Is.EqualTo(new DateTime(2024, 1, 15, 18, 0, 0)));
        }

        [Test]
        public void TestShiftEndTimeSetterBeforeStartTimeException()
        {
            var testShift = new Shift(
                new DateTime(2024, 1, 15),
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            Assert.Throws<ArgumentException>(() => testShift.EndTime = new DateTime(2024, 1, 15, 8, 0, 0));
        }

        [Test]
        public void TestShiftEndTimeSetterEqualStartTimeException()
        {
            var testShift = new Shift(
                new DateTime(2024, 1, 15),
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            Assert.Throws<ArgumentException>(() => testShift.EndTime = new DateTime(2024, 1, 15, 9, 0, 0));
        }
    }
}