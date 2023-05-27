using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Styling;
using JackCraftLauncher.Desktop.Class.Launch;
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
            switch (ThemeSelectComboBox.SelectedIndex)
            {
                case 0:
                    ThemeHandler.SetTheme(ThemeVariant.Light);
                    break;
                case 1:
                    ThemeHandler.SetTheme(ThemeVariant.Dark);
                    break;
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