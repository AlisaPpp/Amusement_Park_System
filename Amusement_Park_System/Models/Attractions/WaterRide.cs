using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

public class WaterRide : IAttractionType
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

    public WaterRide(double waterDepth, double waterTemperature)
    {
        WaterDepth = waterDepth;
        WaterTemperature = waterTemperature;
    }
}