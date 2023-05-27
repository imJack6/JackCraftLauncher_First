using System.IO;
using Newtonsoft.Json.Linq;

namespace JackCraftLauncher.Desktop.Class.Launch;

public class ConfigHandler
{
    public static void loadConfig()
    {
        if (!File.Exists(GlobalVariable.ConfigVariable.MainConfigPath))
        {
            File.WriteAllText(GlobalVariable.ConfigVariable.MainConfigPath, "{}");
        }
    }
    public static string GetConfig(string key)
    {
        string config = File.ReadAllText(GlobalVariable.ConfigVariable.MainConfigPath);
        JObject jObject = JObject.Parse(config);
        return jObject[key].ToString();
    }
}