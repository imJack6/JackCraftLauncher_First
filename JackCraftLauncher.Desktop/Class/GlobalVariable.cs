using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using JackCraftLauncher.Desktop.Class.Launch;
using JackCraftLauncher.Desktop.Class.ListTemplate;
using JackCraftLauncher.Desktop.Class.Model;
using JackCraftLauncher.Desktop.Class.Utils;
using ProjBobcat.Class.Model;

namespace JackCraftLauncher.Desktop.Class;

public class GlobalVariable
{
    public static LaunchCoreHandler LaunchCore = new();
    public static List<VersionInfo> LocalGameList = new();
    public static List<string> LocalJavaList = new();

    public class ConfigVariable
    { 
        public static string MainConfigPath { get; } = $"{DirectoryUtils.GetSystemUserDirectory()}{Path.DirectorySeparatorChar}JackCraft{Path.DirectorySeparatorChar}Launcher{Path.DirectorySeparatorChar}Desktop{Path.DirectorySeparatorChar}config.json";
        public static ThemeModel ConfigThemeModel { get; set; } = ThemeModel.Dark; // 主题
        public static DownloadSourceHandler.DownloadSourceEnum ConfigDownloadSourceEnum { get; set; } =
            DownloadSourceHandler.DownloadSourceEnum.BMCL; // 下载源
    }

    #region 下载列表等

    public static ObservableCollection<DefaultDownloadList> ReleaseVersionDownloadList = new(); // 正式版下载列表
    public static ObservableCollection<DefaultDownloadList> SnapshotVersionDownloadList = new(); // 测试版下载列表
    public static ObservableCollection<DefaultDownloadList> OldVersionDownloadList = new(); // 远古版下载列表

    public static string LastetMinecraftReleaseVersion = "NULL"; // 最新正式版
    public static string LastetMinecraftSnapshotVersion = "NULL"; // 最新快照版

    public static List<string> MinecraftIDList = new(); // Minecraft ID 版本列表
    public static List<string> MinecraftTypeList = new(); // Minecraft Type 类型列表
    public static List<string> MinecraftUrlList = new(); // Minecraft Url 地址列表
    public static List<string> MinecraftTimeList = new(); // Minecraft 时间列表
    public static List<string> MinecraftReleaseTimeList = new(); // Minecraft 发布时间列表

    public static List<string> MinecraftReleaseList = new(); // Minecraft 正式版列表
    public static List<string> MinecraftSnapshotList = new(); // Minecraft 快照列表
    public static List<string> MinecraftOldList = new(); // Minecraft 远古列表

    #endregion
}