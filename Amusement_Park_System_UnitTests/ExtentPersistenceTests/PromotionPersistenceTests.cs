using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests.ExtentPersistanceTests
{
    [TestFixture]
    public class PromotionPersistenceTests
    {
        private string _filePath = Promotion.FilePath;

        //before every test
        [SetUp]
        public void TestSetUp()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Promotion.Extent = new List<Promotion>();
        }

        //after every test
        [TearDown]
        public void TestTearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Promotion.Extent = new List<Promotion>();
        }

        [Test]
        public void Constructor_AddsInstanceToExtent()
        {
            var start1 = DateTime.Today.AddDays(10);
            var end1 = start1.AddDays(30);

            var start2 = DateTime.Today.AddDays(50);
            var end2 = start2.AddDays(20);

            var one = new Promotion("ChristmasSale", start1, end1, 50);
            var two = new Promotion("HalloweenSale", start2, end2, 30);

            Assert.That(Promotion.Extent.Count, Is.EqualTo(2));
            Assert.That(Promotion.Extent, Does.Contain(one));
            Assert.That(Promotion.Extent, Does.Contain(two));
        }

        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            var start = DateTime.Today.AddDays(10);
            var end = start.AddDays(30);

            var one = new Promotion("ChristmasSale", start, end, 50);

            var fromExtent = Promotion.Extent.Single(x => x.Name == "ChristmasSale");

            one.PromotionPercent = 60;

            Assert.That(fromExtent.PromotionPercent, Is.EqualTo(60));
        }

        [Test]
        public void Save_CreatesFile()
        {
            var start = DateTime.Today.AddDays(10);
            var end = start.AddDays(30);

            var one = new Promotion("ChristmasSale", start, end, 50);

            Promotion.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            var start1 = DateTime.Today.AddDays(10);
            var end1 = start1.AddDays(30);

            var start2 = DateTime.Today.AddDays(50);
            var end2 = start2.AddDays(20);

            var one = new Promotion("ChristmasSale", start1, end1, 50);
            var two = new Promotion("HalloweenSale", start2, end2, 30);

            Promotion.Save();
            Promotion.Extent = new List<Promotion>();
            Promotion.Load();

            Assert.That(Promotion.Extent.Count, Is.EqualTo(2));

            var xmas = Promotion.Extent.Single(p => p.Name == "ChristmasSale");
            Assert.That(xmas.StartDate, Is.EqualTo(start1));
            Assert.That(xmas.EndDate, Is.EqualTo(end1));
            Assert.That(xmas.PromotionPercent, Is.EqualTo(50));

            var halloween = Promotion.Extent.Single(p => p.Name == "HalloweenSale");
            Assert.That(halloween.StartDate, Is.EqualTo(start2));
            Assert.That(halloween.EndDate, Is.EqualTo(end2));
            Assert.That(halloween.PromotionPercent, Is.EqualTo(30));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            var start = DateTime.Today.AddDays(10);
            var end = start.AddDays(30);

            var one = new Promotion("ChristmasSale", start, end, 50);

            Assert.That(Promotion.Extent, Is.Not.Empty);

            Promotion.Load();

            Assert.That(Promotion.Extent, Is.Empty);
        }
    }
}
