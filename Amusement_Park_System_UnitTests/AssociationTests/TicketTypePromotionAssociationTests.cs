using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests
{
    public class TicketTypePromotionAssociationTests
    {
        private TicketType ticketType1;
        private TicketType ticketType2;
        private Promotion promotion1;
        private Promotion promotion2;

        [SetUp]
        public void Setup()
        {
            TicketType.ClearExtent();
            Promotion.ClearExtent();
            
            ticketType1 = new TicketType("Regular", false, 100m);
            ticketType2 = new TicketType("VIP", true, 200m);
            
            promotion1 = new Promotion("Summer Sale", DateTime.Now, DateTime.Now.AddDays(30), 20);
            promotion2 = new Promotion("Winter Sale", DateTime.Now, DateTime.Now.AddDays(60), 30);
        }

        [Test]
        public void TestTicketTypeAssignPromotionCreatesBidirectionalAssociation()
        {
            ticketType1.AssignPromotion(promotion1);
            
            Assert.That(ticketType1.Promotion, Is.EqualTo(promotion1));
            Assert.That(promotion1.TicketTypes, Contains.Item(ticketType1));
        }

        [Test]
        public void TestPromotionAddTicketTypeCreatesBidirectionalAssociation()
        {
            promotion1.AddTicketType(ticketType1);
            
            Assert.That(ticketType1.Promotion, Is.EqualTo(promotion1));
            Assert.That(promotion1.TicketTypes, Contains.Item(ticketType1));
        }

        [Test]
        public void TestTicketTypeAssignPromotionRemovesPreviousPromotion()
        {
            ticketType1.AssignPromotion(promotion1);
            ticketType1.AssignPromotion(promotion2);
            
            Assert.That(ticketType1.Promotion, Is.EqualTo(promotion2));
            Assert.That(promotion1.TicketTypes, Does.Not.Contain(ticketType1));
            Assert.That(promotion2.TicketTypes, Contains.Item(ticketType1));
        }

        [Test]
        public void TestTicketTypeAssignNullPromotion()
        {
            ticketType1.AssignPromotion(promotion1);
            ticketType1.AssignPromotion(null);
            
            Assert.That(ticketType1.Promotion, Is.Null);
            Assert.That(promotion1.TicketTypes, Does.Not.Contain(ticketType1));
        }

        [Test]
        public void TestPromotionRemoveTicketType()
        {
            promotion1.AddTicketType(ticketType1);
            promotion1.RemoveTicketType(ticketType1);
            
            Assert.That(ticketType1.Promotion, Is.Null);
            Assert.That(promotion1.TicketTypes, Does.Not.Contain(ticketType1));
        }

        [Test]
        public void TestPromotionDelete()
        {
            promotion1.AddTicketType(ticketType1);
            promotion1.AddTicketType(ticketType2);
            
            promotion1.Delete();
            
            Assert.That(ticketType1.Promotion, Is.Null);
            Assert.That(ticketType2.Promotion, Is.Null);
            Assert.That(Promotion.Extent, Does.Not.Contain(promotion1));
        }

        [Test]
        public void TestMultipleTicketTypesWithSamePromotion()
        {
            promotion1.AddTicketType(ticketType1);
            promotion1.AddTicketType(ticketType2);
            
            Assert.That(ticketType1.Promotion, Is.EqualTo(promotion1));
            Assert.That(ticketType2.Promotion, Is.EqualTo(promotion1));
            Assert.That(promotion1.TicketTypes.Count, Is.EqualTo(2));
            Assert.That(promotion1.TicketTypes, Contains.Item(ticketType1));
            Assert.That(promotion1.TicketTypes, Contains.Item(ticketType2));
        }

        [Test]
        public void TestTicketTypePriceCalculationWithPromotion()
        {
            ticketType1.AssignPromotion(promotion1);
            
            var customer = new Customer("John", "Doe", "john@mail.com");
            var order = new Order(1, "Card", customer);
            var ticket = new Ticket(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 0, 1, ticketType1, order);
            
            decimal expectedPrice = 100m * 0.8m;
            Assert.That(ticket.Price, Is.EqualTo(expectedPrice));
        }

        [Test]
        public void TestExpiredPromotion()
        {
            var expiredPromotion = new Promotion("Expired", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-1), 50);
            ticketType1.AssignPromotion(expiredPromotion);
            
            Assert.That(ticketType1.Promotion, Is.Null);
        }

        [Test]
        public void TestStaticRemoveExpiredPromotions()
        {
            var expiredPromotion = new Promotion("Expired",DateTime.Now,               
                DateTime.Now.AddSeconds(-1), 50);
            ticketType1.AssignPromotion(expiredPromotion);
            
            Promotion.RemoveExpiredPromotions();
            
            Assert.That(Promotion.Extent, Does.Not.Contain(expiredPromotion));
        }

        [Test]
        public void TestTicketTypesCollectionIsReadOnly()
        {
            promotion1.AddTicketType(ticketType1);
            
            var ticketTypes = promotion1.TicketTypes;
            Assert.That(ticketTypes, Is.InstanceOf<IReadOnlyCollection<TicketType>>());
        }
    }
}