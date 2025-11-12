namespace Amusement_Park_System;
using System;
using System.Collections.Generic;
using System.IO;

[Serializable]
public abstract class Attraction
{
    private string _name;
    private int _height;
    private int _maxSeats;
    private bool _vipPassWorks;
    private AttractionState _state = AttractionState.Active;

    public string Name 
    { 
        get => _name;
         set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Attraction name cannot be empty or whitespace.");
            _name = value;
        }
    }

    public int Height 
    { 
        get => _height;
        set
        {
            if (value < 0)
                throw new ArgumentException("Height cannot be negative.");
            _height = value;
        }
    }

    public int MaxSeats 
    { 
        get => _maxSeats;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Maximum seats must be greater than zero.");
            _maxSeats = value;
        }
    }

    public bool VipPassWorks 
    { 
        get => _vipPassWorks;
        set => _vipPassWorks = value;
    }

    public AttractionState State 
    { 
        get => _state;
        protected set => _state = value;
    }

    protected Attraction(string name, int height, int maxSeats, bool vipPassWorks)
    {
        Name = name;
        Height = height;
        MaxSeats = maxSeats;
        VipPassWorks = vipPassWorks;
        
    }
}