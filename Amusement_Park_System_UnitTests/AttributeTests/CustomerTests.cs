using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class CustomerTests
    {
        private readonly Customer customer = new Customer("Ellie", "Williams", "ellie.williams@email.com");

        // Name Tests
        [Test]
        public void TestCustomerName()
        {
            Assert.That(customer.Name, Is.EqualTo("Ellie"));
        }

        [Test]
        public void TestCustomerEmptyNameException()
        {
            Assert.Throws<ArgumentException>(() => new Customer("", "Williams", "email@test.com"));
        }

        [Test]
        public void TestCustomerNullNameException()
        {
            Assert.Throws<ArgumentException>(() => new Customer(null, "Williams", "email@test.com"));
        }

        [Test]
        public void TestCustomerNameSetterEmptyException()
        {
            var testCustomer = new Customer("Test", "User", "test@test.com");
            Assert.Throws<ArgumentException>(() => testCustomer.Name = "");
        }

        [Test]
        public void TestCustomerNameSetterNullException()
        {
            var testCustomer = new Customer("Test", "User", "test@test.com");
            Assert.Throws<ArgumentException>(() => testCustomer.Name = null);
        }

        [Test]
        public void TestCustomerNameSetter()
        {
            var testCustomer = new Customer("Test", "User", "test@test.com");
            testCustomer.Name = "NewName";
            Assert.That(testCustomer.Name, Is.EqualTo("NewName"));
        }

        // Surname Tests
        [Test]
        public void TestCustomerSurname()
        {
            Assert.That(customer.Surname, Is.EqualTo("Williams"));
        }

        [Test]
        public void TestCustomerEmptySurnameException()
        {
            Assert.Throws<ArgumentException>(() => new Customer("Ellie", "", "email@test.com"));
        }

        [Test]
        public void TestCustomerNullSurnameException()
        {
            Assert.Throws<ArgumentException>(() => new Customer("Ellie", null, "email@test.com"));
        }

        [Test]
        public void TestCustomerSurnameSetterEmptyException()
        {
            var testCustomer = new Customer("Test", "User", "test@test.com");
            Assert.Throws<ArgumentException>(() => testCustomer.Surname = "");
        }

        [Test]
        public void TestCustomerSurnameSetterNullException()
        {
            var testCustomer = new Customer("Test", "User", "test@test.com");
            Assert.Throws<ArgumentException>(() => testCustomer.Surname = null);
        }

        [Test]
        public void TestCustomerSurnameSetter()
        {
            var testCustomer = new Customer("Test", "User", "test@test.com");
            testCustomer.Surname = "NewSurname";
            Assert.That(testCustomer.Surname, Is.EqualTo("NewSurname"));
        }

        // ContactInfo Tests
        [Test]
        public void TestCustomerContactInfo()
        {
            Assert.That(customer.ContactInfo, Is.EqualTo("ellie.williams@email.com"));
        }

        [Test]
        public void TestCustomerEmptyContactInfoException()
        {
            Assert.Throws<ArgumentException>(() => new Customer("Ellie", "Williams", ""));
        }

        [Test]
        public void TestCustomerNullContactInfoException()
        {
            Assert.Throws<ArgumentException>(() => new Customer("Ellie", "Williams", null));
        }

        [Test]
        public void TestCustomerContactInfoSetterEmptyException()
        {
            var testCustomer = new Customer("Test", "User", "test@test.com");
            Assert.Throws<ArgumentException>(() => testCustomer.ContactInfo = "");
        }

        [Test]
        public void TestCustomerContactInfoSetterNullException()
        {
            var testCustomer = new Customer("Test", "User", "test@test.com");
            Assert.Throws<ArgumentException>(() => testCustomer.ContactInfo = null);
        }

        [Test]
        public void TestCustomerContactInfoSetter()
        {
            var testCustomer = new Customer("Test", "User", "test@test.com");
            testCustomer.ContactInfo = "new@email.com";
            Assert.That(testCustomer.ContactInfo, Is.EqualTo("new@email.com"));
        }
    }
}