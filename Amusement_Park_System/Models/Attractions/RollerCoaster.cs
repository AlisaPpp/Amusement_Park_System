using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;

public class RollerCoaster : IAttractionType
{
    private double _trackLength;
    private double _maxSpeed;
    private int _numberOfLoops;

    public double TrackLength 
    { 
        get => _trackLength;
        set
        {
            if (value < 0)
                throw new ArgumentException("Track length cannot be negative.");
            _trackLength = value;
        }
    }

    public double MaxSpeed 
    { 
        get => _maxSpeed;
        set
        {
            if (value < 0)
                throw new ArgumentException("Max speed cannot be negative.");
            _maxSpeed = value;
        }
    }

    public int NumberOfLoops 
    { 
        get => _numberOfLoops;
        set
        {
            if (value < 0)
                throw new ArgumentException("Number of loops cannot be negative.");
            _numberOfLoops = value;
        }
    }

    public RollerCoaster(double trackLength, double maxSpeed, int numberOfLoops)
    {
        TrackLength = trackLength;
        MaxSpeed = maxSpeed;
        NumberOfLoops = numberOfLoops;
    }
}