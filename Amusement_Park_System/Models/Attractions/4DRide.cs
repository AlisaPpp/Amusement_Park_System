namespace Amusement_Park_System;

[Serializable]
public class FourDRide : Attraction
{
    private double _showDuration;
    private List<string> _effectTypes;

    public double ShowDuration 
    { 
        get => _showDuration;
        private set
        {
            if (value < 0)
                throw new ArgumentException("Show duration cannot be negative.");
            _showDuration = value;
        }
    }

    public List<string> EffectTypes 
    { 
        get => new List<string>(_effectTypes);
        private set
        {
            if (value == null || value.Count == 0)
                throw new ArgumentException("Effect types must contain at least one effect.");
            _effectTypes = new List<string>(value);
        }
    }

    public FourDRide(string name, int height, int maxSeats, bool vipPassWorks,
        double showDuration, List<string> effectTypes)
        : base(name, height, maxSeats, vipPassWorks)
    {
        ShowDuration = showDuration;
        EffectTypes = effectTypes;
    }
}