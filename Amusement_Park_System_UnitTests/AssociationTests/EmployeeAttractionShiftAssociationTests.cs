using System;
using System.Collections.Generic;
using Amusement_Park_System;
using Amusement_Park_System.Models;
using NUnit.Framework;

namespace Amusement_Park_System_Tests
{
    public class EmployeeAttractionShiftAssociationTests
    {
        private RideOperator rideOperator1 = null!;
        private RideOperator rideOperator2 = null!;
        private Manager manager = null!;
        private Attraction rollerCoasterAttraction = null!;
        private Attraction waterRideAttraction = null!;
        private DateTime testDate;

        [SetUp]
        public void Setup()
        {
            Shift.ClearExtent();
            RideOperator.ClearExtent();
            Manager.ClearExtent();
            Attraction.ClearExtent();

            testDate = new DateTime(2024, 1, 15);

            manager = new Manager(
                "Jane", "Smith", "jane@example.com",
                new DateTime(1985, 5, 15), 5);

            rideOperator1 = new RideOperator(
                "John", "Doe", "john@example.com",
                new DateTime(1990, 1, 1), 3, "OP12345", true);

            rideOperator2 = new RideOperator(
                "Alice", "Brown", "alice@example.com",
                new DateTime(1992, 2, 2), 4, "OP99999", false);

            rollerCoasterAttraction = new Attraction(
                "Thunderbolt",
                140,
                24,
                true,
                null,
                new ExtremeAttraction(new List<string> { "No heart conditions", "Not for pregnant women" }),
                new List<IAttractionType> { new RollerCoaster(1200.5, 85.5, 3) });

            waterRideAttraction = new Attraction(
                "Splash Mountain",
                120,
                20,
                true,
                null,
                new MediumAttraction(true),
                new List<IAttractionType> { new WaterRide(2.5, 25.0) });

            manager.AddManagedEmployee(rideOperator1);
            manager.AddManagedEmployee(rideOperator2);
        }

        [Test]
        public void TestShiftConstructorCreatesAssociation()
        {
            var shift = new Shift(
                testDate,
                new TimeSpan(9, 0, 0),
                new TimeSpan(17, 0, 0),
                rideOperator1,
                rollerCoasterAttraction,
                manager);

            Assert.That(rideOperator1.Shifts, Contains.Item(shift));
            Assert.That(rollerCoasterAttraction.Shifts, Contains.Item(shift));
            Assert.That(manager.ShiftsAssigned, Contains.Item(shift));

            Assert.That(shift.Employee, Is.EqualTo(rideOperator1));
            Assert.That(shift.Attraction, Is.EqualTo(rollerCoasterAttraction));
            Assert.That(shift.Manager, Is.EqualTo(manager));
        }

        [Test]
        public void TestCreatingTwoShiftsSameParamsAllowed()
        {
            var shift1 = new Shift(
                testDate,
                new TimeSpan(9, 0, 0),
                new TimeSpan(17, 0, 0),
                rideOperator1,
                rollerCoasterAttraction,
                manager);

            var shift2 = new Shift(
                testDate,
                new TimeSpan(9, 0, 0),
                new TimeSpan(17, 0, 0),
                rideOperator1,
                rollerCoasterAttraction,
                manager);

            Assert.That(rideOperator1.Shifts.Count, Is.EqualTo(2));
            Assert.That(rollerCoasterAttraction.Shifts.Count, Is.EqualTo(2));
            Assert.That(manager.ShiftsAssigned.Count, Is.EqualTo(2));
            Assert.That(manager.ShiftsAssigned, Contains.Item(shift1));
            Assert.That(manager.ShiftsAssigned, Contains.Item(shift2));
        }

