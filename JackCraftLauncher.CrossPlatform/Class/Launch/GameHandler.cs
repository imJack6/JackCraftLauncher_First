using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using Flurl.Http;
using JackCraftLauncher.CrossPlatform.Class.ListTemplate;
using JackCraftLauncher.CrossPlatform.Views.Menu;
using JackCraftLauncher.CrossPlatform.Views.MyWindow;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjBobcat.Class.Helper;
using ProjBobcat.Class.Model;
using ProjBobcat.Class.Model.LauncherProfile;
using ProjBobcat.Class.Model.Mojang;
using ProjBobcat.DefaultComponent;
using ProjBobcat.DefaultComponent.Authenticator;
using ProjBobcat.DefaultComponent.Launch.GameCore;
using ProjBobcat.DefaultComponent.Logging;
using ProjBobcat.DefaultComponent.ResourceInfoResolver;
using ProjBobcat.Interface;

namespace JackCraftLauncher.CrossPlatform.Class.Launch;

public class GameHandler
{
    public static void RefreshLocalGameList()
    {
        GlobalVariable.LocalGameList = GlobalVariable.LaunchCore.GetCore().VersionLocator.GetAllGames().ToList();
        StartUserControl.Instance.SelectJavaGameVersionComboBox.ItemsSource =
            GlobalVariable.LocalGameList.Select(x => x.Name ?? x.Id);
    }

    public static async void RefreshLocalJavaList(bool fullSearch = false)
    {
        SettingsUserControl.Instance.StartJavaSelectComboBox.PlaceholderText = "正在搜寻Java，此过程可能需要花费较长时间";
        SettingsUserControl.Instance.RefreshLocalJavaComboBoxButton.IsEnabled = false;
        var javaList = SystemInfoHelper.FindJava(fullSearch);
        GlobalVariable.LocalJavaList = await javaList.ToListAsync();
        SettingsUserControl.Instance.StartJavaSelectComboBox.ItemsSource = GlobalVariable.LocalJavaList;
        SettingsUserControl.Instance.StartJavaSelectComboBox.PlaceholderText = "选择 Java";
        SettingsUserControl.Instance.RefreshLocalJavaComboBoxButton.IsEnabled = true;
    }

    public static async void RefreshLocalMinecraftDownloadList(bool updateUi = true)
    {
        #region 清空旧数组

        GlobalVariable.MinecraftReleaseList.Clear();
        GlobalVariable.MinecraftSnapshotList.Clear();
        GlobalVariable.MinecraftOldList.Clear();

        GlobalVariable.MinecraftIDList.Clear();
        GlobalVariable.MinecraftTypeList.Clear();
        GlobalVariable.MinecraftUrlList.Clear();
        GlobalVariable.MinecraftTimeList.Clear();
        GlobalVariable.MinecraftReleaseTimeList.Clear();

        GlobalVariable.ReleaseVersionDownloadList.Clear();
        GlobalVariable.SnapshotVersionDownloadList.Clear();
        GlobalVariable.OldVersionDownloadList.Clear();

        #endregion

        #region 获取版本列表

        var result = await DownloadSourceHandler
            .GetDownloadSource(DownloadSourceHandler.DownloadTargetEnum.VersionInfoV1, null).GetStringAsync();

        #endregion

        #region 解析版本列表

        var jObject = JObject.Parse(result);
        GlobalVariable.LastetMinecraftReleaseVersion = jObject["latest"]!["release"]!.ToString();
        GlobalVariable.LastetMinecraftSnapshotVersion = jObject["latest"]!["snapshot"]!.ToString();
        var allMinecraftList = JArray.Parse(jObject["versions"]!.ToString());
        foreach (var mc in allMinecraftList)
        {
            var MinecraftList = JObject.Parse(mc.ToString());
            var MinecraftId = MinecraftList["id"].ToString();
            var MinecraftType = MinecraftList["type"].ToString();
            var MinecraftUrl = MinecraftList["url"].ToString();
            var MinecraftTime = MinecraftList["time"].ToString();
            var MinecraftReleaseTime = MinecraftList["releaseTime"].ToString();
            // 4.添加到数组
            GlobalVariable.MinecraftIDList.Add(MinecraftId);
            GlobalVariable.MinecraftTypeList.Add(MinecraftType);
            GlobalVariable.MinecraftUrlList.Add(MinecraftUrl);
            GlobalVariable.MinecraftTimeList.Add(MinecraftTime);
            GlobalVariable.MinecraftReleaseTimeList.Add(MinecraftReleaseTime);

            if (MinecraftType == "release")
            {
                GlobalVariable.MinecraftReleaseList.Add(MinecraftId);
                var ReleaseTime = "正式版  发布日期: " + MinecraftReleaseTime;
                GlobalVariable.ReleaseVersionDownloadList.Add(new DefaultDownloadList(MinecraftId, ReleaseTime));
            }
            else if (MinecraftType == "snapshot")
            {
                GlobalVariable.MinecraftSnapshotList.Add(MinecraftId);
                var ReleaseTime = "测试版  发布日期: " + MinecraftReleaseTime;
                GlobalVariable.SnapshotVersionDownloadList.Add(new DefaultDownloadList(MinecraftId, ReleaseTime));
            }
            else if (MinecraftType == "old_alpha")
            {
                GlobalVariable.MinecraftOldList.Add(MinecraftId);
                var ReleaseTime = "远古版  发布日期: " + MinecraftReleaseTime;
                GlobalVariable.OldVersionDownloadList.Add(new DefaultDownloadList(MinecraftId, ReleaseTime));
            }
            else if (MinecraftType == "old_beta")
            {
                GlobalVariable.MinecraftOldList.Add(MinecraftId);
                var ReleaseTime = "远古版  发布日期: " + MinecraftReleaseTime;
                GlobalVariable.OldVersionDownloadList.Add(new DefaultDownloadList(MinecraftId, ReleaseTime));
            }
        }

        if (updateUi)
        {
            DownloadUserControl.Instance.ReleaseVersionListBox.ItemsSource = GlobalVariable.ReleaseVersionDownloadList;
            DownloadUserControl.Instance.SnapshotVersionListBox.ItemsSource =
                GlobalVariable.SnapshotVersionDownloadList;
            DownloadUserControl.Instance.OldVersionListBox.ItemsSource = GlobalVariable.OldVersionDownloadList;

            if (GlobalVariable.LastetMinecraftReleaseVersion == GlobalVariable.LastetMinecraftSnapshotVersion)
                DownloadUserControl.Instance.LatestSnapshotVersionButton.IsVisible = false;
            DownloadUserControl.Instance.LatestReleaseVersionTextBlock.Text =
                GlobalVariable.LastetMinecraftReleaseVersion;
            DownloadUserControl.Instance.LatestSnapshotVersionTextBlock.Text =
                GlobalVariable.LastetMinecraftSnapshotVersion;

            var LatestReleaseVersionIndex =
                GlobalVariable.MinecraftIDList.IndexOf(GlobalVariable.LastetMinecraftReleaseVersion);
            if (LatestReleaseVersionIndex != -1)
                DownloadUserControl.Instance.LatestReleaseVersionTextBlock.Text += "\n正式版  发布日期: " +
                    GlobalVariable.MinecraftReleaseTimeList[LatestReleaseVersionIndex];

            var LatestSnapshotVersionIndex =
                GlobalVariable.MinecraftIDList.IndexOf(GlobalVariable.LastetMinecraftSnapshotVersion);
            if (LatestSnapshotVersionIndex != -1)
                DownloadUserControl.Instance.LatestSnapshotVersionTextBlock.Text += "\n测试版  发布日期: " +
                    GlobalVariable.MinecraftReleaseTimeList[LatestSnapshotVersionIndex];
        }

        #endregion
    }

