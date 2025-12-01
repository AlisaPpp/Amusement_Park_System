using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class MaintenanceStaffTests
    {
        private readonly MaintenanceStaff maintenanceStaff = new MaintenanceStaff(
            "John", "Smith", "john.smith@park.com", new DateTime(1985, 5, 15), 8, "Electrical Systems", 
            new List<string> { "Electrical Safety", "Ride Maintenance" });

        // Name Tests
        [Test]
        public void TestMaintenanceStaffName()
        {
            Assert.That(maintenanceStaff.Name, Is.EqualTo("John"));
        }

        [Test]
        public void TestMaintenanceStaffEmptyNameException()
        {
            Assert.Throws<ArgumentException>(() => new MaintenanceStaff(
                "", "Smith", "test@test.com", new DateTime(1985, 5, 15), 8, "Specialization", new List<string>()));
        }

        [Test]
        public void TestMaintenanceStaffNullNameException()
        {
            Assert.Throws<ArgumentException>(() => new MaintenanceStaff(
                null, "Smith", "test@test.com", new DateTime(1985, 5, 15), 8, "Specialization", new List<string>()));
        }

        [Test]
        public void TestMaintenanceStaffNameSetterEmptyException()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            Assert.Throws<ArgumentException>(() => staff.Name = "");
        }

        [Test]
        public void TestMaintenanceStaffNameSetterNullException()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            Assert.Throws<ArgumentException>(() => staff.Name = null);
        }

        [Test]
        public void TestMaintenanceStaffNameSetter()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            staff.Name = "NewName";
            Assert.That(staff.Name, Is.EqualTo("NewName"));
        }

        // Surname Tests
        [Test]
        public void TestMaintenanceStaffSurname()
        {
            Assert.That(maintenanceStaff.Surname, Is.EqualTo("Smith"));
        }

        [Test]
        public void TestMaintenanceStaffEmptySurnameException()
        {
            Assert.Throws<ArgumentException>(() => new MaintenanceStaff(
                "John", "", "test@test.com", new DateTime(1985, 5, 15), 8, "Specialization", new List<string>()));
        }

        [Test]
        public void TestMaintenanceStaffNullSurnameException()
        {
            Assert.Throws<ArgumentException>(() => new MaintenanceStaff(
                "John", null, "test@test.com", new DateTime(1985, 5, 15), 8, "Specialization", new List<string>()));
        }

        [Test]
        public void TestMaintenanceStaffSurnameSetterEmptyException()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            Assert.Throws<ArgumentException>(() => staff.Surname = "");
        }

        [Test]
        public void TestMaintenanceStaffSurnameSetterNullException()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            Assert.Throws<ArgumentException>(() => staff.Surname = null);
        }

        [Test]
        public void TestMaintenanceStaffSurnameSetter()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            staff.Surname = "NewSurname";
            Assert.That(staff.Surname, Is.EqualTo("NewSurname"));
        }

        // ContactInfo Tests
        [Test]
        public void TestMaintenanceStaffContactInfo()
        {
            Assert.That(maintenanceStaff.ContactInfo, Is.EqualTo("john.smith@park.com"));
        }

        [Test]
        public void TestMaintenanceStaffEmptyContactInfoException()
        {
            Assert.Throws<ArgumentException>(() => new MaintenanceStaff(
                "John", "Smith", "", new DateTime(1985, 5, 15), 8, "Specialization", new List<string>()));
        }

        [Test]
        public void TestMaintenanceStaffNullContactInfoException()
        {
            Assert.Throws<ArgumentException>(() => new MaintenanceStaff(
                "John", "Smith", null, new DateTime(1985, 5, 15), 8, "Specialization", new List<string>()));
        }

        [Test]
        public void TestMaintenanceStaffContactInfoSetterEmptyException()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            Assert.Throws<ArgumentException>(() => staff.ContactInfo = "");
        }

        [Test]
        public void TestMaintenanceStaffContactInfoSetterNullException()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            Assert.Throws<ArgumentException>(() => staff.ContactInfo = null);
        }

        [Test]
        public void TestMaintenanceStaffContactInfoSetter()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            staff.ContactInfo = "new@email.com";
            Assert.That(staff.ContactInfo, Is.EqualTo("new@email.com"));
        }

        // BirthDate Tests
        [Test]
        public void TestMaintenanceStaffBirthDate()
        {
            Assert.That(maintenanceStaff.BirthDate, Is.EqualTo(new DateTime(1985, 5, 15)));
        }

        [Test]
        public void TestMaintenanceStaffFutureBirthDateException()
        {
            Assert.Throws<ArgumentException>(() => new MaintenanceStaff(
                "John", "Smith", "test@test.com", DateTime.Now.AddDays(1), 8, "Specialization", new List<string>()));
        }

        [Test]
        public void TestMaintenanceStaffUnderageBirthDateException()
        {
            Assert.Throws<ArgumentException>(() => new MaintenanceStaff(
                "John", "Smith", "test@test.com", DateTime.Now.AddYears(-17), 8, "Specialization", new List<string>()));
        }

        [Test]
        public void TestMaintenanceStaffBirthDateSetterFutureException()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            Assert.Throws<ArgumentException>(() => staff.BirthDate = DateTime.Now.AddDays(1));
        }

        [Test]
        public void TestMaintenanceStaffBirthDateSetterUnderageException()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            Assert.Throws<ArgumentException>(() => staff.BirthDate = DateTime.Now.AddYears(-17));
        }

        [Test]
        public void TestMaintenanceStaffBirthDateSetter()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            staff.BirthDate = new DateTime(1985, 5, 15);
            Assert.That(staff.BirthDate, Is.EqualTo(new DateTime(1985, 5, 15)));
        }

        // YearsOfExperience Tests
        [Test]
        public void TestMaintenanceStaffYearsOfExperience()
        {
            Assert.That(maintenanceStaff.YearsOfExperience, Is.EqualTo(8));
        }

        [Test]
        public void TestMaintenanceStaffNegativeYearsOfExperienceException()
        {
            Assert.Throws<ArgumentException>(() => new MaintenanceStaff(
                "John", "Smith", "test@test.com", new DateTime(1985, 5, 15), -1, "Specialization", new List<string>()));
        }

        [Test]
        public void TestMaintenanceStaffYearsOfExperienceSetterNegativeException()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            Assert.Throws<ArgumentException>(() => staff.YearsOfExperience = -1);
        }

        [Test]
        public void TestMaintenanceStaffYearsOfExperienceSetter()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            staff.YearsOfExperience = 10;
            Assert.That(staff.YearsOfExperience, Is.EqualTo(10));
        }

        // Specialization Tests
        [Test]
        public void TestMaintenanceStaffSpecialization()
        {
            Assert.That(maintenanceStaff.Specialization, Is.EqualTo("Electrical Systems"));
        }

        [Test]
        public void TestMaintenanceStaffEmptySpecializationException()
        {
            Assert.Throws<ArgumentException>(() => new MaintenanceStaff(
                "John", "Smith", "test@test.com", new DateTime(1985, 5, 15), 8, "", new List<string>()));
        }

        [Test]
        public void TestMaintenanceStaffNullSpecializationException()
        {
            Assert.Throws<ArgumentException>(() => new MaintenanceStaff(
                "John", "Smith", "test@test.com", new DateTime(1985, 5, 15), 8, null, new List<string>()));
        }

        [Test]
        public void TestMaintenanceStaffSpecializationSetterEmptyException()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            Assert.Throws<ArgumentException>(() => staff.Specialization = "");
        }

        [Test]
        public void TestMaintenanceStaffSpecializationSetterNullException()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            Assert.Throws<ArgumentException>(() => staff.Specialization = null);
        }

        [Test]
        public void TestMaintenanceStaffSpecializationSetter()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            staff.Specialization = "Hydraulic Systems";
            Assert.That(staff.Specialization, Is.EqualTo("Hydraulic Systems"));
        }

        // Certifications Tests
        [Test]
        public void TestMaintenanceStaffCertifications()
        {
            Assert.That(maintenanceStaff.Certifications, Is.EqualTo(new List<string> { "Electrical Safety", "Ride Maintenance" }));
        }

        [Test]
        public void TestMaintenanceStaffCertificationsSetter()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            var newCerts = new List<string> { "Advanced Repair" };
            staff.Certifications = newCerts;
            Assert.That(staff.Certifications, Is.EqualTo(newCerts));
        }

        [Test]
        public void TestMaintenanceStaffCertificationsCanBeNull()
        {
            var staff = new MaintenanceStaff("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 5, "Mechanical", new List<string>());
            staff.Certifications = null;
            Assert.That(staff.Certifications, Is.Null);
        }
    }
}