using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using JackCraftLauncher.CrossPlatform.Class;
using JackCraftLauncher.CrossPlatform.Class.Utils;

namespace JackCraftLauncher.CrossPlatform.Views.Menu;

public partial class DownloadUserControl : UserControl
{
    public static DownloadUserControl? Instance;
    public string InstallVersionName = "1.0";
    
    public DownloadUserControl()
    {
        Instance = this;
        InitializeComponent();
        //GameHandler.RefreshLocalMinecraftDownloadList();
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
    
    private void InstallStartButton_OnClick(object? sender, RoutedEventArgs e)
    {
        DownloadTabControl.SelectedIndex = 2;
        
        InstallStatusLogTextBox.Text = "";
        InstallStatusLogTextBox.Text += $"[安装] - [初始化]： 开始安装 {DownloadSaveVersionNameTextBox.Text} ({InstallVersionName}){Environment.NewLine}";
        /*InstallStatusLogTextBox.Text += $"[安装] - [初始化]： 创建文件夹 {GlobalVariable.minecraftVersionDirectory}{DownloadSaveVersionNameTextBox.Text} {Environment.NewLine}";
        DirectoryUtils.CreateDirectory($"{GlobalVariable.minecraftVersionDirectory}{DownloadSaveVersionNameTextBox.Text}");*/
        InstallStatusLogTextBox.Text += $"[安装] - [开始下载]： JSON - {DownloadSaveVersionNameTextBox.Text}.json {Environment.NewLine}";
        

    }

    public void GoToDownloadInstallTabItem(string MCVersion)
    {
        InstallMinecraftVersionTextBlock.Text = MCVersion;
        DownloadSaveVersionNameTextBox.Text = MCVersion;
        DownloadTabControl.SelectedIndex = 1;
    }
}