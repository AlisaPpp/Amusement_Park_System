using Amusement_Park_System;
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class ShiftPersistenceTests
    {
        private string _filePath = Shift.FilePath;
        private Employee employee;
        private Attraction attraction;

        // before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Shift.ClearExtent();
            
            employee = new RideOperator(
                "Test", "Employee", "test@example.com", 
                new DateTime(1990, 1, 1), 3, "OP12345", true);
            
            attraction = new RollerCoaster(
                "Test Coaster", 140, 24, true, 1200.5, 85.5, 3);
        }

        // after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Shift.ClearExtent();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            /*var shift1 = employee.AssignShift(
                attraction,
                new DateTime(2026, 5, 1),
                new DateTime(2026, 5, 1, 9, 0, 0),
                new DateTime(2026, 5, 1, 17, 0, 0));

            var shift2 = employee.AssignShift(
                attraction,
                new DateTime(2026, 5, 2),
                new DateTime(2026, 5, 2, 10, 0, 0),
                new DateTime(2026, 5, 2, 18, 0, 0));*/

            Assert.That(Shift.Extent.Count, Is.EqualTo(2));
            //Assert.That(Shift.Extent, Does.Contain(shift1));
            //Assert.That(Shift.Extent, Does.Contain(shift2));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            /*var shift = employee.AssignShift(
                attraction,
                new DateTime(2026, 5, 1),
                new DateTime(2026, 5, 1, 9, 0, 0),
                new DateTime(2026, 5, 1, 17, 0, 0));*/

            var fromExtent = Shift.Extent.Single(s => s.Date == new DateTime(2026, 5, 1));

            //shift.EndTime = new DateTime(2026, 5, 1, 18, 0, 0);

            Assert.That(fromExtent.EndTime, Is.EqualTo(new DateTime(2026, 5, 1, 18, 0, 0)));
        }

        [Test]
        public void Save_CreatesFile()
        {
            /*var shift = employee.AssignShift(
                attraction,
                new DateTime(2026, 5, 1),
                new DateTime(2026, 5, 1, 9, 0, 0),
                new DateTime(2026, 5, 1, 17, 0, 0));*/

            Shift.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            /*var shift1 = employee.AssignShift(
                attraction,
                new DateTime(2026, 5, 1),
                new DateTime(2026, 5, 1, 9, 0, 0),
                new DateTime(2026, 5, 1, 17, 0, 0));

            var shift2 = employee.AssignShift(
                attraction,
                new DateTime(2026, 5, 2),
                new DateTime(2026, 5, 2, 10, 0, 0),
                new DateTime(2026, 5, 2, 18, 0, 0));*/

            Shift.Save();

            Shift.ClearExtent();
            Shift.Load();

            Assert.That(Shift.Extent.Count, Is.EqualTo(2));

            var first = Shift.Extent.Single(s => s.Date == new DateTime(2026, 5, 1));
            Assert.That(first.StartTime, Is.EqualTo(new DateTime(2026, 5, 1, 9, 0, 0)));
            Assert.That(first.EndTime, Is.EqualTo(new DateTime(2026, 5, 1, 17, 0, 0)));

            var second = Shift.Extent.Single(s => s.Date == new DateTime(2026, 5, 2));
            Assert.That(second.StartTime, Is.EqualTo(new DateTime(2026, 5, 2, 10, 0, 0)));
            Assert.That(second.EndTime, Is.EqualTo(new DateTime(2026, 5, 2, 18, 0, 0)));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            /*var shift = employee.AssignShift(
                attraction,
                new DateTime(2026, 5, 1),
                new DateTime(2026, 5, 1, 9, 0, 0),
                new DateTime(2026, 5, 1, 17, 0, 0));*/

            Assert.That(Shift.Extent, Is.Not.Empty);

            Shift.Load();

            Assert.That(Shift.Extent, Is.Empty);
        }
    }
}