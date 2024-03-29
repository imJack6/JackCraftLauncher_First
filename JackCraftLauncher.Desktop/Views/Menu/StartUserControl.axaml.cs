﻿using Avalonia.Controls;
using Avalonia.Interactivity;
using JackCraftLauncher.Desktop.Class;
using JackCraftLauncher.Desktop.Class.Launch;
using JackCraftLauncher.Desktop.Views.MyControls;
using ProjBobcat.Class.Model;
using ProjBobcat.DefaultComponent.Launch.GameCore;

namespace JackCraftLauncher.Desktop.Views.Menu;

public partial class StartUserControl : UserControl
{
    public static StartUserControl Instance;

    public StartUserControl()
    {
        Instance = this;
        InitializeComponent();
        GameHandler.RefreshLocalGameList();
    }

    private async void StartJavaGameButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (SelectJavaGameVersionComboBox.SelectedIndex == -1)
            await MyDialog.ShowDialog(MainWindow.Instance.RootGrid, "提示", "没有选择启动版本");
        else if (SettingsUserControl.Instance.StartJavaSelectComboBox.SelectedIndex == -1)
            await MyDialog.ShowDialog(MainWindow.Instance.RootGrid, "提示", "没有选择启动 Java");
        else
            GameHandler.StartGame(GlobalVariable.LocalGameList[SelectJavaGameVersionComboBox.SelectedIndex]);
    }

    private void StartBedrockGameButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var launchsettings = new LaunchSettings();
        var core = new DefaultMineCraftUWPCore();
        core.Launch(launchsettings);
    }

    private void RefreshLocalGameComboListButton_OnClick(object? sender, RoutedEventArgs e)
    {
        GameHandler.RefreshLocalGameList();
    }
}