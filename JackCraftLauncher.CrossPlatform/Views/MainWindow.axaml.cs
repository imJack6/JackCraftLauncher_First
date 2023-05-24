using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using JackCraftLauncher.CrossPlatform.Class.Launch;

namespace JackCraftLauncher.CrossPlatform.Views;

public partial class MainWindow : Window
{
    public static MainWindow Instance;

    public MainWindow()
    {
        Instance = this;
        InitializeComponent();
    }

    #region 菜单按钮事件

    private void MenuRadioButton_Click(object sender, RoutedEventArgs e)
    {
        var checkedButton = (RadioButton)sender;
        switch (checkedButton.Name)
        {
            case "UserRadioButton":
                MenuTabControl.SelectedIndex = 0;
                break;
            case "StartRadioButton":
                MenuTabControl.SelectedIndex = 1;
                break;
            case "DownloadRadioButton":
                MenuTabControl.SelectedIndex = 2;
                break;
            case "SettingsRadioButton":
                MenuTabControl.SelectedIndex = 3;
                break;
        }
    }

    #endregion

    #region 窗体控制按钮事件

    private void TitleBar_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (e.ClickCount <= 1)
            BeginMoveDrag(e);
        else if (e.ClickCount == 2)
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        if (WindowState == WindowState.Maximized)
            WindowState = WindowState.Normal;
        else if (WindowState == WindowState.Normal)
            WindowState = WindowState.Maximized;
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    #endregion
}