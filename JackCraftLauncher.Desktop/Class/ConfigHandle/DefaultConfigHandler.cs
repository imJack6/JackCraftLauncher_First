using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using JackCraftLauncher.Desktop.Class.Launch;
using JackCraftLauncher.Desktop.Class.Model;
using JackCraftLauncher.Desktop.Class.Model.ErrorModels;
using JackCraftLauncher.Desktop.Class.Utils;
using JackCraftLauncher.Desktop.Views.Menu;
using JackCraftLauncher.Desktop.Views.MyWindow;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjBobcat.Class.Model;

namespace JackCraftLauncher.Desktop.Class.ConfigHandle;

public class DefaultConfigHandler
{
    private static readonly string ConfigFilePath = GlobalVariable.ConfigVariable.MainConfigPath;

    public static void LoadSettingsConfig()
    {
        #region 启动器配置

        #region 主题加载

        var theme = (ThemeModel)GetConfig(GlobalConstants.ConfigThemeNode);
        if (theme is not (ThemeModel.Light or ThemeModel.Dark))
        {
            GlobalVariable.ConfigVariable.ConfigThemeModel = ThemeModel.Dark;
            SetConfig(GlobalConstants.ConfigThemeNode, ThemeModel.Dark);
        }
        else
        {
            GlobalVariable.ConfigVariable.ConfigThemeModel = theme;
            SettingsUserControl.Instance!.ThemeSelectComboBox.SelectedIndex = (int)theme;
        }

        #endregion

        #endregion

        #region 下载配置

        #region 下载源加载

        var downloadSource =
            (DownloadSourceHandler.DownloadSourceEnum)GetConfig(GlobalConstants.ConfigDownloadSourceNode);
        if (downloadSource is not (DownloadSourceHandler.DownloadSourceEnum.Official
            or DownloadSourceHandler.DownloadSourceEnum.BMCL or DownloadSourceHandler.DownloadSourceEnum.MCBBS))
        {
            GlobalVariable.ConfigVariable.ConfigDownloadSourceEnum = DownloadSourceHandler.DownloadSourceEnum.BMCL;
            SetConfig(GlobalConstants.ConfigDownloadSourceNode, DownloadSourceHandler.DownloadSourceEnum.BMCL);
        }
        else
        {
            GlobalVariable.ConfigVariable.ConfigDownloadSourceEnum = downloadSource;
            SettingsUserControl.Instance!.DownloadSourceSelectComboBox.SelectedIndex = (int)downloadSource;
        }

        #endregion

        #region 下载并行数加载

        var parallelismCount = (int)GetConfig(GlobalConstants.ConfigDownloadParallelismCountNode);
        if (parallelismCount < 1 || parallelismCount > 16)
        {
            GlobalVariable.ConfigVariable.ConfigDownloadParallelismCount = 8;
            SetConfig(GlobalConstants.ConfigDownloadParallelismCountNode, 8);
        }
        else
        {
            GlobalVariable.ConfigVariable.ConfigDownloadParallelismCount = parallelismCount;
            SettingsUserControl.Instance!.DownloadParallelismCountSlider.Value = parallelismCount;
        }

        #endregion

        #region 下载线程(分片)数加载

        var downloadThreadCount = (int)GetConfig(GlobalConstants.ConfigDownloadThreadCountNode);
        if (downloadThreadCount < 1 || downloadThreadCount > 32)
        {
            GlobalVariable.ConfigVariable.ConfigDownloadThreadCount = 8;
            SetConfig(GlobalConstants.ConfigDownloadThreadCountNode, 8);
        }
        else
        {
            GlobalVariable.ConfigVariable.ConfigDownloadThreadCount = downloadThreadCount;
            SettingsUserControl.Instance!.DownloadSegmentsForLargeFileSlider.Value = downloadThreadCount;
        }

        #endregion

        #region 下载重试次数加载

        var downloadRetryCount = (int)GetConfig(GlobalConstants.ConfigDownloadRetryCountNode);
        if (downloadRetryCount < 0 || downloadRetryCount > 10)
        {
            GlobalVariable.ConfigVariable.ConfigDownloadRetryCount = 2;
            SetConfig(GlobalConstants.ConfigDownloadRetryCountNode, 2);
        }
        else
        {
            GlobalVariable.ConfigVariable.ConfigDownloadRetryCount = downloadRetryCount;
            SettingsUserControl.Instance!.DownloadTotalRetrySlider.Value = downloadRetryCount;
        }

        #endregion

        #endregion

        #region 游戏配置

        #region 启动Java加载

        var startJavaPathList =
            (List<string>)GetConfig(GlobalConstants.ConfigJavaPathListNode);
        var startJavaIndex =
            (int)GetConfig(GlobalConstants.ConfigSelectedJavaIndexNode);
        GlobalVariable.LocalJavaList = startJavaPathList;
        GlobalVariable.ConfigVariable.ConfigGameStartJavaIndex = startJavaIndex;
        GlobalVariable.ConfigVariable.ConfigGameStartJavaPath = startJavaPathList[startJavaIndex];
        SettingsUserControl.Instance!.StartJavaSelectComboBox.ItemsSource = startJavaPathList;
        SettingsUserControl.Instance!.StartJavaSelectComboBox.SelectedIndex = startJavaIndex;

        #endregion

        #region GcType加载

        var gameGcType = (GcType)GetConfig(GlobalConstants.ConfigGameGcTypeNode);
        if ((int)gameGcType < 0 || (int)gameGcType > 5)
        {
            GlobalVariable.ConfigVariable.ConfigGameGcType = GcType.G1Gc;
            SetConfig(GlobalConstants.ConfigGameGcTypeNode, GcType.G1Gc);
        }
        else
        {
            GlobalVariable.ConfigVariable.ConfigGameGcType = gameGcType;
            SettingsUserControl.Instance!.GameGcTypeSelectComboBox.SelectedIndex = (int)gameGcType;
        }

        #endregion

        #region 分辨率加载

        var resolutionWidth = (uint)GetConfig(GlobalConstants.ConfigGameResolutionWidthNode);
        var resolutionHeight = (uint)GetConfig(GlobalConstants.ConfigGameResolutionHeightNode);
        GlobalVariable.ConfigVariable.ConfigGameResolutionWidth = resolutionWidth;
        GlobalVariable.ConfigVariable.ConfigGameResolutionHeight = resolutionHeight;
        SettingsUserControl.Instance!.GameResolutionWidthTextBox.Text = resolutionWidth.ToString();
        SettingsUserControl.Instance!.GameResolutionHeightTextBox.Text = resolutionHeight.ToString();

        #endregion

        #endregion
    }

