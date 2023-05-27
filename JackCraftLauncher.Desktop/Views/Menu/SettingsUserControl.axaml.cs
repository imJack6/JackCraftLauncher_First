using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Styling;
using JackCraftLauncher.Desktop.Class;
using JackCraftLauncher.Desktop.Class.ConfigHandle;
using JackCraftLauncher.Desktop.Class.Launch;
using JackCraftLauncher.Desktop.Class.Model;
using JackCraftLauncher.Desktop.Views.Themes;

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
}