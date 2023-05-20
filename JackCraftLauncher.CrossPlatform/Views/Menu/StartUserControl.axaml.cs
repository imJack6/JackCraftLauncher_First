using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Avalonia.Styling;
using JackCraftLauncher.CrossPlatform.Class;
using JackCraftLauncher.CrossPlatform.Class.Launch;
using JackCraftLauncher.CrossPlatform.Views.MyControls;
using JackCraftLauncher.CrossPlatform.Views.MyWindow;
using JackCraftLauncher.CrossPlatform.Views.Themes;

namespace JackCraftLauncher.CrossPlatform.Views.Menu;

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
        ViewLogWindow logWindow = new ViewLogWindow();
        logWindow.Show();
        logWindow.AddLog("[00:01:00][Render thread/Info]: Test Message");
        logWindow.AddLog("[00:01:00][Render thread/ERROR]: test err");
        logWindow.AddLog("[00:01:00][Render thread/warn]: test warning");
        logWindow.AddLog("[00:01:00][aa");
        if (ThemeHandler.GetTheme().Equals(ThemeVariant.Dark))
            ThemeHandler.SetTheme(ThemeVariant.Light);
        else if (ThemeHandler.GetTheme().Equals(ThemeVariant.Light))
            ThemeHandler.SetTheme(ThemeVariant.Dark);
        if (SelectJavaGameVersionComboBox.SelectedIndex == -1)
        {
            await MyDialog.ShowDialog(MainWindow.Instance.RootGrid,"提示","没有选择启动版本");
            return;
        }
        else
        {
        }
    }
    private void RefreshLocalGameComboListButton_OnClick(object? sender, RoutedEventArgs e)
    {
        GameHandler.RefreshLocalGameList();
    }
}