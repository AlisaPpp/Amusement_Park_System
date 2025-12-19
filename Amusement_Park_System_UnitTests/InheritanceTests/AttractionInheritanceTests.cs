
using Amusement_Park_System;

namespace Amusement_Park_System_Tests
{
    public class AttractionInheritanceTests
    {
        private IAttractionIntensity _extremeIntensity;
        private IAttractionIntensity _mediumIntensity;
        private IAttractionIntensity _lightIntensity;

        private IAttractionType _rollerCoaster;
        private IAttractionType _waterRide;
        private IAttractionType _fourDRide;

        [SetUp]
        public void Setup()
        {
            Attraction.ClearExtent();

            _extremeIntensity = new ExtremeAttraction(new List<string> { "No heart problems" });
            _mediumIntensity = new MediumAttraction(true);
            _lightIntensity = new LightAttraction(false);

            _rollerCoaster = new RollerCoaster(1000, 120, 3);
            _waterRide = new WaterRide(2.5, 25);
            _fourDRide = new FourDRide(10, new List<string> { "Smoke", "Lights" });
        }

        [Test]
        public void Can_Create_Attraction_With_Intensity_And_Types()
        {
            var attraction = new Attraction(
                "Ride 1",
                150,
                20,
                true,
                null,
                _extremeIntensity,
                new List<IAttractionType> { _rollerCoaster, _waterRide }
            );

            Assert.That(attraction.Intensity, Is.EqualTo(_extremeIntensity));
            CollectionAssert.Contains(attraction.Types, _rollerCoaster);
            CollectionAssert.Contains(attraction.Types, _waterRide);
        }

        [Test]
        public void Attraction_Throws_When_Intensity_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Attraction(
                    "Invalid Ride",
                    100,
                    10,
                    true,
                    null,
                    null,
                    new List<IAttractionType> { _rollerCoaster }
                )
            );
        }

        [Test]
        public void Attraction_Throws_When_No_Types()
        {
            Assert.Throws<ArgumentException>(() =>
                new Attraction(
                    "No Types Ride",
                    100,
                    10,
                    true,
                    null,
                    _extremeIntensity,
                    new List<IAttractionType>()
                )
            );
        }

        [Test]
        public void Attraction_Throws_When_Duplicate_Types()
        {
            var duplicateTypes = new List<IAttractionType> { _rollerCoaster, _rollerCoaster };
            Assert.Throws<ArgumentException>(() =>
                new Attraction(
                    "Duplicate Types Ride",
                    120,
                    15,
                    true,
                    null,
                    _extremeIntensity,
                    duplicateTypes
                )
            );
        }

        [Test]
        public void Attraction_Types_Are_Overlapping()
        {
            var types = new List<IAttractionType> { _rollerCoaster, _fourDRide };
            var attraction1 = new Attraction("Ride1", 150, 10, true,null, _extremeIntensity, types);
            var attraction2 = new Attraction("Ride2", 160, 12, true,null, _mediumIntensity,
                new List<IAttractionType> { _fourDRide });

            CollectionAssert.Contains(attraction1.Types, _fourDRide);
            CollectionAssert.Contains(attraction2.Types, _fourDRide);
        }

        [Test]
        public void Attraction_Intensity_Is_Disjoint()
        {
            var attraction1 = new Attraction("Ride1", 150, 10, true,null, _extremeIntensity,
                new List<IAttractionType> { _rollerCoaster });
            var attraction2 = new Attraction("Ride2", 160, 12, true,null, _mediumIntensity,
                new List<IAttractionType> { _waterRide });

            Assert.That(attraction1.Intensity, Is.InstanceOf<ExtremeAttraction>());
            Assert.That(attraction2.Intensity, Is.InstanceOf<MediumAttraction>());
        }

        [Test]
        public void Attraction_Validates_NameHeightMaxSeats()
        {
            var attraction = new Attraction("Ride1", 150, 20, true, null, _lightIntensity,
                new List<IAttractionType> { _rollerCoaster });

            Assert.That(attraction.Name, Is.EqualTo("Ride1"));
            Assert.That(attraction.Height, Is.EqualTo(150));
            Assert.That(attraction.MaxSeats, Is.EqualTo(20));
        }
    }
    
}
