using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

public class LightAttraction : IAttractionIntensity
{
    public int MinimumAge => 0;
    public int MinimumHeightRequirement => 100;
    
    private bool _isParentSupervisionRequired;

    public bool IsParentSupervisionRequired 
    { 
        get => _isParentSupervisionRequired;
        set => _isParentSupervisionRequired = value;
    }
    

    public LightAttraction(bool isParentSupervisionRequired)
    {
       IsParentSupervisionRequired = isParentSupervisionRequired;
    }
}