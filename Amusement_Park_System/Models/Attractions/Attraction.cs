using Amusement_Park_System.Models;
using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;
using System;
using System.Collections.Generic;
using System.IO;

public class Attraction
{
    private static List<Attraction> _extent = new();
    public static IReadOnlyList<Attraction> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/attractions.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    
    private string _name;
    private int _height;
    private int _maxSeats;
    private bool _vipPassWorks;
    private AttractionState _state = AttractionState.Active;
    
    //multi-aspect inheritance using composition method
    public IAttractionIntensity Intensity { get;}
    private readonly List<IAttractionType> _types = new();
    public IReadOnlyCollection<IAttractionType> Types => _types.AsReadOnly();

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

    protected Attraction(string name, int height, int maxSeats, bool vipPassWorks, Zone? zone, 
        IAttractionIntensity intensity, IEnumerable<IAttractionType> types)
    {
        Name = name;
        Height = height;
        MaxSeats = maxSeats;
        VipPassWorks = vipPassWorks;
        Intensity = intensity ?? throw new ArgumentNullException(nameof(intensity));
        if (types == null)
            throw new ArgumentNullException(nameof(types));
        var typeList = types.ToList();
        if (!typeList.Any())
            throw new ArgumentException("Attraction must be of at least one type.");
        if (typeList.GroupBy(t => t.GetType()).Any(g => g.Count() > 1))
            throw new ArgumentException("Duplicate attraction types are not allowed.");
        _types.AddRange(typeList);
        if (zone !=null) AssignZone(zone);
        _extent.Add(this);
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
        if (!_shifts.Contains(shift))
        {
            return;
        }
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
    
    //malfunction report association
    private HashSet<MalfunctionReport> _reports = new();
    public IReadOnlyCollection<MalfunctionReport> Reports => _reports;

    internal void AddReportInternal(MalfunctionReport report)
    {
        if (report == null) 
            throw new ArgumentNullException(nameof(report));
        _reports.Add(report);
    }

}