    public static async void StartGame(VersionInfo versionInfo)
    {
        var core = new DefaultGameCore
        {
            ClientToken = GlobalVariable.LaunchCore.GetCore().ClientToken,
            RootPath = GlobalVariable.LaunchCore.GetCore().RootPath,
            VersionLocator = GlobalVariable.LaunchCore.GetCore().VersionLocator,
            GameLogResolver = new DefaultGameLogResolver()
        };
        var resourceCompletion = await GetResourceCompletion(versionInfo);
        
        var launchSettings = new LaunchSettings
        {
            FallBackGameArguments =
                new GameArguments // 游戏启动参数缺省值，适用于以该启动设置启动的所有游戏，对于具体的某个游戏，可以设置（见下）具体的启动参数，如果所设置的具体参数出现缺失，将使用这个补全
                {
                    GcType = GcType.G1Gc, // GC类型
                    JavaExecutable =
                        SettingsUserControl.Instance.StartJavaSelectComboBox.SelectedItem!.ToString()!, // Java路径
                    Resolution = new ResolutionModel // 游戏窗口分辨率
                    {
                        Height = 600, // 高度
                        Width = 800 // 宽度
                    },
                    MinMemory = 2048, // 最小内存
                    MaxMemory = 4196 // 最大内存
                },
            Version = versionInfo.Id!, // 需要启动的游戏ID，例如1.7.10或者1.15.2
            //VersionInsulation = false, // 版本隔离
            //GamePath = core.RootPath!, // 游戏根目录
            VersionInsulation = true, // 版本隔离
            GamePath = GamePathHelper.GetGamePath(versionInfo.Id!), // 游戏根目录
            GameResourcePath = core.RootPath!, // 资源根目录
            GameName = versionInfo.Name, // 游戏名称
            VersionLocator = core.VersionLocator, // 游戏定位器 
            LauncherName = "JackCraft Launcher"
        };
        launchSettings.Authenticator = new OfflineAuthenticator
        {
            Username = "JCLTest",
            LauncherAccountParser = core.VersionLocator.LauncherAccountParser // launcher_profiles.json解析组件
        };
        var logWindow = new ViewLogWindow();
        core.LaunchLogEventDelegate += (sender, args) =>
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (logWindow.IsVisible)
                    logWindow.AddLog($"[ 启动器 ] {args.Item}");
            });
        };
        core.GameLogEventDelegate += (sender, args) =>
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (logWindow.IsVisible)
                    logWindow.AddLog($"[ 游戏 ] {args.RawContent}");
            });
        };
        core.GameExitEventDelegate += (sender, args) =>
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (logWindow.IsVisible)
                    logWindow.AddLog("[ 启动器 ] 游戏已退出");
            });
        };
        
        logWindow.Show();
        resourceCompletion.DownloadFileCompletedEvent += (sender, args) =>
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (sender is not DownloadFile file) return;
                if (logWindow.IsVisible)
                {
                    var isSuccess = args.Success == null
                        ? string.Empty
                        : $"[{(args.Success.Value ? "成功" : "失败")}]";
                    var retry = file.RetryCount == 0
                        ? string.Empty
                        : $"<Retry - {file.RetryCount}>";
                    var fileName = file.FileType switch
                    {
                        ProjBobcat.Class.Model.ResourceType.Asset => file.FileName.AsSpan()[..10],
                        ProjBobcat.Class.Model.ResourceType.LibraryOrNative => file.FileName,
                        _ => file.FileName
                    };
                    var pD =
                        //$"[{file.FileType} 已完成] - {retry}{isSuccess} {fileName.ToString()} ({resourceCompletion.TotalDownloaded} / {resourceCompletion.NeedToDownload}) [{args.AverageSpeed:F} Kb / s]";
                        $"[{file.FileType} 已完成] - {retry}{isSuccess} - {fileName.ToString()} - ({resourceCompletion.TotalDownloaded} / {resourceCompletion.NeedToDownload})";
                    logWindow.AddLog($"[ 资源补全 ] {pD}");
                }
            });
        };

        await resourceCompletion.CheckAndDownloadTaskAsync().ConfigureAwait(false);
        var result = await core.LaunchTaskAsync(launchSettings).ConfigureAwait(true); // 返回游戏启动结果，以及异常信息（如果存在）
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (logWindow.IsVisible)
            {
                logWindow.LaunchResult = result;
                logWindow.Title = $"日志显示 (PID:{result.GameProcess!.Id})";
                logWindow.TitleTextBlock.Text = $"日志显示 (PID:{result.GameProcess!.Id})";
            }
        });
    }

    public static async Task<DefaultResourceCompleter> GetResourceCompletion(VersionInfo versionInfo)
    {
        var versionManifest = await DownloadSourceHandler
            .GetDownloadSource(DownloadSourceHandler.DownloadTargetEnum.VersionInfoV1, null).GetStringAsync();
        var manifest = JsonConvert.DeserializeObject<VersionManifest>(versionManifest);
        var versions = manifest!.Versions;
        var basePath = GlobalVariable.LaunchCore.GetCore().RootPath!;
        // Assets 解析器
        var resolverAsset = new AssetInfoResolver
        {
            AssetIndexUriRoot = DownloadSourceHandler
                .GetDownloadSource(DownloadSourceHandler.DownloadTargetEnum.MinecraftAssetsIndex, null),
            AssetUriRoot = DownloadSourceHandler
                .GetDownloadSource(DownloadSourceHandler.DownloadTargetEnum.MinecraftAssets, null),
            BasePath = basePath,
            VersionInfo = versionInfo,
            CheckLocalFiles = true,
            Versions = versions // 在上一步获取到的 Versions 数组
        };
        // log4j 日志格式化组件解析器
        var resolverLogging = new GameLoggingInfoResolver
        {
            BasePath = basePath,
            VersionInfo = versionInfo,
            CheckLocalFiles = true
        };
        // Libraries 解析器
        var resolverLibrary = new LibraryInfoResolver
        {
            BasePath = basePath,
            ForgeUriRoot = "https://files.minecraftforge.net/maven/",
            ForgeMavenUriRoot = "https://maven.minecraftforge.net/",
            ForgeMavenOldUriRoot = "https://files.minecraftforge.net/maven/",
            FabricMavenUriRoot = "https://maven.fabricmc.net/",
            LibraryUriRoot = DownloadSourceHandler
                .GetDownloadSource(DownloadSourceHandler.DownloadTargetEnum.MinecraftLibraries, null),
            VersionInfo = versionInfo,
            CheckLocalFiles = true
        };
        // 版本信息解析器
        var resolverVersion = new VersionInfoResolver
        {
            BasePath = basePath,
            VersionInfo = versionInfo,
            CheckLocalFiles = true
        };
        // 资源补全器
        var completer = new DefaultResourceCompleter
        {
            MaxDegreeOfParallelism = Convert.ToInt16(SettingsUserControl.Instance.MaxDegreeOfParallelismSlider.Value),
            ResourceInfoResolvers = new List<IResourceInfoResolver>
            {
                resolverAsset,
                resolverLogging,
                resolverLibrary,
                resolverVersion
            },
            TotalRetry = Convert.ToInt16(SettingsUserControl.Instance.TotalRetrySlider.Value),
            CheckFile = true,
            DownloadParts = Convert.ToInt16(SettingsUserControl.Instance.TotalDownloadSegmentsForLargeFileSlider.Value)
        };

        return completer;
    }
}