using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

public class MediumAttraction : IAttractionIntensity
{
    public int MinimumHeightRequirement => 120;
    public int MinimumAge => 8;
    
    private bool _familyFriendly;

    public bool FamilyFriendly 
    { 
        get => _familyFriendly;
        set => _familyFriendly = value;
    }
    
    public MediumAttraction(bool familyFriendly)
    {
        FamilyFriendly = familyFriendly;
    }
}