namespace Amusement_Park_System;

[Serializable]
public class LightAttraction : Attraction
{

    public int MinimumHeightRequirement = 100;
    public bool IsParentSupervisionRequired { get; private set; }
    

    public LightAttraction(string name, int height, int maxSeats, bool vipPassWorks, bool isParentSupervisionRequired )
        : base(name, height, maxSeats, vipPassWorks)
    {
       IsParentSupervisionRequired = isParentSupervisionRequired;
       
    }
}