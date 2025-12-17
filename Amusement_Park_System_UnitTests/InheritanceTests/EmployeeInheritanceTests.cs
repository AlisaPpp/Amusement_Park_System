
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class EmployeeInheritanceTests
    {
        private RideOperator _rideOperator;
        private MaintenanceStaff _maintenanceStaff;
        private Manager _manager;

        [SetUp]
        public void Setup()
        {
            RideOperator.ClearExtent();
            MaintenanceStaff.ClearExtent();
            Manager.ClearExtent();

            _rideOperator = new RideOperator(
                "John",
                "Doe",
                "123456",
                DateTime.Now.AddYears(-25),
                3,
                "LIC-123",
                true
            );

            _maintenanceStaff = new MaintenanceStaff(
                "Anna",
                "Smith",
                "987654",
                DateTime.Now.AddYears(-30),
                5,
                "Electrical",
                new() { "ISO-9001" }
            );

            _manager = new Manager(
                "Mike",
                "Brown",
                "555555",
                DateTime.Now.AddYears(-40),
                15
            );
        }
        

        [Test]
        public void Employee_Is_Abstract()
        {
            Assert.That(typeof(Employee).IsAbstract, Is.True);
        }
        

        [Test]
        public void RideOperator_Is_Employee()
        {
            Assert.That(_rideOperator, Is.InstanceOf<Employee>());
        }

        [Test]
        public void MaintenanceStaff_Is_Employee()
        {
            Assert.That(_maintenanceStaff, Is.InstanceOf<Employee>());
        }

        [Test]
        public void Manager_Is_Employee()
        {
            Assert.That(_manager, Is.InstanceOf<Employee>());
        }
        

        [Test]
        public void RideOperator_Is_Not_MaintenanceStaff_Or_Manager()
        {
            Assert.That(_rideOperator, Is.Not.InstanceOf<MaintenanceStaff>());
            Assert.That(_rideOperator, Is.Not.InstanceOf<Manager>());
        }

        [Test]
        public void MaintenanceStaff_Is_Not_RideOperator_Or_Manager()
        {
            Assert.That(_maintenanceStaff, Is.Not.InstanceOf<RideOperator>());
            Assert.That(_maintenanceStaff, Is.Not.InstanceOf<Manager>());
        }

        [Test]
        public void Manager_Is_Not_RideOperator_Or_MaintenanceStaff()
        {
            Assert.That(_manager, Is.Not.InstanceOf<RideOperator>());
            Assert.That(_manager, Is.Not.InstanceOf<MaintenanceStaff>());
        }
        

        [Test]
        public void All_Employees_Belong_To_Exactly_One_Subclass()
        {
            Employee[] employees =
            {
                _rideOperator,
                _maintenanceStaff,
                _manager
            };

            foreach (var e in employees)
            {
                int subclassCount = 0;

                if (e is RideOperator) subclassCount++;
                if (e is MaintenanceStaff) subclassCount++;
                if (e is Manager) subclassCount++;

                Assert.That(subclassCount, Is.EqualTo(1));
            }
        }
    }
}
