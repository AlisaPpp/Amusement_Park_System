namespace Amusement_Park_System;

[Serializable]
public class LightAttraction : Attraction
{
   
    public int MinimumHeightRequirement { get; private set; }
    public bool IsParentSupervisionRequired { get; private set; }
    

    public LightAttraction(string name, int height, int maxSeats, bool vipPassWorks,
        int minimumHeightRequirement, bool isParentSupervisionRequired )
        : base(name, height, maxSeats, vipPassWorks)
    {
        if (minimumHeightRequirement < 100)
            throw new ArgumentException("Light attractions must have minimum height of 100cm");
        
        this.MinimumHeightRequirement = minimumHeightRequirement;
        this.IsParentSupervisionRequired = isParentSupervisionRequired;
        
    }
}