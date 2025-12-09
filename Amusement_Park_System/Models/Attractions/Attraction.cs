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

    protected Attraction(string name, int height, int maxSeats, bool vipPassWorks)
    {
        Name = name;
        Height = height;
        MaxSeats = maxSeats;
        VipPassWorks = vipPassWorks;
        
    }
    
    
    private readonly HashSet<Shift> _shifts = new();

    public IReadOnlyCollection<Shift> Shifts => _shifts;
    
    
    public Shift AssignShift(Employee employee,
        DateTime date,
        DateTime startTime,
        DateTime endTime)
    {
        if (employee == null) throw new ArgumentNullException(nameof(employee));

        if (_shifts.Any(s =>
                s.Employee  == employee &&
                s.Date      == date.Date &&
                s.StartTime == startTime &&
                s.EndTime   == endTime))
        {
            throw new InvalidOperationException("This shift is already assigned for this attraction, employee and time.");
        }
        
        var shift = Shift.Create(employee, this, date, startTime, endTime);
        
        _shifts.Add(shift);
        employee.AssignShift(this, shift);

        return shift;
    }

    public void RemoveShift(Shift shift)
    {
        if (shift == null) throw new ArgumentNullException(nameof(shift));
        if (!_shifts.Contains(shift)) return;

        _shifts.Remove(shift);
        shift.Employee?.RemoveShiftInternal(shift);
        shift.Delete();
    }
    
    internal void AddShiftFromEmployee(Shift shift)
    {
        if (shift == null) throw new ArgumentNullException(nameof(shift));
        _shifts.Add(shift);
    }

    internal void RemoveShiftInternal(Shift shift)
    {
        _shifts.Remove(shift);
    }
    
}