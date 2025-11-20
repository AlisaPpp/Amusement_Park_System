using Amusement_Park_System.Models;
using System;

namespace Amusement_Park_System_Tests
{
    public class PromotionTests
    {
        private readonly Promotion promotion = new Promotion(
            "Summer Sale", 
            DateTime.Now.Date.AddDays(1), 
            DateTime.Now.Date.AddDays(30), 
            20);

        // Name Tests
        [Test]
        public void TestPromotionName()
        {
            Assert.That(promotion.Name, Is.EqualTo("Summer Sale"));
        }

        [Test]
        public void TestPromotionEmptyNameException()
        {
            Assert.Throws<ArgumentException>(() => new Promotion(
                "", 
                DateTime.Now.Date.AddDays(1), 
                DateTime.Now.Date.AddDays(30), 
                20));
        }

        [Test]
        public void TestPromotionNullNameException()
        {
            Assert.Throws<ArgumentException>(() => new Promotion(
                null, 
                DateTime.Now.Date.AddDays(1), 
                DateTime.Now.Date.AddDays(30), 
                20));
        }

        [Test]
        public void TestPromotionNameSetterEmptyException()
        {
            var testPromotion = new Promotion(
                "Summer Sale", 
                DateTime.Now.Date.AddDays(1), 
                DateTime.Now.Date.AddDays(30), 
                20);
            Assert.Throws<ArgumentException>(() => testPromotion.Name = "");
        }

        [Test]
        public void TestPromotionNameSetterNullException()
        {
            var testPromotion = new Promotion(
                "Summer Sale", 
                DateTime.Now.Date.AddDays(1), 
                DateTime.Now.Date.AddDays(30), 
                20);
            Assert.Throws<ArgumentException>(() => testPromotion.Name = null);
        }

        [Test]
        public void TestPromotionNameSetter()
        {
            var testPromotion = new Promotion(
                "Summer Sale", 
                DateTime.Now.Date.AddDays(1), 
                DateTime.Now.Date.AddDays(30), 
                20);
            testPromotion.Name = "Winter Sale";
            Assert.That(testPromotion.Name, Is.EqualTo("Winter Sale"));
        }

        // StartDate Tests
        [Test]
        public void TestPromotionStartDate()
        {
            Assert.That(promotion.StartDate, Is.EqualTo(DateTime.Now.Date.AddDays(1)));
        }

        [Test]
        public void TestPromotionPastStartDateException()
        {
            Assert.Throws<ArgumentException>(() => new Promotion(
                "Summer Sale", 
                DateTime.Now.Date.AddDays(-1), 
                DateTime.Now.Date.AddDays(30), 
                20));
        }

        [Test]
        public void TestPromotionStartDateSetterPastException()
        {
            var testPromotion = new Promotion(
                "Summer Sale", 
                DateTime.Now.Date.AddDays(2), 
                DateTime.Now.Date.AddDays(30), 
                20);
            Assert.Throws<ArgumentException>(() => testPromotion.StartDate = DateTime.Now.Date.AddDays(-1));
        }

        [Test]
        public void TestPromotionStartDateSetter()
        {
            var testPromotion = new Promotion(
                "Summer Sale", 
                DateTime.Now.Date.AddDays(1), 
                DateTime.Now.Date.AddDays(30), 
                20);
            testPromotion.StartDate = DateTime.Now.Date.AddDays(5);
            Assert.That(testPromotion.StartDate, Is.EqualTo(DateTime.Now.Date.AddDays(5)));
        }

        // EndDate Tests
        [Test]
        public void TestPromotionEndDate()
        {
            Assert.That(promotion.EndDate, Is.EqualTo(DateTime.Now.Date.AddDays(30)));
        }

        [Test]
        public void TestPromotionEndDateBeforeStartDateException()
        {
            Assert.Throws<ArgumentException>(() => new Promotion(
                "Summer Sale", 
                DateTime.Now.Date.AddDays(10), 
                DateTime.Now.Date.AddDays(5), 
                20));
        }

        [Test]
        public void TestPromotionEndDateSetterBeforeStartException()
        {
            var testPromotion = new Promotion(
                "Summer Sale", 
                DateTime.Now.Date.AddDays(10), 
                DateTime.Now.Date.AddDays(30), 
                20);
            Assert.Throws<ArgumentException>(() => testPromotion.EndDate = DateTime.Now.Date.AddDays(5));
        }

        [Test]
        public void TestPromotionEndDateSetter()
        {
            var testPromotion = new Promotion(
                "Summer Sale", 
                DateTime.Now.Date.AddDays(1), 
                DateTime.Now.Date.AddDays(30), 
                20);
            testPromotion.EndDate = DateTime.Now.Date.AddDays(45);
            Assert.That(testPromotion.EndDate, Is.EqualTo(DateTime.Now.Date.AddDays(45)));
        }

        // PromotionPercent Tests
        [Test]
        public void TestPromotionPromotionPercent()
        {
            Assert.That(promotion.PromotionPercent, Is.EqualTo(20));
        }

        [Test]
        public void TestPromotionNegativePromotionPercentException()
        {
            Assert.Throws<ArgumentException>(() => new Promotion(
                "Summer Sale", 
                DateTime.Now.Date.AddDays(1), 
                DateTime.Now.Date.AddDays(30), 
                -5));
        }

        [Test]
        public void TestPromotionPromotionPercentOver100Exception()
        {
            Assert.Throws<ArgumentException>(() => new Promotion(
                "Summer Sale", 
                DateTime.Now.Date.AddDays(1), 
                DateTime.Now.Date.AddDays(30), 
                101));
        }

        [Test]
        public void TestPromotionPromotionPercentSetterNegativeException()
        {
            var testPromotion = new Promotion(
                "Summer Sale", 
                DateTime.Now.Date.AddDays(1), 
                DateTime.Now.Date.AddDays(30), 
                20);
            Assert.Throws<ArgumentException>(() => testPromotion.PromotionPercent = -5);
        }

        [Test]
        public void TestPromotionPromotionPercentSetterOver100Exception()
        {
            var testPromotion = new Promotion(
                "Summer Sale", 
                DateTime.Now.Date.AddDays(1), 
                DateTime.Now.Date.AddDays(30), 
                20);
            Assert.Throws<ArgumentException>(() => testPromotion.PromotionPercent = 101);
        }

        [Test]
        public void TestPromotionPromotionPercentSetter()
        {
            var testPromotion = new Promotion(
                "Summer Sale", 
                DateTime.Now.Date.AddDays(1), 
                DateTime.Now.Date.AddDays(30), 
                20);
            testPromotion.PromotionPercent = 30;
            Assert.That(testPromotion.PromotionPercent, Is.EqualTo(30));
        }
    }
}