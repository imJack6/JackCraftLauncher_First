using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using JackCraftLauncher.Desktop.Class.Utils;

namespace JackCraftLauncher.Desktop.Views;

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

    private void CrashTest_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new Exception("这是一个测试异常 - throw new Exception 测试");
    }
    private void Test_OnClick(object? sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog();
        dialog.Filters.Add(new FileDialogFilter() { Name = "所有文件", Extensions = { "*" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "文本文件", Extensions = { "txt" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "图片文件", Extensions = { "png", "jpg", "jpeg", "bmp" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "可执行文件", Extensions = { "exe" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "压缩文件", Extensions = { "zip", "rar", "7z" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "音频文件", Extensions = { "mp3", "wav", "flac" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "视频文件", Extensions = { "mp4", "avi", "mkv" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "其他文件", Extensions = { "dat", "bin", "iso" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "所有文件", Extensions = { "*" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "文本文件", Extensions = { "txt" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "图片文件", Extensions = { "png", "jpg", "jpeg", "bmp" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "可执行文件", Extensions = { "exe" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "压缩文件", Extensions = { "zip", "rar", "7z" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "音频文件", Extensions = { "mp3", "wav", "flac" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "视频文件", Extensions = { "mp4", "avi", "mkv" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "其他文件", Extensions = { "dat", "bin", "iso" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = "所有文件", Extensions = { "*" } });
        dialog.Filters.Add(new FileDialogFilter() { Name = DirectoryUtils.GetSystemUserDirectory(), Extensions = { "*" } });
        dialog.ShowAsync(this);
    }

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