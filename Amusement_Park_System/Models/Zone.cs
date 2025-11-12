namespace Amusement_Park_System;
using System;
using System.Collections.Generic;

[Serializable]
public class Zone
{
    
    private string _name;
    private string _theme;
    private TimeSpan _openingTime;
    private TimeSpan _closingTime;

    public string Name
    {
        get => _name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Zone name cannot be empty.");
            _name = value;
        }
    }

    public string Theme
    {
        get => _theme;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Zone theme cannot be empty.");
            _theme = value;
        }
    }

    public TimeSpan OpeningTime
    {
        get => _openingTime;
        private set => _openingTime = value;
    }

    public TimeSpan ClosingTime
    {
        get => _closingTime;
        private set
        {
            if (value <= _openingTime)
                throw new ArgumentException("Closing time must be after opening time.");
            _closingTime = value;
        }
    }

    public Zone(string name, string theme, TimeSpan openingTime, TimeSpan closingTime)
    {
        Name = name;
        Theme = theme;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        
    }
}