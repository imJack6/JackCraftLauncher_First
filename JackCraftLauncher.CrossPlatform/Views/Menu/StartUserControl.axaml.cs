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
using ProjBobcat.Class.Model;

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
        if (SelectJavaGameVersionComboBox.SelectedIndex == -1)
            await MyDialog.ShowDialog(MainWindow.Instance.RootGrid,"提示","没有选择启动版本");
        else
            if (SettingsUserControl.Instance.StartJavaSelectComboBox.SelectedIndex == -1)
                await MyDialog.ShowDialog(MainWindow.Instance.RootGrid,"提示","没有选择启动 Java");
            else
            {
                GameHandler.StartGame(GlobalVariable.LocalGameList[SelectJavaGameVersionComboBox.SelectedIndex]);
            }
    }
    private void RefreshLocalGameComboListButton_OnClick(object? sender, RoutedEventArgs e)
    {
        GameHandler.RefreshLocalGameList();
    }
}