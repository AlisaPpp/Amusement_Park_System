namespace Amusement_Park_System;

[Serializable]
public class MediumAttraction : Attraction

{
    public const int MinimumHeightRequirement = 120;
    public const int MinimumAge = 8;
    public bool FamilyFriendly { get; private set; }
    
    public MediumAttraction(string name, int height, int maxSeats, bool vipPassWorks, bool familyFriendly)
        : base(name, height, maxSeats, vipPassWorks)
    {
        FamilyFriendly = familyFriendly;
    }
}