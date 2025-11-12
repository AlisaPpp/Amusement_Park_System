namespace Amusement_Park_System;

[Serializable]
public class ExtremeAttraction : Attraction
{
    public static int MinimumAge = 12;
    public static int MinimumHeightRequirement = 140;
    public List<string>? SafetyRestrictions { get; private set; }


    public ExtremeAttraction(string name, int height, int maxSeats, bool vipPassWorks, string safetyRestrictions)
        : base(name, height, maxSeats, vipPassWorks)
    {
        SafetyRestrictions = new List<string>();
    }


}
