using Amusement_Park_System;
using Amusement_Park_System.Models;


namespace Amusement_Park_System_Tests
{
    public class ManagerShiftAssociationTests
    {
        private Manager _manager;
        private Manager _otherManager;
        private RideOperator _operator1;
        private RideOperator _operator2;
        private MaintenanceStaff _staff1;
        private MaintenanceStaff _staff2;
        private Attraction _attraction;

        [SetUp]
        public void Setup()
        {
            Manager.ClearExtent();
            RideOperator.ClearExtent();
            MaintenanceStaff.ClearExtent();
            Shift.ClearExtent();
            RollerCoaster.ClearExtent();

            _manager = new Manager("John", "Doe", "123", new DateTime(1980, 1, 1), 5);
            _otherManager = new Manager("Tom", "White", "000", new DateTime(1975, 5, 5), 10);

            _operator1 = new RideOperator("Alice", "Smith", "456", new DateTime(1995, 2, 2), 2, "LIC123", true);
            _operator2 = new RideOperator("Mary", "Jones", "457", new DateTime(1996, 2, 3), 3, "LIC124", false);

            _staff1 = new MaintenanceStaff("Bob", "Brown", "789", new DateTime(1990, 3, 3), 4, "Electric", null);
            _staff2 = new MaintenanceStaff("Jane", "Black", "790", new DateTime(1991, 4, 4), 5, "Mechanical", null);

            _attraction = new RollerCoaster("Coaster", 120, 20, true, 500, 80, 2);
        }


        [Test]
        public void AddManagedEmployee_AssignsEmployee_WhenValid()
        {
            _manager.AddManagedEmployee(_operator1);

            Assert.That(_manager.EmployeesManaged, Contains.Item(_operator1));
            Assert.That(_operator1.Manager, Is.EqualTo(_manager));
        }

        [Test]
        public void AddManagedEmployee_Throws_WhenEmployeeIsManager()
        {
            Assert.Throws<InvalidOperationException>(() => _manager.AddManagedEmployee(_otherManager));
        }

        [Test]
        public void AddManagedEmployee_Throws_WhenDifferentEmployeeType()
        {
            _manager.AddManagedEmployee(_operator1); 
            Assert.Throws<InvalidOperationException>(() => _manager.AddManagedEmployee(_staff1)); 
        }

        [Test]
        public void AddManagedEmployee_Throws_WhenEmployeeHasAnotherManager()
        {
            _otherManager.AddManagedEmployee(_operator1);
            Assert.Throws<InvalidOperationException>(() => _manager.AddManagedEmployee(_operator1));
        }

        [Test]
        public void RemoveManagedEmployee_RemovesEmployeeCorrectly()
        {
            _manager.AddManagedEmployee(_operator1);
            _manager.RemoveManagedEmployee(_operator1);

            Assert.That(_manager.EmployeesManaged, Does.Not.Contain(_operator1));
            Assert.That(_operator1.Manager, Is.Null);
        }

        [Test]
        public void RemoveManagedEmployee_DoesNothing_WhenEmployeeNotManaged()
        {
            Assert.DoesNotThrow(() => _manager.RemoveManagedEmployee(_operator1));
        }

        [Test]
        public void ManagesEmployee_ReturnsTrue_WhenEmployeeManaged()
        {
            _manager.AddManagedEmployee(_operator1);
            Assert.IsTrue(_manager.ManagesEmployee(_operator1));
        }

        [Test]
        public void ManagesEmployee_ReturnsFalse_WhenEmployeeNotManaged()
        {
            Assert.IsFalse(_manager.ManagesEmployee(_operator1));
        }
        
        [Test]
        public void ShiftConstructor_AssignsManager_WhenManagerManagesEmployee()
        {
            _manager.AddManagedEmployee(_operator1);
            var shift = new Shift(DateTime.Today, TimeSpan.FromHours(9), TimeSpan.FromHours(12), _operator1, _attraction, _manager);

            Assert.That(shift.Manager, Is.EqualTo(_manager));
            Assert.That(_manager.ShiftsAssigned, Contains.Item(shift));
        }

        [Test]
        public void ShiftConstructor_Throws_WhenManagerDoesNotManageEmployee()
        {
            _manager.AddManagedEmployee(_staff1); 
            Assert.Throws<InvalidOperationException>(() =>
                new Shift(DateTime.Today, TimeSpan.FromHours(9), TimeSpan.FromHours(12), _operator1, _attraction, _manager));
        }

        [Test]
        public void ShiftConstructor_Throws_OnNullArguments()
        {
            _manager.AddManagedEmployee(_operator1);
            Assert.Throws<ArgumentNullException>(() => new Shift(DateTime.Today, TimeSpan.FromHours(9), TimeSpan.FromHours(12), null!, _attraction, _manager));
            Assert.Throws<ArgumentNullException>(() => new Shift(DateTime.Today, TimeSpan.FromHours(9), TimeSpan.FromHours(12), _operator1, null!, _manager));
            Assert.Throws<ArgumentNullException>(() => new Shift(DateTime.Today, TimeSpan.FromHours(9), TimeSpan.FromHours(12), _operator1, _attraction, null!));
        }

        [Test]
        public void DeleteShift_RemovesShiftFromManager()
        {
            _manager.AddManagedEmployee(_operator1);
            var shift = new Shift(DateTime.Today, TimeSpan.FromHours(9), TimeSpan.FromHours(12), _operator1, _attraction, _manager);

            shift.Delete();

            Assert.That(_manager.ShiftsAssigned, Does.Not.Contain(shift));
            Assert.That(_manager.ShiftsAssigned, Does.Not.Contain(shift));
            Assert.That(_manager.ShiftsAssigned, Does.Not.Contain(shift));
        }

        [Test]
        public void MultipleShifts_CanBeAssignedToManager()
        {
            _manager.AddManagedEmployee(_operator1);
            var shift1 = new Shift(DateTime.Today, TimeSpan.FromHours(9), TimeSpan.FromHours(12), _operator1, _attraction, _manager);
            var shift2 = new Shift(DateTime.Today, TimeSpan.FromHours(13), TimeSpan.FromHours(16), _operator1, _attraction, _manager);

            Assert.That(_manager.ShiftsAssigned.Count, Is.EqualTo(2));
            Assert.That(_manager.ShiftsAssigned, Contains.Item(shift1));
            Assert.That(_manager.ShiftsAssigned, Contains.Item(shift2));
        }

        [Test]
        public void ShiftAssignment_DoesNotDuplicateShifts()
        {
            _manager.AddManagedEmployee(_operator1);
            var shift = new Shift(DateTime.Today, TimeSpan.FromHours(9), TimeSpan.FromHours(12), _operator1, _attraction, _manager);
            Assert.That(_manager.ShiftsAssigned.Count, Is.EqualTo(1));
        }

        [Test]
        public void ManagerWithNoShifts_HasEmptyCollection()
        {
            Assert.That(_manager.ShiftsAssigned.Count, Is.EqualTo(0));
        }
    }
}
