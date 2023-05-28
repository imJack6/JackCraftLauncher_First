using System;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Styling;
using JackCraftLauncher.Desktop.Class;
using JackCraftLauncher.Desktop.Class.ConfigHandle;
using JackCraftLauncher.Desktop.Class.Launch;
using JackCraftLauncher.Desktop.Class.Model;
using JackCraftLauncher.Desktop.Views.Themes;
using ProjBobcat.Class.Model;

namespace JackCraftLauncher.Desktop.Views.Menu;

public partial class SettingsUserControl : UserControl
{
    public static SettingsUserControl? Instance;

    public SettingsUserControl()
    {
        Instance = this;
        InitializeComponent();
        //GameHandler.RefreshLocalJavaList();
    }

    private void ThemeSelectComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ThemeSelectComboBox != null)
        {
            switch (ThemeSelectComboBox.SelectedIndex)
            {
                case 0:
                    ThemeHandler.SetTheme(ThemeVariant.Light);
                    break;
                case 1:
                    ThemeHandler.SetTheme(ThemeVariant.Dark);
                    break;
            }

            GlobalVariable.ConfigVariable.ConfigThemeModel = (ThemeModel)ThemeSelectComboBox.SelectedIndex;
            DefaultConfigHandler.SetConfig(GlobalConstants.ConfigThemeNode,
                GlobalVariable.ConfigVariable.ConfigThemeModel);
        }
    }

    private void StartJavaSelectComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (StartJavaSelectComboBox != null)
        {
            GlobalVariable.ConfigVariable.ConfigGameStartJavaIndex = StartJavaSelectComboBox.SelectedIndex;
            DefaultConfigHandler.SetConfig(GlobalConstants.ConfigSelectedJavaIndexNode,
                GlobalVariable.ConfigVariable.ConfigGameStartJavaIndex);
        }
    }

    private void DownloadSourceSelectComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DownloadSourceSelectComboBox != null && DownloadSourceSelectComboBox.SelectedIndex >= 0)
        {
            GlobalVariable.ConfigVariable.ConfigDownloadSourceEnum =
                (DownloadSourceHandler.DownloadSourceEnum)DownloadSourceSelectComboBox.SelectedIndex;
            DefaultConfigHandler.SetConfig(GlobalConstants.ConfigDownloadSourceNode,
                GlobalVariable.ConfigVariable.ConfigDownloadSourceEnum);
        }
    }

    private void RefreshLocalJavaComboBoxFullSearch_OnClick(object? sender, RoutedEventArgs e)
    {
        GameHandler.RefreshLocalJavaList(true);
    }

    private void RefreshLocalJavaComboBoxNormalSearch_OnClick(object? sender, RoutedEventArgs e)
    {
        GameHandler.RefreshLocalJavaList();
    }

    private void DownloadParallelismCountSlider_OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        if (DownloadParallelismCountSlider != null)
        {
            GlobalVariable.ConfigVariable.ConfigDownloadParallelismCount = (int)DownloadParallelismCountSlider.Value;
            DefaultConfigHandler.SetConfig(GlobalConstants.ConfigDownloadParallelismCountNode,
                GlobalVariable.ConfigVariable.ConfigDownloadParallelismCount);
        }
    }

    private void DownloadSegmentsForLargeFileSlider_OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        if (DownloadSegmentsForLargeFileSlider != null)
        {
            GlobalVariable.ConfigVariable.ConfigDownloadThreadCount = (int)DownloadSegmentsForLargeFileSlider.Value;
            DefaultConfigHandler.SetConfig(GlobalConstants.ConfigDownloadThreadCountNode,
                GlobalVariable.ConfigVariable.ConfigDownloadThreadCount);
        }
    }

    private void DownloadTotalRetrySlider_OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        if (DownloadTotalRetrySlider != null)
        {
            GlobalVariable.ConfigVariable.ConfigDownloadRetryCount = (int)DownloadTotalRetrySlider.Value;
            DefaultConfigHandler.SetConfig(GlobalConstants.ConfigDownloadRetryCountNode,
                GlobalVariable.ConfigVariable.ConfigDownloadRetryCount);
        }
    }

    private void GameGcTypeSelectComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (GameGcTypeSelectComboBox != null)
        {
            GlobalVariable.ConfigVariable.ConfigGameGcType = (GcType)GameGcTypeSelectComboBox.SelectedIndex;
            DefaultConfigHandler.SetConfig(GlobalConstants.ConfigGameGcTypeNode,
                GlobalVariable.ConfigVariable.ConfigGameGcType);
        }
    }

    private void ResolutionDigitsOnly_OnTextChanging(object? sender, TextChangingEventArgs e)
    {
        var textBox = (sender as TextBox)!;
        textBox.Text = Regex.Replace(textBox.Text!, "[^0-9]", "");
        if (textBox.Text.Equals(""))
            textBox.Text = "1";
        switch (textBox.Name)
        {
            case "GameResolutionWidthTextBox":
                GlobalVariable.ConfigVariable.ConfigGameResolutionWidth = Convert.ToUInt32(textBox.Text);
                DefaultConfigHandler.SetConfig(GlobalConstants.ConfigGameResolutionWidthNode,
                    GlobalVariable.ConfigVariable.ConfigGameResolutionWidth);
                break;
            case "GameResolutionHeightTextBox":
                GlobalVariable.ConfigVariable.ConfigGameResolutionHeight = Convert.ToUInt32(textBox.Text);
                DefaultConfigHandler.SetConfig(GlobalConstants.ConfigGameResolutionHeightNode,
                    GlobalVariable.ConfigVariable.ConfigGameResolutionHeight);
                break;
        }
    }
}