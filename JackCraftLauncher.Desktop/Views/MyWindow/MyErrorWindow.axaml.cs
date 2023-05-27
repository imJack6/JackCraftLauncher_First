using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using JackCraftLauncher.Desktop.Class.Model.ErrorModels;

namespace JackCraftLauncher.Desktop.Views.MyWindow;

public partial class MyErrorWindow : Window
{
    public MyErrorWindow(ErrorResult errorResult)
    {
        InitializeComponent();
        ErrorMessageTextBox.Text = $"错误: {errorResult.ErrorMessage.Error}{Environment.NewLine}";
        ErrorMessageTextBox.Text += $"错误类型: {errorResult.ErrorType}{Environment.NewLine}";
        ErrorMessageTextBox.Text += $"错误信息: {errorResult.ErrorMessage.ErrorMsg}{Environment.NewLine}";
        ErrorMessageTextBox.Text += $"修复措施: {errorResult.ErrorMessage.Fix}{Environment.NewLine}";
        ErrorMessageTextBox.Text += $"----------------------------------{Environment.NewLine}";
        ErrorMessageTextBox.Text += $"错误详情: {Environment.NewLine}{FormatException(errorResult.ErrorMessage.Exception)}";
        Title = $"JackCraft Launcher 出现了错误 - {errorResult.ErrorMessage.Error} - {errorResult.ErrorType}";
    }

    public static string FormatException(Exception e)
    {
        var output = "";
        output += "Application: " + AppDomain.CurrentDomain.FriendlyName + Environment.NewLine;
        output += "Time: " + DateTime.Now + Environment.NewLine + Environment.NewLine;
        output += "Message: " + e.Message + Environment.NewLine;
        output += e.ToString();
        return output;
    }

    public static MyErrorWindow CreateErrorWindow(ErrorType errorType, string error, string errorMsg, string fix, Exception ex)
    {
        var errorWindow = new MyErrorWindow(new ErrorResult
            {
                ErrorType = errorType,
                ErrorMessage = new ErrorMessage
                {
                    Error = error,
                    ErrorMsg = errorMsg,
                    Fix = fix,
                    Exception = ex
                }
            }
        );
        errorWindow.Show();
        return errorWindow;
    }
    
    private void Close_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private async void Copy_OnClick(object? sender, RoutedEventArgs e)
    {
        await Clipboard!.SetTextAsync(ErrorMessageTextBox.Text);
    }
}