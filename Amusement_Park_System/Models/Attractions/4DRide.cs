using Amusement_Park_System.Models;
using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

public class FourDRide : IAttractionType
{
    
    private double _showDuration;
    private List<string> _effectTypes;

    public double ShowDuration 
    { 
        get => _showDuration;
        set
        {
            if (value < 0)
                throw new ArgumentException("Show duration cannot be negative.");
            _showDuration = value;
        }
    }

    public List<string> EffectTypes 
    { 
        get => new List<string>(_effectTypes);
        set
        {
            if (value == null || value.Count == 0)
                throw new ArgumentException("Effect types must contain at least one effect.");
            _effectTypes = new List<string>(value);
        }
    }

    public FourDRide (double showDuration, List<string> effectTypes)
    {
        ShowDuration = showDuration;
        EffectTypes = effectTypes;
    }
}