using System.Collections.Generic;
using System.Collections.ObjectModel;
using JackCraftLauncher.CrossPlatform.Class.Launch;
using JackCraftLauncher.CrossPlatform.Class.ListTemplate;
using ProjBobcat.Class.Model;

namespace JackCraftLauncher.CrossPlatform.Class;

public class GlobalVariable
{
    public static LaunchCoreHandler LaunchCore = new();
    public static List<VersionInfo> LocalGameList = new();
    public static List<string> LocalJavaList = new();

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
    
    public class ConfigVariable
    {
        public static DownloadSourceHandler.DownloadSourceEnum ConfigDownloadSourceEnum { get; set; } = DownloadSourceHandler.DownloadSourceEnum.MCBBS; // 下载源
    }
}