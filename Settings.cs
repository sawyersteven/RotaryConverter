using System;
using System.IO;
using System.Text.Json;

public class Settings
{
    private readonly string filePath = Path.Join(Path.GetDirectoryName(Environment.ProcessPath), "RotaryConverter.settings");

    public class UserSettings
    {
        public double RotaryLength { get; set; } = 100;
    }

    public UserSettings User;

    public Settings()
    {
        User = new UserSettings();
        try
        {
            string json = File.ReadAllText(filePath);
            User = JsonSerializer.Deserialize<UserSettings>(json);
            if (User == null) throw new FormatException();
        }
        catch
        {
            User = new UserSettings();
        }
    }

    public void Write()
    {
        string json = JsonSerializer.Serialize(User);
        File.WriteAllText(filePath, json, System.Text.Encoding.UTF8);
    }
}
