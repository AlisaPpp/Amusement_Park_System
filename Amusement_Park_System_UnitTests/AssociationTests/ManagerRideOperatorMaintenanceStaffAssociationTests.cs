
using Amusement_Park_System.Models;


namespace Amusement_Park_System_Tests
{
    public class ManagerRideOperatorMaintenanceStaffAssociationTests
    {
        private Manager _manager;
        private RideOperator _rideOp1;
        private RideOperator _rideOp2;
        private MaintenanceStaff _staff1;
        private MaintenanceStaff _staff2;

        [SetUp]
        public void Setup()
        {
            Manager.ClearExtent();
            RideOperator.ClearExtent();
            MaintenanceStaff.ClearExtent();

            _manager = new Manager("John", "Doe", "123", new DateTime(1980, 1, 1), 5);
            _rideOp1 = new RideOperator("Alice", "Smith", "456", new DateTime(1995, 2, 2), 2, "LIC123", true);
            _rideOp2 = new RideOperator("Eve", "Jones", "789", new DateTime(1996, 3, 3), 1, "LIC456", false);
            _staff1 = new MaintenanceStaff("Bob", "Brown", "789", new DateTime(1990, 3, 3), 4, "Electrical", null);
            _staff2 = new MaintenanceStaff("Carol", "Green", "321", new DateTime(1992, 4, 4), 3, "Mechanical", null);
        }

        [Test]
        public void AddManagedEmployee_AllowsFirstEmployee()
        {
            _manager.AddManagedEmployee(_rideOp1);
            Assert.That(_manager.EmployeesManaged, Contains.Item(_rideOp1));
            Assert.That(_rideOp1.Manager, Is.EqualTo(_manager));
        }

        [Test]
        public void AddManagedEmployee_Throws_WhenNull()
        {
            Assert.Throws<ArgumentNullException>(() => _manager.AddManagedEmployee(null!));
        }

        [Test]
        public void AddManagedEmployee_Throws_WhenAddingManager()
        {
            var otherManager = new Manager("Tom", "White", "000", new DateTime(1975, 5, 5), 10);
            Assert.Throws<InvalidOperationException>(() => _manager.AddManagedEmployee(otherManager));
        }

        [Test]
        public void AddManagedEmployee_Throws_WhenAddingDifferentTypeAfterRideOperator()
        {
            _manager.AddManagedEmployee(_rideOp1);
            Assert.Throws<InvalidOperationException>(() => _manager.AddManagedEmployee(_staff1));
        }

        [Test]
        public void AddManagedEmployee_Throws_WhenAddingDifferentTypeAfterMaintenanceStaff()
        {
            _manager.AddManagedEmployee(_staff1);
            Assert.Throws<InvalidOperationException>(() => _manager.AddManagedEmployee(_rideOp1));
        }

        [Test]
        public void AddManagedEmployee_Throws_WhenEmployeeHasAnotherManager()
        {
            var otherManager = new Manager("Tom", "White", "000", new DateTime(1975, 5, 5), 10);
            otherManager.AddManagedEmployee(_rideOp1);
            Assert.Throws<InvalidOperationException>(() => _manager.AddManagedEmployee(_rideOp1));
        }

        [Test]
        public void AddManagedEmployee_AllowsMultipleSameTypeEmployees()
        {
            _manager.AddManagedEmployee(_rideOp1);
            _manager.AddManagedEmployee(_rideOp2);

            Assert.That(_manager.EmployeesManaged.Count, Is.EqualTo(2));
            Assert.That(_manager.EmployeesManaged, Contains.Item(_rideOp1));
            Assert.That(_manager.EmployeesManaged, Contains.Item(_rideOp2));
        }

        [Test]
        public void AddManagedEmployee_SameEmployeeTwice_IsIgnored()
        {
            _manager.AddManagedEmployee(_rideOp1);
            _manager.AddManagedEmployee(_rideOp1); 

            Assert.That(_manager.EmployeesManaged.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemoveManagedEmployee_RemovesEmployee()
        {
            _manager.AddManagedEmployee(_rideOp1);
            _manager.RemoveManagedEmployee(_rideOp1);

            Assert.That(_manager.EmployeesManaged, Does.Not.Contain(_rideOp1));
            Assert.That(_rideOp1.Manager, Is.Null);
        }

        [Test]
        public void RemoveManagedEmployee_Null_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _manager.RemoveManagedEmployee(null!));
        }

        [Test]
        public void RemoveManagedEmployee_NotManagedEmployee_DoesNothing()
        {
            _manager.AddManagedEmployee(_rideOp1);
            Assert.DoesNotThrow(() => _manager.RemoveManagedEmployee(_rideOp2));
        }

        [Test]
        public void ManagesEmployee_ReturnsTrue_WhenManaged()
        {
            _manager.AddManagedEmployee(_rideOp1);
            Assert.IsTrue(_manager.ManagesEmployee(_rideOp1));
        }

        [Test]
        public void ManagesEmployee_ReturnsFalse_WhenNotManaged()
        {
            Assert.IsFalse(_manager.ManagesEmployee(_rideOp1));
        }
    }
}
