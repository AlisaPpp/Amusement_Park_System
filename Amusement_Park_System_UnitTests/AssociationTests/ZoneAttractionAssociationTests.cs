using Amusement_Park_System;

namespace Amusement_Park_System_Tests;

public class ZoneAttractionAssociationTests
{
    [SetUp]
    public void Setup()
    {
        Zone.ClearExtent();
        ExtremeAttraction.ClearExtent();
        MediumAttraction.ClearExtent();
        LightAttraction.ClearExtent();
        FourDRide.ClearExtent();
        RollerCoaster.ClearExtent();
        WaterRide.ClearExtent();
    }

    [Test]
    public void AddAttraction_AssignsZone_WhenAttractionHasNoZone()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var a = new ExtremeAttraction("Extreme1", 150, 10, true, null);

        z.AddAttraction(a);

        Assert.That(a.Zone, Is.EqualTo(z));
        Assert.That(z.Attractions, Contains.Item(a));
    }

    [Test]
    public void AddAttraction_Throws_WhenAttractionAlreadyHasZone()
    {
        var z1 = new Zone("Z1", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var z2 = new Zone("Z2", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var a = new MediumAttraction("Medium1", 120, 20, true, true);

        z1.AddAttraction(a);

        Assert.Throws<InvalidOperationException>(() => z2.AddAttraction(a));
    }

    [Test]
    public void AssignZone_Throws_WhenAttractionAlreadyAssigned()
    {
        var z1 = new Zone("Z1", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var z2 = new Zone("Z2", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var a = new LightAttraction("Light1", 100, 12, true, false);

        z1.AddAttraction(a);

        Assert.Throws<InvalidOperationException>(() => a.AssignZone(z2));
    }

    [Test]
    public void ClearZone_RemovesAttractionFromZone()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var a = new RollerCoaster("RC1", 50, 12, true, 400, 80, 3);

        z.AddAttraction(a);
        a.ClearZone();

        Assert.That(a.Zone, Is.Null);
        Assert.That(z.Attractions, Does.Not.Contain(a));
    }

    [Test]
    public void AddAttraction_DoesNotAddDuplicate()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var a = new WaterRide("Water1", 10, 5, true, 2.0, 20.0);

        z.AddAttraction(a);
        z.AddAttraction(a);

        Assert.That(z.Attractions.Count, Is.EqualTo(1));
    }

    [Test]
    public void MultipleAttractions_CanExistInOneZone()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var a1 = new ExtremeAttraction("Extreme1", 150, 20, true, null);
        var a2 = new MediumAttraction("Medium1", 120, 20, true, true);

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

    [Test]
    public void DeleteZone_RemovesAllAttractionsFromExtent()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var a1 = new ExtremeAttraction("Extreme1", 150, 10, true, null);
        var a2 = new MediumAttraction("Medium1", 120, 20, true, true);

        z.AddAttraction(a1);
        z.AddAttraction(a2);

        z.DeleteZone();

        Assert.That(a1.Zone, Is.Null);
        Assert.That(a2.Zone, Is.Null);
        Assert.That(z.Attractions.Count, Is.EqualTo(0));
    }

    [Test]
    public void ChildZone_DeleteCascadesAttractions()
    {
        var parent = new Zone("Parent", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var child = new Zone("Child", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        parent.AddChild(child);

        var a = new RollerCoaster("RC", 50, 12, true, 400, 80, 3);
        child.AddAttraction(a);

        parent.DeleteZone();

        Assert.That(child.Attractions.Count, Is.EqualTo(0));
        Assert.That(a.Zone, Is.Null);
    }
}
