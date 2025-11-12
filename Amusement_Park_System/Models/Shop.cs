namespace Amusement_Park_System;
using System;

[Serializable]
public class Shop
{
    private string _name;
    private ShopType _type;
    private string _location;

    public string Name 
    { 
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Shop name cannot be empty.");
            _name = value;
        }
    }

    public ShopType Type 
    { 
        get => _type;
        set => _type = value;
    }

    public string Location 
    { 
        get => _location;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Shop location cannot be empty.");
            _location = value;
        }
    }

    public Shop(string name, ShopType type, string location)
    {
        Name = name;
        Type = type;
        Location = location;
    }
}