    #region 配置文件操作

    public static Config LoadConfig()
    {
        DirectoryUtils.CreateDirectory(Path.GetDirectoryName(ConfigFilePath)!);
        if (!File.Exists(ConfigFilePath)) return new Config();

        try
        {
            var json = File.ReadAllText(ConfigFilePath);
            if (string.IsNullOrWhiteSpace(json))
                SaveConfig(new Config());
            return JsonConvert.DeserializeObject<Config>(json)!;
        }
        catch (Exception ex)
        {
            MyErrorWindow.CreateErrorWindow(
                ErrorType.InternalError,
                "加载失败",
                "加载配置文件失败 - 可能原因 1.配置文件损坏 2.配置文件被篡改 3.没有权限",
                "1.删除配置文件后重启程序 2.给予权限",
                ex);
            return new Config();
        }
    }

    public static void SaveConfig(Config config)
    {
        if (!File.Exists(ConfigFilePath))
        {
            var fileStream = File.Create(ConfigFilePath);
            fileStream.Close();
        }

        var json = JsonConvert.SerializeObject(config);
        File.WriteAllText(ConfigFilePath, JObject.Parse(json).ToString());
    }

    public static object GetConfig(string propertyName)
    {
        var config = LoadConfig();
        var propertyNames = propertyName.Split('.');
        object obj = config;
        var type = typeof(Config);
        for (var i = 0; i < propertyNames.Length; i++)
        {
            var property = type.GetProperty(propertyNames[i]);
            if (property == null)
            {
                MyErrorWindow.CreateErrorWindow(
                    ErrorType.InternalError,
                    "内部错误",
                    $"{type.Name} 类中找不到属性 {propertyNames[i]} - 可能原因 1.程序被篡改 2.开发者出错",
                    "1.重试 2.联系开发者 2.重新下载程序",
                    new ArgumentException($"{type.Name} 类中找不到属性 {propertyNames[i]}"));
                return null!;
            }

            obj = GetConfigValue(obj, property);
            type = property.PropertyType;
        }

