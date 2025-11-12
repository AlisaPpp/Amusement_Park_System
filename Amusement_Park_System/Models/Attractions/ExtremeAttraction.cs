namespace Amusement_Park_System;

[Serializable]
public class ExtremeAttraction : Attraction
{
    public int MinimumAge { get; private set; }
    public int MinimumHeight { get; private set; }
    public string SafetyRestrictions { get; private set; }

    public ExtremeAttraction(string name, int height, int maxSeats, bool vipPassWorks,
                           int minimumAge, int minimumHeight, string safetyRestrictions)
        : base(name, height, maxSeats, vipPassWorks)
    {
        if (minimumAge < 12)
            throw new ArgumentException("Extreme attractions require minimum age of 12 years");
        if (minimumHeight < 140)
            throw new ArgumentException("Extreme attractions must have minimum height of 140cm");
        if (string.IsNullOrWhiteSpace(safetyRestrictions))
            throw new ArgumentException("Safety restrictions cannot be empty");

        this.MinimumAge = minimumAge;
        this.MinimumHeight = minimumHeight;
        this.SafetyRestrictions = safetyRestrictions;
    }


}
