namespace Amusement_Park_System;


[Serializable]
class Attraction
{
    public String Name { get; private set;}
    public bool DoesVipWorks { get; private set; }
    public int AttractionHeight { get; private set; }
    public Zone Zone { get; private set; }
    public String Status { get; set; } = "Active";
    public int MaxSeats { get; private set; }


}