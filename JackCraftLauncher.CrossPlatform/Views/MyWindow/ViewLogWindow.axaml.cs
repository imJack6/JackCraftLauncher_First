using System;
using System.Linq;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;

namespace JackCraftLauncher.CrossPlatform.Views.MyWindow;

public partial class ViewLogWindow : Window
{
    public ViewLogWindow()
    {
        InitializeComponent();
    }
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
    public void AddLog(string log)
    {
        var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };

        #region 没有多余符号

        /*List<string> output1 = new List<string>();
        // 使用正则表达式匹配出每个部分
        Regex regex1 = new Regex(@"^\[(\d\d:\d\d:\d\d)\]\[(.+?)\/(.+?)\]: (.+)$");
        Match match1 = regex1.Match(log);
        if (match1.Success)
        {
            // 将匹配的结果依次添加到列表中
            output1.Add(match1.Groups[1].Value);
            output1.Add(match1.Groups[2].Value);
            output1.Add(match1.Groups[3].Value);
            output1.Add(match1.Groups[4].Value);
        }
        // 结果：
        // output1[0] = "00:01:00"
        // output1[1] = "Render thread"
        // output1[2] = "Info"
        // output1[3] = "Test Message"
        */

        #endregion

        #region 有多余符号

        var output = Regex.Split(log, @"(\[)|(\]\[)|(/)|(]: )")
            .Where(match => !string.IsNullOrWhiteSpace(match))
            .ToList();
        // 结果：
        // output[0] = "["
        // output[1] = "00:01:00"
        // output[2] = "]["
        // output[3] = "Render thread"
        // output[4] = "/"
        // output[5] = "Info"
        // output[6] = "]: "
        // output[7] = "Test Message"

        #endregion

        for (var i = 0; i < output.Count; i++)
        {
            var textBlock = new TextBlock { Text = output[i] };
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

            stackPanel.Children.Add(textBlock);
        }

        LogListBox.Items.Add(stackPanel);
    }
}