using Amusement_Park_System;
using Amusement_Park_System.Models;

namespace Amusement_Park_System_Tests;

public class ZoneShopAssociationTests
{
    [SetUp]
    public void Setup()
    {
        Zone.ClearExtent();
        Shop.ClearExtent();
    }

    [Test]
    public void ShopConstructor_AssignsZone_WhenZoneNotNull()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var s = new Shop("S", ShopType.Food, "L1", z);

        Assert.That(s.Zone, Is.EqualTo(z));
        Assert.That(z.Shops, Contains.Item(s));
    }

    [Test]
    public void ShopConstructor_AllowsNullZone()
    {
        var s = new Shop("S", ShopType.Food, "L1", null);

        Assert.That(s.Zone, Is.Null);
    }

    [Test]
    public void AddShop_AssignsZone_WhenShopHasNoZone()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var s = new Shop("S", ShopType.Food, "L1", null);

        z.AddShop(s);

        Assert.That(s.Zone, Is.EqualTo(z));
        Assert.That(z.Shops, Contains.Item(s));
    }

    [Test]
    public void AddShop_Throws_WhenShopAlreadyHasZone()
    {
        var z1 = new Zone("Z1", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var z2 = new Zone("Z2", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var s = new Shop("S", ShopType.Food, "L1", z1);

        Assert.Throws<InvalidOperationException>(() => z2.AddShop(s));
    }

    [Test]
    public void AssignZone_Throws_WhenShopAlreadyAssigned()
    {
        var z1 = new Zone("A", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var z2 = new Zone("B", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var s = new Shop("S", ShopType.Merchandise, "L", z1);

        Assert.Throws<InvalidOperationException>(() => s.AssignZone(z2));
    }

    [Test]
    public void ClearZone_RemovesShopFromZone()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var s = new Shop("S", ShopType.Food, "L", z);

        s.ClearZone();

        Assert.That(s.Zone, Is.Null);
        Assert.That(z.Shops, Does.Not.Contain(s));
    }

    [Test]
    public void AddShop_DoesNotAddDuplicate()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var s = new Shop("S", ShopType.Food, "L", z);
        z.AddShop(s);

        Assert.That(z.Shops.Count, Is.EqualTo(1));
    }

    [Test]
    public void MultipleShops_CanExistInOneZone()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var s1 = new Shop("S1", ShopType.Food, "L1", z);
        var s2 = new Shop("S2", ShopType.Merchandise, "L2", z);

        Assert.That(z.Shops.Count, Is.EqualTo(2));
    }

    [Test]
    public void Extent_IncreasesOnCreation()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var s1 = new Shop("S1", ShopType.Food, "L1", z);
        var s2 = new Shop("S2", ShopType.Food, "L2", null);

        Assert.That(Shop.Extent.Count, Is.EqualTo(2));
    }

    [Test]
    public void Extent_ClearsCorrectly()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));
        var s = new Shop("S", ShopType.Food, "L", z);

        Shop.ClearExtent();

        Assert.That(Shop.Extent.Count, Is.EqualTo(0));
    }

    [Test]
    public void ZoneWithNoShops_HasEmptyCollection()
    {
        var z = new Zone("Z", "T", TimeSpan.FromHours(9), TimeSpan.FromHours(22));

        Assert.That(z.Shops.Count, Is.EqualTo(0));
    }
}
