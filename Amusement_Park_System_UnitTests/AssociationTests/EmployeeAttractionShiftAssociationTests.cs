using Amusement_Park_System;
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class EmployeeAttractionShiftAssociationTests
    {
        private RideOperator rideOperator;
        private Manager manager;
        private MaintenanceStaff maintenanceStaff;
        private RollerCoaster rollerCoaster;
        private WaterRide waterRide;
        private ExtremeAttraction extremeAttraction;
        private FourDRide fourDRide;
        private MediumAttraction mediumAttraction;
        private LightAttraction lightAttraction;
        private DateTime testDate;
        private Zone zone;

        [SetUp]
        public void Setup()
        {
            Shift.ClearExtent();
            RideOperator.ClearExtent();
            Manager.ClearExtent();
            MaintenanceStaff.ClearExtent();
            RollerCoaster.ClearExtent();
            WaterRide.ClearExtent();
            ExtremeAttraction.ClearExtent();
            FourDRide.ClearExtent();
            MediumAttraction.ClearExtent();
            LightAttraction.ClearExtent();

            testDate = new DateTime(2024, 1, 15);

            rideOperator = new RideOperator("John", "Doe", "john@example.com",
                new DateTime(1990, 1, 1), 3, "OP12345", true);

            manager = new Manager("Jane", "Smith", "jane@example.com",
                new DateTime(1985, 5, 15), 5);

            maintenanceStaff = new MaintenanceStaff("Bob", "Brown", "bob@example.com",
                new DateTime(1988, 8, 20), 4, "Electrical",
                new List<string> { "Cert1", "Cert2" });

            rollerCoaster = new RollerCoaster("Thunderbolt", 140, 24, 
                true, 1200.5, 85.5, 3);
            waterRide = new WaterRide("Splash Mountain", 120, 20, true, 2.5, 25.0);
            extremeAttraction = new ExtremeAttraction("Extreme Drop", 150, 16, false,
                new List<string> { "No heart conditions", "Not for pregnant women" });
            fourDRide = new FourDRide("4D Adventure", 100, 30, true, 15.5,
                new List<string> { "3D Glasses", "Motion Seats", "Water Spray" });
            mediumAttraction = new MediumAttraction("Family Coaster", 120, 20, true, true);
            lightAttraction = new LightAttraction("Kiddie Ride", 100, 15, true, true);

            manager.AddManagedEmployee(rideOperator);
        }


        [Test]
        public void TestShiftConstructorCreatesAssociation()
        {
            var shift = new Shift(testDate, 
                new TimeSpan(9,0,0), new TimeSpan(17,0,0),
                rideOperator, rollerCoaster, manager);

            Assert.That(rideOperator.Shifts, Contains.Item(shift));
            Assert.That(rollerCoaster.Shifts, Contains.Item(shift));
            Assert.That(manager.Shifts, Contains.Item(shift));

            Assert.That(shift.Employee, Is.EqualTo(rideOperator));
            Assert.That(shift.Attraction, Is.EqualTo(rollerCoaster));
            Assert.That(shift.Manager, Is.EqualTo(manager));
        }
        

        [Test]
        public void TestCreatingTwoShiftsSameParamsAllowed()
        {
            var shift1 = new Shift(testDate, new TimeSpan(9,0,0), new TimeSpan(17,0,0),
                rideOperator, rollerCoaster, manager);

            var shift2 = new Shift(testDate, new TimeSpan(9,0,0), new TimeSpan(17,0,0),
                rideOperator, rollerCoaster, manager);

            Assert.That(rideOperator.Shifts.Count, Is.EqualTo(2));
            Assert.That(rollerCoaster.Shifts.Count, Is.EqualTo(2));
            Assert.That(manager.Shifts.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestShiftDeleteRemovesAssociations()
        {
            var shift = new Shift(testDate, 
                new TimeSpan(9,0,0), new TimeSpan(17,0,0),
                rideOperator, rollerCoaster, manager);

            shift.Delete();

            Assert.That(rideOperator.Shifts, Does.Not.Contain(shift));
            Assert.That(rollerCoaster.Shifts, Does.Not.Contain(shift));
            Assert.That(manager.Shifts, Does.Not.Contain(shift));

            Assert.That(Shift.Extent, Does.Not.Contain(shift));
        }

        [Test]
        public void TestShiftConstructorNullEmployeeThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Shift(testDate, new TimeSpan(9,0,0), new TimeSpan(17,0,0),
                    null, rollerCoaster, manager));
        }

        [Test]
        public void TestShiftConstructorNullAttractionThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Shift(testDate, new TimeSpan(9,0,0), new TimeSpan(17,0,0),
                    rideOperator, null, manager));
        }

        [Test]
        public void TestEmployeeMultipleShiftsDifferentTimes()
        {
            var shift1 = new Shift(testDate, new TimeSpan(9,0,0), new TimeSpan(13,0,0),
                rideOperator, rollerCoaster, manager);

            var shift2 = new Shift(testDate, new TimeSpan(14,0,0), new TimeSpan(18,0,0),
                rideOperator, rollerCoaster, manager);

            Assert.That(rideOperator.Shifts.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestEmployeeMultipleAttractionsSameDay()
        {
            var shift1 = new Shift(testDate, new TimeSpan(9, 0, 0), new TimeSpan(13, 0, 0),
                rideOperator, rollerCoaster, manager);

            var shift2 = new Shift(testDate, new TimeSpan(14, 0, 0), new TimeSpan(18, 0, 0),
                rideOperator, waterRide, manager);

            Assert.That(rideOperator.Shifts.Count, Is.EqualTo(2));
            Assert.That(rollerCoaster.Shifts, Contains.Item(shift1));
            Assert.That(waterRide.Shifts, Contains.Item(shift2));
        }

        [Test]
        public void TestAttractionMultipleEmployees()
        {
            var shift1 = new Shift(testDate, new TimeSpan(9,0,0), new TimeSpan(13,0,0),
                rideOperator, rollerCoaster, manager);

            var shift2 = new Shift(testDate, new TimeSpan(14,0,0), new TimeSpan(18,0,0),
                maintenanceStaff, rollerCoaster, manager);

            Assert.That(rollerCoaster.Shifts.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestEmployeeShiftsCollectionIsReadOnly()
        {
            var shift = new Shift(testDate, new TimeSpan(9,0,0), new TimeSpan(17,0,0),
                rideOperator, rollerCoaster, manager);

            Assert.That(rideOperator.Shifts, Is.InstanceOf<IReadOnlyCollection<Shift>>());
        }

        [Test]
        public void TestAttractionShiftsCollectionIsReadOnly()
        {
            var shift = new Shift(testDate, new TimeSpan(9,0,0), new TimeSpan(17,0,0),
                rideOperator, rollerCoaster, manager);

            Assert.That(rollerCoaster.Shifts, Is.InstanceOf<IReadOnlyCollection<Shift>>());
        }
    }
}