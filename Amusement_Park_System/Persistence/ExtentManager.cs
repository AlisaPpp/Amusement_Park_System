using System.Text.Json;

namespace Amusement_Park_System.Persistence;

public static class ExtentManager
{
    public static void Save<T>(List<T> extent, string location)
    {
        if (extent == null)
            throw new ArgumentNullException(nameof(extent));
        
        var directory = Path.GetDirectoryName(location);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        
        var json = JsonSerializer.Serialize(extent, options);
        File.WriteAllText(location, json);
    }

    public static void Load<T>(ref List<T> extent, string location)
    {
        if (File.Exists(location))
        {
            var json = File.ReadAllText(location);
            var loaded = JsonSerializer.Deserialize<List<T>>(json);
            
            if (loaded != null)
                extent = loaded;
        }
        else
        {
            extent = new List<T>();
        }
    }
}