using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class ManagerTests
    {
        private readonly Manager manager = new Manager(
            "Sarah", "Wilson", "sarah.wilson@park.com", new DateTime(1980, 8, 10), 8);

        // MinYearsOfExperience Tests 
        [Test]
        public void TestManagerMinYearsOfExperience()
        {
            Assert.That(Manager.MinYearsOfExperience, Is.EqualTo(3));
        }

        // YearsOfExperience Validation Tests
        [Test]
        public void TestManagerInsufficientExperienceException()
        {
            Assert.Throws<ArgumentException>(() => new Manager(
                "Test", "Manager", "test@test.com", new DateTime(1990, 1, 1), 2));
        }

        [Test]
        public void TestManagerExactMinimumExperience()
        {
            var minExpManager = new Manager(
                "Test", "Manager", "test@test.com", new DateTime(1990, 1, 1), 3);
            Assert.That(minExpManager.YearsOfExperience, Is.EqualTo(3));
        }

        [Test]
        public void TestManagerSufficientExperience()
        {
            var sufficientExpManager = new Manager(
                "Test", "Manager", "test@test.com", new DateTime(1990, 1, 1), 5);
            Assert.That(sufficientExpManager.YearsOfExperience, Is.EqualTo(5));
        }
    }
}