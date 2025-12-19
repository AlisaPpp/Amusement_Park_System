
using Amusement_Park_System;


namespace Amusement_Park_System_Tests;

public class ZoneAttractionAssociationTests
{
    [SetUp]
    public void Setup()
    {
        Zone.ClearExtent();
        Attraction.ClearExtent();
    }

    private static Attraction MakeAttraction(string name, IAttractionIntensity intensity, IAttractionType type)
    {
        return new Attraction(
            name,
            120,
            20,
            true,
            null,
            intensity,
            new List<IAttractionType> { type });
    }

    [Test]
    public void AddAttraction_AssignsZone_WhenAttractionHasNoZone()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var a = MakeAttraction("A1", new ExtremeAttraction(null), new RollerCoaster(400, 80, 3));

        z.AddAttraction(a);

        Assert.That(a.Zone, Is.EqualTo(z));
        Assert.That(z.Attractions, Contains.Item(a));
    }

    [Test]
    public void AddAttraction_Throws_WhenAttractionAlreadyHasZone()
    {
        var z1 = new Zone("Z1", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var z2 = new Zone("Z2", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var a = MakeAttraction("A1", new MediumAttraction(true), new RollerCoaster(400, 80, 3));

        z1.AddAttraction(a);

        Assert.Throws<InvalidOperationException>(() => z2.AddAttraction(a));
    }

    [Test]
    public void AssignZone_Throws_WhenAttractionAlreadyAssigned()
    {
        var z1 = new Zone("Z1", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var z2 = new Zone("Z2", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var a = MakeAttraction("A1", new LightAttraction(false), new RollerCoaster(400, 80, 3));

        z1.AddAttraction(a);

        Assert.Throws<InvalidOperationException>(() => a.AssignZone(z2));
    }

    [Test]
    public void ClearZone_RemovesAttractionFromZone()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var a = MakeAttraction("A1", new ExtremeAttraction(null), new RollerCoaster(400, 80, 3));

        z.AddAttraction(a);
        a.ClearZone();

        Assert.That(a.Zone, Is.Null);
        Assert.That(z.Attractions, Does.Not.Contain(a));
    }

    [Test]
    public void AddAttraction_DoesNotAddDuplicate()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var a = MakeAttraction("A1", new LightAttraction(true), new WaterRide(2.0, 20.0));

        z.AddAttraction(a);
        z.AddAttraction(a);

        Assert.That(z.Attractions.Count, Is.EqualTo(1));
    }

    [Test]
    public void MultipleAttractions_CanExistInOneZone()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var a1 = MakeAttraction("A1", new ExtremeAttraction(null), new RollerCoaster(400, 80, 3));
        var a2 = MakeAttraction("A2", new MediumAttraction(true), new WaterRide(2.0, 20.0));

        z.AddAttraction(a1);
        z.AddAttraction(a2);

        Assert.That(z.Attractions.Count, Is.EqualTo(2));
        Assert.That(z.Attractions, Contains.Item(a1));
        Assert.That(z.Attractions, Contains.Item(a2));
    }

    [Test]
    public void ZoneWithNoAttractions_HasEmptyCollection()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));

        Assert.That(z.Attractions.Count, Is.EqualTo(0));
    }
}
