using Amusement_Park_System.Models;

namespace Amusement_Park_System;
using System;
using System.Collections.Generic;
using System.IO;

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
                throw new ArgumentException("Attraction name cannot be empty.");
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
        set => _state = value;
    }
    
    private Zone? _zone;
    public Zone? Zone => _zone;

    protected Attraction(string name, int height, int maxSeats, bool vipPassWorks, Zone? zone)
    {
        Name = name;
        Height = height;
        MaxSeats = maxSeats;
        VipPassWorks = vipPassWorks;
        if (zone !=null) AssignZone(zone);
        
    }
    
    //shift association
    private readonly HashSet<Shift> _shifts = new();
    public IReadOnlyCollection<Shift> Shifts => _shifts.ToList().AsReadOnly();

    internal void AddShiftInternal(Shift shift)
    {
        if (_shifts.Contains(shift))
        {
            return;
        }
        
        _shifts.Add(shift);
    }

    internal void RemoveShiftInternal(Shift shift)
    {
        _shifts.Remove(shift);
    }
    
    //zone association

    public void AssignZone(Zone newZone)
    {
        if (newZone == null)
            throw new ArgumentNullException(nameof(newZone));

        if (_zone != null)
            throw new InvalidOperationException("Attraction already has a zone");

        _zone = newZone;
        newZone.AddAttractionInternal(this);
    }

    public void ClearZone()
    {
        if (_zone == null)
            return;

        _zone.RemoveAttractionInternal(this);
        _zone = null;
    }
    
}