        [Test]
        public void TestShiftDeleteRemovesAssociations()
        {
            var shift = new Shift(
                testDate,
                new TimeSpan(9, 0, 0),
                new TimeSpan(17, 0, 0),
                rideOperator1,
                rollerCoasterAttraction,
                manager);

            shift.Delete();

            Assert.That(rideOperator1.Shifts, Does.Not.Contain(shift));
            Assert.That(rollerCoasterAttraction.Shifts, Does.Not.Contain(shift));
            Assert.That(manager.ShiftsAssigned, Does.Not.Contain(shift));
            Assert.That(Shift.Extent, Does.Not.Contain(shift));
        }

        [Test]
        public void TestShiftConstructorNullEmployeeThrows()
        {
            Assert.Throws<NullReferenceException>(() =>
                new Shift(
                    testDate,
                    new TimeSpan(9, 0, 0),
                    new TimeSpan(17, 0, 0),
                    null!,
                    rollerCoasterAttraction,
                    manager));
        }

        [Test]
        public void TestShiftConstructorNullAttractionThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Shift(
                    testDate,
                    new TimeSpan(9, 0, 0),
                    new TimeSpan(17, 0, 0),
                    rideOperator1,
                    null!,
                    manager));
        }

        [Test]
        public void TestShiftConstructorNullManagerThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Shift(
                    testDate,
                    new TimeSpan(9, 0, 0),
                    new TimeSpan(17, 0, 0),
                    rideOperator1,
                    rollerCoasterAttraction,
                    null!));
        }

        [Test]
        public void TestEmployeeMultipleShiftsDifferentTimes()
        {
            new Shift(
                testDate,
                new TimeSpan(9, 0, 0),
                new TimeSpan(13, 0, 0),
                rideOperator1,
                rollerCoasterAttraction,
                manager);

            new Shift(
                testDate,
                new TimeSpan(14, 0, 0),
                new TimeSpan(18, 0, 0),
                rideOperator1,
                rollerCoasterAttraction,
                manager);

            Assert.That(rideOperator1.Shifts.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestEmployeeMultipleAttractionsSameDay()
        {
            var shift1 = new Shift(
                testDate,
                new TimeSpan(9, 0, 0),
                new TimeSpan(13, 0, 0),
                rideOperator1,
                rollerCoasterAttraction,
                manager);

            var shift2 = new Shift(
                testDate,
                new TimeSpan(14, 0, 0),
                new TimeSpan(18, 0, 0),
                rideOperator1,
                waterRideAttraction,
                manager);

            Assert.That(rideOperator1.Shifts.Count, Is.EqualTo(2));
            Assert.That(rollerCoasterAttraction.Shifts, Contains.Item(shift1));
            Assert.That(waterRideAttraction.Shifts, Contains.Item(shift2));
        }

        [Test]
        public void TestAttractionMultipleEmployees()
        {
            new Shift(
                testDate,
                new TimeSpan(9, 0, 0),
                new TimeSpan(13, 0, 0),
                rideOperator1,
                rollerCoasterAttraction,
                manager);

            new Shift(
                testDate,
                new TimeSpan(14, 0, 0),
                new TimeSpan(18, 0, 0),
                rideOperator2,
                rollerCoasterAttraction,
                manager);

            Assert.That(rollerCoasterAttraction.Shifts.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestEmployeeShiftsCollectionIsReadOnly()
        {
            new Shift(
                testDate,
                new TimeSpan(9, 0, 0),
                new TimeSpan(17, 0, 0),
                rideOperator1,
                rollerCoasterAttraction,
                manager);

            Assert.That(rideOperator1.Shifts, Is.InstanceOf<IReadOnlyCollection<Shift>>());
        }

        [Test]
        public void TestAttractionShiftsCollectionIsReadOnly()
        {
            new Shift(
                testDate,
                new TimeSpan(9, 0, 0),
                new TimeSpan(17, 0, 0),
                rideOperator1,
                rollerCoasterAttraction,
                manager);

            Assert.That(rollerCoasterAttraction.Shifts, Is.InstanceOf<IReadOnlyCollection<Shift>>());
        }
    }
}
