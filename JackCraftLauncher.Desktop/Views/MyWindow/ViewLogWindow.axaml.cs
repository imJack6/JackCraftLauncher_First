using System;
using System.Linq;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using ProjBobcat.Class.Model;

namespace JackCraftLauncher.Desktop.Views.MyWindow;

public partial class ViewLogWindow : Window
{
    public LaunchResult LaunchResult;

    public ViewLogWindow()
    {
        InitializeComponent();
    }

    public void AddLog(string log)
    {
        var warpPanel = new WrapPanel();

        var prefixInput = log.Split(new char[] { ' ' }, 2);
        var logContent = log;
        if (prefixInput.Length > 1 && prefixInput[0].StartsWith("[") && prefixInput[0].EndsWith("]"))
        {
            logContent = prefixInput[1];
            string prefix = prefixInput[0] + " ";
            var prefixOutput = Regex.Split(prefix, @"(\[)|(\] )")
                .Where(match => !string.IsNullOrWhiteSpace(match))
                .ToList();
            for (var i = 0; i < prefixOutput.Count; i++)
            {
                var textBlock = new TextBlock { Text = prefixOutput[i], FontSize = 15 };
                switch (i)
                {
                    case 1:
                        textBlock.Foreground = this.FindResource("DefaultLogTimeForeground") as IBrush;
                        break;
                    default:
                        textBlock.Foreground = this.FindResource("DefaultForeground") as IBrush;
                        break;
                }

                warpPanel.Children.Add(textBlock);
            }
        }

        if (log.StartsWith("[游戏]"))
        {
            var output = Regex.Split(logContent, @"(\[)|(\] \[)|(/)|(]: )")
                .Where(match => !string.IsNullOrWhiteSpace(match))
                .ToList();
            // 结果：
            // output[0] = "["
            // output[1] = "00:00:00"
            // output[2] = "] ["
            // output[3] = "Render thread"
            // output[4] = "/"
            // output[5] = "Info"
            // output[6] = "]: "
            // output[7] = "Test Message"

            for (var i = 0; i < output.Count; i++)
            {
                var textBlock = new TextBlock { Text = output[i], FontSize = 15 };
                switch (i)
                {
                    case 1:
                        textBlock.Foreground = this.FindResource("DefaultLogTimeForeground") as IBrush;
                        break;
                    case 3:
                        textBlock.Foreground = this.FindResource("DefaultLogThreadForeground") as IBrush;
                        break;
                    case 5:
                        if (string.Equals(output[i], "INFO", StringComparison.OrdinalIgnoreCase))
                            textBlock.Foreground = this.FindResource("DefaultLogInfoForeground") as IBrush;
                        else if (string.Equals(output[i], "WARN", StringComparison.OrdinalIgnoreCase))
                            textBlock.Foreground = this.FindResource("DefaultLogWarnForeground") as IBrush;
                        else if (string.Equals(output[i], "ERROR", StringComparison.OrdinalIgnoreCase))
                            textBlock.Foreground = this.FindResource("DefaultLogErrorForeground") as IBrush;
                        break;
                    default:
                        textBlock.Foreground = this.FindResource("DefaultForeground") as IBrush;
                        break;
                }

                warpPanel.Children.Add(textBlock);
            }
        }
        else
        {
            var textBlock = new TextBlock { Text = prefixInput[1], FontSize = 15 };
            warpPanel.Children.Add(textBlock);
        }

        LogListBox.Items.Add(warpPanel);
        
        var listboxScroll = (ScrollViewer)LogListBox.Scroll!;
        if (AutomaticScrollingToggleButton.IsChecked == true)
            listboxScroll.ScrollToEnd();
    }
    private void LogListBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (LogListBox.SelectedIndex != -1)
            LogListBox.SelectedIndex = -1;
    }

    private void CloseGame_OnClick(object? sender, RoutedEventArgs e)
    {
        if (LaunchResult != null && LaunchResult.GameProcess != null) LaunchResult.GameProcess.Kill();
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