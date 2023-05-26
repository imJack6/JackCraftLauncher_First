using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using JackCraftLauncher.CrossPlatform.Class;
using JackCraftLauncher.CrossPlatform.Class.Launch;
using JackCraftLauncher.CrossPlatform.Class.ListTemplate;
using JackCraftLauncher.CrossPlatform.Class.Utils;
using ProjBobcat.Class.Helper;
using ProjBobcat.Class.Model;

namespace JackCraftLauncher.CrossPlatform.Views.Menu;

public partial class DownloadUserControl : UserControl
{
    public static DownloadUserControl? Instance;
    public string InstallVersionName = "1.0";

    public DownloadUserControl()
    {
        Instance = this;
        InitializeComponent();
        GameHandler.RefreshLocalMinecraftDownloadList();
    }

    private void LatestReleaseVersionButton_OnClick(object? sender, RoutedEventArgs e)
    {
        GoToDownloadInstallTabItem(GlobalVariable.LastetMinecraftReleaseVersion);
        InstallVersionName = GlobalVariable.LastetMinecraftReleaseVersion;
    }

    private void LatestSnapshotVersionButton_OnClick(object? sender, RoutedEventArgs e)
    {
        GoToDownloadInstallTabItem(GlobalVariable.LastetMinecraftSnapshotVersion);
        InstallVersionName = GlobalVariable.LastetMinecraftSnapshotVersion;
    }

    private async void InstallStartButton_OnClick(object? sender, RoutedEventArgs e)
    {
        DownloadTabControl.SelectedIndex = 2;

        #region 初始化

        InstallStatusLogTextBox.Text =
            $"[初始化] 开始安装 {DownloadSaveVersionNameTextBox.Text} ({InstallVersionName}){Environment.NewLine}";
        var folderPath =
            $"{GlobalVariable.LaunchCore.GetCore().RootPath}{Path.DirectorySeparatorChar}versions{Path.DirectorySeparatorChar}{DownloadSaveVersionNameTextBox.Text}";
        InstallStatusLogTextBox.Text += $"[初始化] 创建文件夹 {folderPath}{Environment.NewLine}";
        DirectoryUtils.CreateDirectory(folderPath);

        #endregion

        #region 下载Json

        InstallStatusLogTextBox.Text +=
            $"[下载] - [开始] JSON - {DownloadSaveVersionNameTextBox.Text}.json {Environment.NewLine}";
        var versionIndexOf = GlobalVariable.MinecraftIDList.IndexOf(InstallVersionName);
        var minecraftJsonUrl = GlobalVariable.MinecraftUrlList[versionIndexOf];
        minecraftJsonUrl =
            DownloadSourceHandler.PistonMetaUrlHandle(GlobalVariable.ConfigVariable.ConfigDownloadSourceEnum,
                minecraftJsonUrl);
        var downloadSettings = new DownloadSettings
        {
            DownloadParts = 32,
            RetryCount = 2,
            CheckFile = true,
            Timeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds
        };
        var downloadFile = new DownloadFile
        {
            DownloadUri = minecraftJsonUrl,
            FileName = $"{DownloadSaveVersionNameTextBox.Text}.json",
            DownloadPath = folderPath,
            RetryCount = 2
        };
        await DownloadHelper.AdvancedDownloadFile(downloadFile, downloadSettings);
        InstallStatusLogTextBox.Text +=
            $"[下载] - [完成] JSON - {DownloadSaveVersionNameTextBox.Text}.json 下载完成 {Environment.NewLine}";

        #endregion

        #region 安装完成

        GameHandler.RefreshLocalGameList();
        InstallStatusLogTextBox.Text +=
            $"[安装] - [完成] {DownloadSaveVersionNameTextBox.Text} ({InstallVersionName}) 安装完成 {Environment.NewLine}";
        var timeBack = 3;
        for (var i = timeBack; i > 0; i--)
        {
            InstallStatusLogTextBox.Text += $">>> {i} 秒后返回 <<< {Environment.NewLine}";
            await Task.Delay(1000);
        }

        DownloadTabControl.SelectedIndex = 0;

        #endregion
    }

    public void GoToDownloadInstallTabItem(string mcVersion)
    {
        InstallMinecraftVersionTextBlock.Text = mcVersion;
        DownloadSaveVersionNameTextBox.Text = mcVersion;
        DownloadTabControl.SelectedIndex = 1;
    }

    private void DownloadSaveVersionNameTextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (DownloadSaveVersionNameTextBox.Text!.Length > 0)
        {
            var folderPath =
                $"{GlobalVariable.LaunchCore.GetCore().RootPath}{Path.DirectorySeparatorChar}versions{Path.DirectorySeparatorChar}{DownloadSaveVersionNameTextBox.Text}";
            if (Directory.Exists(folderPath))
            {
                DownloadSavePathTextBlock.Text = $"文件夹 {DownloadSaveVersionNameTextBox.Text} 已存在";
                InstallStartButton.IsEnabled = false;
            }
            else
            {
                DownloadSavePathTextBlock.Text = $"安装在 {DownloadSaveVersionNameTextBox.Text}";
                InstallStartButton.IsEnabled = true;
            }
        }
        else
        {
            DownloadSavePathTextBlock.Text = "请键入版本名字";
            InstallStartButton.IsEnabled = false;
        }
    }

    private void BackToSelectVersionTabItem_Click(object sender, RoutedEventArgs e)
    {
        DownloadTabControl.SelectedIndex = 0;
    }

    private void VersionListBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var listBox = (ListBox)sender!;
        if (listBox.SelectedIndex != -1)
        {
            var defaultDownloadList = (DefaultDownloadList)listBox.SelectedItem!;
            var selectVersion = defaultDownloadList.Version;
            GoToDownloadInstallTabItem(selectVersion);
            InstallVersionName = selectVersion;
            listBox.SelectedIndex = -1;
        }
    }
}