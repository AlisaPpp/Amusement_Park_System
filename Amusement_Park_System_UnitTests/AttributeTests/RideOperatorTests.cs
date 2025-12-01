using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class RideOperatorTests
    {
        private readonly RideOperator rideOperator = new RideOperator(
            "Mike", "Johnson", "mike.johnson@park.com", new DateTime(1990, 3, 20), 5, "OP12345", true);

        // OperatorLicenceId Tests
        [Test]
        public void TestRideOperatorOperatorLicenceId()
        {
            Assert.That(rideOperator.OperatorLicenceId, Is.EqualTo("OP12345"));
        }

        [Test]
        public void TestRideOperatorEmptyOperatorLicenceIdException()
        {
            Assert.Throws<ArgumentException>(() => new RideOperator(
                "Mike", "Johnson", "test@test.com", new DateTime(1990, 3, 20), 5, "", true));
        }

        [Test]
        public void TestRideOperatorNullOperatorLicenceIdException()
        {
            Assert.Throws<ArgumentException>(() => new RideOperator(
                "Mike", "Johnson", "test@test.com", new DateTime(1990, 3, 20), 5, null, true));
        }

        [Test]
        public void TestRideOperatorOperatorLicenceIdSetterEmptyException()
        {
            var op = new RideOperator("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 3, "LIC123", false);
            Assert.Throws<ArgumentException>(() => op.OperatorLicenceId = "");
        }

        [Test]
        public void TestRideOperatorOperatorLicenceIdSetterNullException()
        {
            var op = new RideOperator("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 3, "LIC123", false);
            Assert.Throws<ArgumentException>(() => op.OperatorLicenceId = null);
        }

        [Test]
        public void TestRideOperatorOperatorLicenceIdSetter()
        {
            var op = new RideOperator("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 3, "LIC123", false);
            op.OperatorLicenceId = "NEW456";
            Assert.That(op.OperatorLicenceId, Is.EqualTo("NEW456"));
        }

        // IsFirstAidCertified Tests
        [Test]
        public void TestRideOperatorIsFirstAidCertified()
        {
            Assert.That(rideOperator.IsFirstAidCertified, Is.True);
        }

        [Test]
        public void TestRideOperatorIsFirstAidCertifiedSetter()
        {
            var op = new RideOperator("Test", "User", "test@test.com", new DateTime(1990, 1, 1), 3, "LIC123", false);
            op.IsFirstAidCertified = true;
            Assert.That(op.IsFirstAidCertified, Is.True);
        }
    }
}