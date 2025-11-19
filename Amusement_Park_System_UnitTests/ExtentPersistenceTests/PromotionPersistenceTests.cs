
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
            Promotion one = new Promotion("ChristmasSale", DateTime.Parse("2026-01-08"), DateTime.Parse("2026-09-02"),
                50);
            Promotion two = new Promotion("HalloweenSale", DateTime.Parse("2026-10-15"), DateTime.Parse("2026-11-03"),
                30);

            Assert.That(Promotion.Extent.Count, Is.EqualTo(2));
            Assert.That(Promotion.Extent, Does.Contain(one));
            Assert.That(Promotion.Extent, Does.Contain(two));
        }
        
        [Test]
        public void TestChangingPropertyUpdatesObjectInExtent()
        {
            Promotion one = new Promotion("ChristmasSale", DateTime.Parse("2026-01-08"), DateTime.Parse("2026-09-02"),
                50);
            var fromExtent = Promotion.Extent.Single(x => x.Name == "ChristmasSale");
            one.PromotionPercent = 60;

            Assert.That(fromExtent.PromotionPercent, Is.EqualTo(60));
        }


        [Test]
        public void Save_CreatesFile()
        {
            Promotion one = new Promotion("ChristmasSale", DateTime.Parse("2026-01-08"), DateTime.Parse("2026-09-02"),
                50);

            Promotion.Save();

            Assert.That(File.Exists(_filePath), Is.True);
            Assert.That(new FileInfo(_filePath).Length, Is.GreaterThan(0));
        }

        [Test]
        public void SaveThenLoad_RestoresExtent()
        {
            Promotion one = new Promotion("ChristmasSale", DateTime.Parse("2026-01-08"), DateTime.Parse("2026-09-02"),
                50);
            Promotion two = new Promotion("HalloweenSale", DateTime.Parse("2026-10-15"), DateTime.Parse("2026-11-03"),
                30);

            Promotion.Save();
            Promotion.Extent = new List<Promotion>();
            Promotion.Load();


            Assert.That(Promotion.Extent.Count, Is.EqualTo(2));
            var xmas = Promotion.Extent.Single(p => p.Name == "ChristmasSale");
            Assert.That(xmas.StartDate, Is.EqualTo(DateTime.Parse("2026-01-08")));
            Assert.That(xmas.EndDate, Is.EqualTo(DateTime.Parse("2026-09-02")));
            Assert.That(xmas.PromotionPercent, Is.EqualTo(50));

            var halloween = Promotion.Extent.Single(p => p.Name == "HalloweenSale");
            Assert.That(halloween.StartDate, Is.EqualTo(DateTime.Parse("2026-10-15")));
            Assert.That(halloween.EndDate, Is.EqualTo(DateTime.Parse("2026-11-03")));
            Assert.That(halloween.PromotionPercent, Is.EqualTo(30));
        }

        [Test]
        public void TestLoadWhenFileMissingLeavesExtentEmpty()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Promotion one = new Promotion("ChristmasSale", DateTime.Parse("2026-01-08"), DateTime.Parse("2026-09-02"),
                50);

            Assert.That(Promotion.Extent, Is.Not.Empty);


            Promotion.Load();

            Assert.That(Promotion.Extent, Is.Empty);
        }
    }
}