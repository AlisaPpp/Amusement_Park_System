using Amusement_Park_System;

namespace Amusement_Park_System_Tests
{
    public class MeduimAttractionTests
    {
        private Attraction mediumAttraction = new MediumAttraction("Pepsy hyperion",
            120, 24, false, true);

        // FamilyFriendly Tests
        [Test]
        public void TestMediumAttractionFamilyFriendly()
        {
            var medium = (MediumAttraction)mediumAttraction;
            Assert.That(medium.FamilyFriendly, Is.True);
        }
    }
}