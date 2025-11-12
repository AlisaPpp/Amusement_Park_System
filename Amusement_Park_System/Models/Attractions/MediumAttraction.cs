namespace Amusement_Park_System;

[Serializable]
public class MediumAttraction : Attraction
{
    public const int MinimumHeightRequirement = 120;
    public const int MinimumAge = 8;
    
    private bool _familyFriendly;

    public bool FamilyFriendly 
    { 
        get => _familyFriendly;
        private set => _familyFriendly = value;
    }
    
    public MediumAttraction(string name, int height, int maxSeats, bool vipPassWorks, bool familyFriendly)
        : base(name, height, maxSeats, vipPassWorks)
    {
        FamilyFriendly = familyFriendly;
    }
}