using System.Net;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using JackCraftLauncher.Desktop.Class.ConfigHandle;
using JackCraftLauncher.Desktop.Class.Launch;
using JackCraftLauncher.Desktop.Views;

namespace JackCraftLauncher.Desktop;

public class App : Application
{
    public override void Initialize()
    {
        ServicePointManager.DefaultConnectionLimit = 512;
        AvaloniaXamlLoader.Load(this);
        
        DefaultConfigHandler.LoadConfig();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new MainWindow();

        base.OnFrameworkInitializationCompleted();
    }
}