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
            
            rideOperator = new RideOperator(
                "John", "Doe", "john@example.com", 
                new DateTime(1990, 1, 1), 3, "OP12345", true);
            
            manager = new Manager(
                "Jane", "Smith", "jane@example.com", 
                new DateTime(1985, 5, 15), 5);
            
            maintenanceStaff = new MaintenanceStaff(
                "Bob", "Brown", "bob@example.com", 
                new DateTime(1988, 8, 20), 4, "Electrical",
                new List<string> { "Cert1", "Cert2" });
            
            rollerCoaster = new RollerCoaster(
                "Thunderbolt", 140, 24, true, 1200.5, 85.5, 3);
            
            waterRide = new WaterRide(
                "Splash Mountain", 120, 20, true, 2.5, 25.0);
            
            extremeAttraction = new ExtremeAttraction(
                "Extreme Drop", 150, 16, false,
                new List<string> { "No heart conditions", "Not for pregnant women" });
            
            fourDRide = new FourDRide(
                "4D Adventure", 100, 30, true, 15.5,
                new List<string> { "3D Glasses", "Motion Seats", "Water Spray" });
            
            mediumAttraction = new MediumAttraction(
                "Family Coaster", 120, 20, true, true);
            
            lightAttraction = new LightAttraction(
                "Kiddie Ride", 100, 15, true, true);
        }

        // Employee AssignShift
        [Test]
        public void TestEmployeeAssignShiftCreatesAssociation()
        {
            var shift = rideOperator.AssignShift(
                rollerCoaster,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            Assert.That(rideOperator.Shifts, Contains.Item(shift));
            Assert.That(rollerCoaster.Shifts, Contains.Item(shift));
            Assert.That(shift.Employee, Is.EqualTo(rideOperator));
            Assert.That(shift.Attraction, Is.EqualTo(rollerCoaster));
        }

        [Test]
        public void TestManagerAssignShiftCreatesAssociation()
        {
            var shift = manager.AssignShift(
                waterRide,
                testDate,
                new DateTime(2024, 1, 15, 10, 0, 0),
                new DateTime(2024, 1, 15, 18, 0, 0));
            
            Assert.That(manager.Shifts, Contains.Item(shift));
            Assert.That(waterRide.Shifts, Contains.Item(shift));
            Assert.That(shift.Employee, Is.EqualTo(manager));
            Assert.That(shift.Attraction, Is.EqualTo(waterRide));
        }

        [Test]
        public void TestMaintenanceStaffAssignShiftCreatesAssociation()
        {
            var shift = maintenanceStaff.AssignShift(
                extremeAttraction,
                testDate,
                new DateTime(2024, 1, 15, 8, 0, 0),
                new DateTime(2024, 1, 15, 16, 0, 0));
            
            Assert.That(maintenanceStaff.Shifts, Contains.Item(shift));
            Assert.That(extremeAttraction.Shifts, Contains.Item(shift));
            Assert.That(shift.Employee, Is.EqualTo(maintenanceStaff));
            Assert.That(shift.Attraction, Is.EqualTo(extremeAttraction));
        }

        // Attraction AssignShift
        [Test]
        public void TestAttractionAssignShiftCreatesAssociation()
        {
            var shift = fourDRide.AssignShift(
                rideOperator,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            Assert.That(fourDRide.Shifts, Contains.Item(shift));
            Assert.That(rideOperator.Shifts, Contains.Item(shift));
            Assert.That(shift.Attraction, Is.EqualTo(fourDRide));
            Assert.That(shift.Employee, Is.EqualTo(rideOperator));
        }

        [Test]
        public void TestMediumAttractionAssignShiftCreatesAssociation()
        {
            var shift = mediumAttraction.AssignShift(
                manager,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            Assert.That(mediumAttraction.Shifts, Contains.Item(shift));
            Assert.That(manager.Shifts, Contains.Item(shift));
            Assert.That(shift.Attraction, Is.EqualTo(mediumAttraction));
            Assert.That(shift.Employee, Is.EqualTo(manager));
        }

        [Test]
        public void TestLightAttractionAssignShiftCreatesAssociation()
        {
            var shift = lightAttraction.AssignShift(
                rideOperator,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            Assert.That(lightAttraction.Shifts, Contains.Item(shift));
            Assert.That(rideOperator.Shifts, Contains.Item(shift));
            Assert.That(shift.Attraction, Is.EqualTo(lightAttraction));
            Assert.That(shift.Employee, Is.EqualTo(rideOperator));
        }

        // 3. All connection tests
        [Test]
        public void TestEmployeeAssignShiftUpdatesAttractionCollection()
        {
            var shift = rideOperator.AssignShift(
                rollerCoaster,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            Assert.That(rollerCoaster.Shifts.Count, Is.EqualTo(1));
            Assert.That(rollerCoaster.Shifts, Contains.Item(shift));
        }

        [Test]
        public void TestAttractionAssignShiftUpdatesEmployeeCollection()
        {
            var shift = waterRide.AssignShift(
                maintenanceStaff,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            Assert.That(maintenanceStaff.Shifts.Count, Is.EqualTo(1));
            Assert.That(maintenanceStaff.Shifts, Contains.Item(shift));
        }

        // 4. duplicate tests
        [Test]
        public void TestDuplicateShiftFromEmployeeThrowsException()
        {
            rideOperator.AssignShift(
                rollerCoaster,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            Assert.Throws<InvalidOperationException>(() => 
                rideOperator.AssignShift(
                    rollerCoaster,
                    testDate,
                    new DateTime(2024, 1, 15, 9, 0, 0),
                    new DateTime(2024, 1, 15, 17, 0, 0)));
        }

        [Test]
        public void TestDuplicateShiftFromAttractionThrowsException()
        {
            fourDRide.AssignShift(
                rideOperator,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            Assert.Throws<InvalidOperationException>(() => 
                fourDRide.AssignShift(
                    rideOperator,
                    testDate,
                    new DateTime(2024, 1, 15, 9, 0, 0),
                    new DateTime(2024, 1, 15, 17, 0, 0)));
        }

        // 5. null tests
        [Test]
        public void TestEmployeeAssignShiftNullAttractionThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => 
                rideOperator.AssignShift(
                    null,
                    testDate,
                    new DateTime(2024, 1, 15, 9, 0, 0),
                    new DateTime(2024, 1, 15, 17, 0, 0)));
        }

        [Test]
        public void TestAttractionAssignShiftNullEmployeeThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => 
                rollerCoaster.AssignShift(
                    null,
                    testDate,
                    new DateTime(2024, 1, 15, 9, 0, 0),
                    new DateTime(2024, 1, 15, 17, 0, 0)));
        }

        // 6. multiple associations
        [Test]
        public void TestEmployeeMultipleShiftsDifferentTimes()
        {
            var shift1 = rideOperator.AssignShift(
                rollerCoaster,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 13, 0, 0));
            
            var shift2 = rideOperator.AssignShift(
                rollerCoaster,
                testDate,
                new DateTime(2024, 1, 15, 14, 0, 0),
                new DateTime(2024, 1, 15, 18, 0, 0));
            
            Assert.That(rideOperator.Shifts.Count, Is.EqualTo(2));
            Assert.That(rideOperator.Shifts, Contains.Item(shift1));
            Assert.That(rideOperator.Shifts, Contains.Item(shift2));
        }

        [Test]
        public void TestEmployeeMultipleAttractions()
        {
            var shift1 = rideOperator.AssignShift(
                rollerCoaster,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 13, 0, 0));
            
            var shift2 = rideOperator.AssignShift(
                waterRide,
                testDate,
                new DateTime(2024, 1, 15, 14, 0, 0),
                new DateTime(2024, 1, 15, 18, 0, 0));
            
            Assert.That(rideOperator.Shifts.Count, Is.EqualTo(2));
            Assert.That(rollerCoaster.Shifts, Contains.Item(shift1));
            Assert.That(waterRide.Shifts, Contains.Item(shift2));
        }

        [Test]
        public void TestAttractionMultipleEmployees()
        {
            var shift1 = rollerCoaster.AssignShift(
                rideOperator,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 13, 0, 0));
            
            var shift2 = rollerCoaster.AssignShift(
                manager,
                testDate,
                new DateTime(2024, 1, 15, 14, 0, 0),
                new DateTime(2024, 1, 15, 18, 0, 0));
            
            Assert.That(rollerCoaster.Shifts.Count, Is.EqualTo(2));
            Assert.That(rideOperator.Shifts, Contains.Item(shift1));
            Assert.That(manager.Shifts, Contains.Item(shift2));
        }

        // 7. remove tests
        [Test]
        public void TestEmployeeRemoveShift()
        {
            var shift = rideOperator.AssignShift(
                rollerCoaster,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            rideOperator.RemoveShift(shift);
            
            Assert.That(rideOperator.Shifts, Does.Not.Contain(shift));
            Assert.That(rollerCoaster.Shifts, Does.Not.Contain(shift));
            Assert.That(shift.Employee, Is.Null);
            Assert.That(shift.Attraction, Is.Null);
        }

        [Test]
        public void TestAttractionRemoveShift()
        {
            var shift = rollerCoaster.AssignShift(
                rideOperator,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            rollerCoaster.RemoveShift(shift);
            
            Assert.That(rollerCoaster.Shifts, Does.Not.Contain(shift));
            Assert.That(rideOperator.Shifts, Does.Not.Contain(shift));
            Assert.That(shift.Employee, Is.Null);
            Assert.That(shift.Attraction, Is.Null);
        }

        [Test]
        public void TestRemoveShiftNotInEmployeeCollection()
        {
            var shift = rideOperator.AssignShift(
                rollerCoaster,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            Assert.DoesNotThrow(() => manager.RemoveShift(shift));
        }

        [Test]
        public void TestRemoveShiftNotInAttractionCollection()
        {
            var shift = rollerCoaster.AssignShift(
                rideOperator,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            Assert.DoesNotThrow(() => waterRide.RemoveShift(shift));
        }

        // 8. collection tests
        [Test]
        public void TestEmployeeShiftsCollectionIsReadOnly()
        {
            var shift = rideOperator.AssignShift(
                rollerCoaster,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            var employeeShifts = rideOperator.Shifts;
            Assert.That(employeeShifts, Is.InstanceOf<IReadOnlyCollection<Shift>>());
        }

        [Test]
        public void TestAttractionShiftsCollectionIsReadOnly()
        {
            var shift = rollerCoaster.AssignShift(
                rideOperator,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            var attractionShifts = rollerCoaster.Shifts;
            Assert.That(attractionShifts, Is.InstanceOf<IReadOnlyCollection<Shift>>());
        }
        

        // 10. complex tests
        [Test]
        public void TestEmployeeShiftRemovalClearsReverseConnections()
        {
            var shift = rideOperator.AssignShift(
                rollerCoaster,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            rideOperator.RemoveShift(shift);
            
            Assert.That(rideOperator.Shifts, Does.Not.Contain(shift));
            Assert.That(rollerCoaster.Shifts, Does.Not.Contain(shift));
            Assert.That(shift.Employee, Is.Null);
            Assert.That(shift.Attraction, Is.Null);
        }

        [Test]
        public void TestAttractionShiftRemovalClearsReverseConnections()
        {
            var shift = rollerCoaster.AssignShift(
                rideOperator,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 17, 0, 0));
            
            rollerCoaster.RemoveShift(shift);
            
            Assert.That(rollerCoaster.Shifts, Does.Not.Contain(shift));
            Assert.That(rideOperator.Shifts, Does.Not.Contain(shift));
            Assert.That(shift.Employee, Is.Null);
            Assert.That(shift.Attraction, Is.Null);
        }

        [Test]
        public void TestEmployeeCanWorkMultipleAttractionsSameDay()
        {
            var shift1 = rideOperator.AssignShift(
                rollerCoaster,
                testDate,
                new DateTime(2024, 1, 15, 9, 0, 0),
                new DateTime(2024, 1, 15, 13, 0, 0));
            
            var shift2 = rideOperator.AssignShift(
                waterRide,
                testDate,
                new DateTime(2024, 1, 15, 14, 0, 0),
                new DateTime(2024, 1, 15, 18, 0, 0));
            
            var shift3 = rideOperator.AssignShift(
                fourDRide,
                testDate,
                new DateTime(2024, 1, 15, 19, 0, 0),
                new DateTime(2024, 1, 15, 22, 0, 0));
            
            Assert.That(rideOperator.Shifts.Count, Is.EqualTo(3));
            Assert.That(rollerCoaster.Shifts, Contains.Item(shift1));
            Assert.That(waterRide.Shifts, Contains.Item(shift2));
            Assert.That(fourDRide.Shifts, Contains.Item(shift3));
        }
    }
}