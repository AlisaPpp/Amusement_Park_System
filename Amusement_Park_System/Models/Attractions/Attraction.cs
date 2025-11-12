

namespace Amusement_Park_System;
using System;
using System.Collections.Generic;
using System.IO;

[Serializable]
public abstract class Attraction
{
    public static List<Attraction> Extent { get; private set; } = new List<Attraction>();
    
    public string Name { get; protected set; }
    public int Height { get; protected set; }
    public int MaxSeats { get; protected set; }
    public bool VipPassWorks { get; protected set; }
    public AttractionState State { get; protected set; } = AttractionState.Active;

    protected Attraction(string name, int height, int maxSeats, bool vipPassWorks)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Attraction name cannot be empty.");
        if (height < 0)
            throw new ArgumentException("Height cannot be negative.");
        if (maxSeats <= 0)
            throw new ArgumentException("Maximum seats must be greater than zero.");

        this.Name = name;
        this.Height = height;
        this.MaxSeats = maxSeats;
        this.VipPassWorks = vipPassWorks;

        Extent.Add(this);
    }
    

}