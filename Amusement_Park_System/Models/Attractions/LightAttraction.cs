namespace Amusement_Park_System;

[Serializable]
public class LightAttraction : Attraction
{
    public static int MinimumHeightRequirement = 100;
    
    private bool _isParentSupervisionRequired;

    public bool IsParentSupervisionRequired 
    { 
        get => _isParentSupervisionRequired;
        set => _isParentSupervisionRequired = value;
    }
    

    public LightAttraction(string name, int height, int maxSeats, bool vipPassWorks, bool isParentSupervisionRequired )
        : base(name, height, maxSeats, vipPassWorks)
    {
       IsParentSupervisionRequired = isParentSupervisionRequired;
       
    }
}