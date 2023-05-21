using System.Collections.Generic;
using JackCraftLauncher.CrossPlatform.Class.Launch;
using ProjBobcat.Class.Model;

namespace JackCraftLauncher.CrossPlatform.Class;

public static class GlobalVariable
{
    public static LaunchCoreHandler LaunchCore = new();
    public static List<VersionInfo> LocalGameList = new();
    public static List<string> LocalJavaList = new();
}