using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

[Serializable]
public class ExtremeAttraction : IAttractionIntensity
{
    public int MinimumAge => 12;
    public int MinimumHeightRequirement => 140;
    
    private List<string>? _safetyRestrictions;

    public List<string>? SafetyRestrictions 
    { 
        get => _safetyRestrictions;
        set
        {
            if (value != null)
            {
                if (value.Any(s => string.IsNullOrWhiteSpace(s)))
                    throw new ArgumentException("The safety restrictions cannot be empty or whitespace.");
            }
            _safetyRestrictions = value;
        }
    }


    public ExtremeAttraction(List<string>? safetyRestrictions)
    {
        SafetyRestrictions = safetyRestrictions;
    }

}