        SaveConfig(config);
        return obj;
    }

    public static void SetConfig(string propertyName, object value)
    {
        var config = LoadConfig();
        var propertyNames = propertyName.Split('.');
        object obj = config;
        var type = typeof(Config);
        for (var i = 0; i < propertyNames.Length - 1; i++)
        {
            var property = type.GetProperty(propertyNames[i]);
            if (property == null)
            {
                MyErrorWindow.CreateErrorWindow(
                    ErrorType.InternalError,
                    "内部错误",
                    $"{type.Name} 类中找不到属性 {propertyNames[i]} - 可能原因 1.程序被篡改 2.开发者出错",
                    "1.重试 2.联系开发者 2.重新下载程序",
                    new ArgumentException($"{type.Name} 类中找不到属性 {propertyNames[i]}"));
                return;
            }

            obj = GetConfigValue(obj, property);
            type = property.PropertyType;
        }

        var lastProperty = type.GetProperty(propertyNames[propertyNames.Length - 1]);
        if (lastProperty == null)
        {
            MyErrorWindow.CreateErrorWindow(
                ErrorType.InternalError,
                "内部错误",
                $"{type.Name} 类中找不到属性 {propertyNames[propertyNames.Length - 1]} - 可能原因 1.程序被篡改 2.开发者出错",
                "1.重试 2.联系开发者 2.重新下载程序",
                new ArgumentException($"{type.Name} 类中找不到属性 {propertyNames[propertyNames.Length - 1]}"));
            return;
        }

        lastProperty.SetValue(obj, value);
        SaveConfig(config);
    }

    private static object GetConfigValue(object obj, PropertyInfo property)
    {
        var value = property.GetValue(obj);
        if (value == null)
        {
            value = Activator.CreateInstance(property.PropertyType);
            property.SetValue(obj, value);
        }

        return value!;
    }

    #endregion

    #region 配置文件模型

    public class Config
    {
        public LauncherSettings LauncherSettings { get; set; } = new();
        public DownloadSettings DownloadSettings { get; set; } = new();
        public GlobalGameSettings GlobalGameSettings { get; set; } = new();
    }

    public class LauncherSettings
    {
        public ThemeModel Theme { get; set; } = ThemeModel.Dark;
    }

    public class DownloadSettings
    {
        public DownloadSourceHandler.DownloadSourceEnum DownloadSource { get; set; } =
            DownloadSourceHandler.DownloadSourceEnum.BMCL;

        public int ParallelismCount { get; set; } = 8;
        public int ThreadCount { get; set; } = 8;
        public int RetryCount { get; set; } = 2;
    }

    public class GlobalGameSettings
    {
        public int SelectedJavaIndex { get; set; } = -1;
        public List<string> JavaPathList { get; set; } = new();
        public GcType GcType { get; set; } = GcType.G1Gc;
        public uint ResolutionWidth { get; set; } = 800;
        public uint ResolutionHeight { get; set; } = 450;
    }

    #endregion
}