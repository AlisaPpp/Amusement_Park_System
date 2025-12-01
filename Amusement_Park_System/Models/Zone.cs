using Amusement_Park_System.Models;
using Amusement_Park_System.Persistence;

namespace Amusement_Park_System;
using System;
using System.Collections.Generic;

[Serializable]
public class Zone
{
    private static List<Zone> _extent = new();
    public static IReadOnlyList<Zone> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/zones.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    
    private string _name;
    private string _theme;
    private TimeSpan _openingTime;
    private TimeSpan _closingTime;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Zone name cannot be empty.");
            _name = value;
        }
    }

    public string Theme
    {
        get => _theme;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Zone theme cannot be empty.");
            _theme = value;
        }
    }

    public TimeSpan OpeningTime
    {
        get => _openingTime;
        set => _openingTime = value;
    }

    public TimeSpan ClosingTime
    {
        get => _closingTime;
        set
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
        
        _extent.Add(this);
    }
}