using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using JackCraftLauncher.CrossPlatform.Views.MyControls;
using Color = System.Drawing.Color;

namespace JackCraftLauncher.CrossPlatform.Views.MyWindow;

public partial class ViewLogWindow : Window
{
    public ViewLogWindow()
    {
        InitializeComponent();
    }

    public async void AddLog(string log)
    {
        try
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;

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

            List<string> output2 = new List<string>();
            // 使用正则表达式匹配出每个部分
            Regex regex2 = new Regex(@"(\[)|(\]\[)|(/)|(]: )");
            string[] matches = regex2.Split(log);
            foreach (string match2 in matches)
            {
                if (!string.IsNullOrEmpty(match2))
                {
                    output2.Add(match2);
                }
            }

            // 结果：
            // output2[0] = "["
            // output2[1] = "00:01:00"
            // output2[2] = "]["
            // output2[3] = "Render thread"
            // output2[4] = "/"
            // output2[5] = "Info"
            // output2[6] = "]: "
            // output2[7] = "Test Message"

            #endregion

            try
            {
                for (int i = 0; i < 8; i++)
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = output2[i];
                    if (i.Equals(0) || i.Equals(2) || i.Equals(4) || i.Equals(6) || i.Equals(7))
                        textBlock.Foreground = Brushes.White;
                    else if (i.Equals(1))
                        //textBlock.Foreground = Brushes.DarkGray;
                        textBlock.Foreground = this.FindResource("DefaultLogTimeForeground") as IBrush;
                    else if (i.Equals(3))
                        //textBlock.Foreground = Brushes.DarkGoldenrod;
                        textBlock.Foreground = this.FindResource("DefaultLogThreadForeground") as IBrush;
                    else if (i.Equals(5))
                        //textBlock.Foreground = Brushes.Yellow;
                        if (string.Equals(output2[i],"INFO",StringComparison.OrdinalIgnoreCase))
                            textBlock.Foreground = this.FindResource("DefaultLogInfoForeground") as IBrush;
                        else if (string.Equals(output2[i],"WARN",StringComparison.OrdinalIgnoreCase))
                            textBlock.Foreground = this.FindResource("DefaultLogWarnForeground") as IBrush;
                        else if (string.Equals(output2[i],"ERROR",StringComparison.OrdinalIgnoreCase))
                            textBlock.Foreground = this.FindResource("DefaultLogErrorForeground") as IBrush;
                    stackPanel.Children.Add(textBlock);
                }
            }
            catch (Exception) {}

            IEnumerable ien = LogListBox.Items;
            List<StackPanel> listStackPanel = ien.Cast<StackPanel>().ToList();
            listStackPanel.Add(stackPanel);
            //LogListBox.Items = listStackPanel;
            LogListBox.ItemsSource = listStackPanel;
            //await MyDialog.ShowDialog(MainWindow.Instance.RootGrid,"提示",$"Time: {time} \nThread: {thread} \nLevel: {level} \nMessage: {message}");
            //LogTextBox.Text += log + "\n";

        }
        catch (Exception)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;

            TextBlock textBlock = new TextBlock();
            textBlock.Text = log;
            stackPanel.Children.Add(textBlock);
            
            IEnumerable ien = LogListBox.Items;
            List<StackPanel> listStackPanel = ien.Cast<StackPanel>().ToList();
            listStackPanel.Add(stackPanel);
            //LogListBox.Items = listStackPanel;
            LogListBox.ItemsSource = listStackPanel;
        }
    }

}