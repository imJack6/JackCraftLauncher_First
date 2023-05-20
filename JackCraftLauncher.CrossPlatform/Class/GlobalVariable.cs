using System.Collections.Generic;
using System.Linq;
using JackCraftLauncher.CrossPlatform.Class.Launch;
using ProjBobcat.Class.Model;

namespace JackCraftLauncher.CrossPlatform.Class;

public static class GlobalVariable
{
    public static LaunchCoreHandler LaunchCore = new LaunchCoreHandler();
    public static List<VersionInfo> LocalGameList = new List<VersionInfo>();
    public static List<string> LocalJavaList = new List<string>();
}