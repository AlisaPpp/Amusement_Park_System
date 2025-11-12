namespace Amusement_Park_System;

[Serializable]
public class WaterRide : Attraction
{
    private double _waterDepth;
    private double _waterTemperature;

    public double WaterDepth 
    { 
        get => _waterDepth;
        set
        {
            if (value < 0)
                throw new ArgumentException("Water depth cannot be negative.");
            _waterDepth = value;
        }
    }

    public double WaterTemperature 
    { 
        get => _waterTemperature;
        set => _waterTemperature = value;
    }

    public WaterRide(string name, int height, int maxSeats, bool vipPassWorks,
        double waterDepth, double waterTemperature)
        : base(name, height, maxSeats, vipPassWorks)
    {
        WaterDepth = waterDepth;
        WaterTemperature = waterTemperature;
    }
}