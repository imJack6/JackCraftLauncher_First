using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace JackCraftLauncher.CrossPlatform.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    #region 菜单按钮事件
    private void MenuRadioButton_Click(object sender, RoutedEventArgs e)
    {
        RadioButton checkedButton = (RadioButton)sender;
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
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
    }
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        if (this.WindowState == WindowState.Maximized)
            this.WindowState = WindowState.Normal;
        else if (this.WindowState == WindowState.Normal)
            this.WindowState = WindowState.Maximized;
    }
    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }
    #endregion
}