namespace Amusement_Park_System;
using System;
using System.Collections.Generic;
using System.IO;

[Serializable]
public class Attraction
{
    public String Name { get; private set;}
    public bool DoesVipWorks { get; private set; }
    public int AttractionHeight { get; private set; }
    public Zone Zone { get; private set; }
    public AttractionState State { get; private set; } = AttractionState.Active;
    public int MaxSeats { get; private set; }


}