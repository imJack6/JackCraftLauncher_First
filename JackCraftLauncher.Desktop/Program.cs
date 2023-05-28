using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Avalonia;
using Avalonia.Media;

namespace JackCraftLauncher.Desktop;

internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        try
        {
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
        catch (Exception e)
        {
            var fileName = "crash_report_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            using (var sw = new StreamWriter(filePath))
            {
                sw.WriteLine($"程序发生了致命错误，这是于 {DateTime.Now} 生成的崩溃日志。");
                sw.WriteLine(
                    $"The program encountered a fatal error, which is a crash log generated on {DateTime.Now}.");
                sw.WriteLine("-------------------------------");
                sw.WriteLine("Application: " + AppDomain.CurrentDomain.FriendlyName);
                sw.WriteLine("Time: " + DateTime.Now);
                sw.WriteLine("Path: " + filePath);
                sw.WriteLine();
                sw.WriteLine("Message: " + e.Message);
                sw.WriteLine(e.ToString());
                sw.WriteLine("-------------------------------");
                sw.WriteLine("如果此次崩溃是意外，请将此文件的内容无修改的反馈给开发者。");
                sw.WriteLine(
                    "If the crash was unexpected, please provide feedback to the developer regarding the unmodified content of this file.");

                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }

            throw;
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .With(new FontManagerOptions
            {
                DefaultFamilyName = "avares://JackCraftLauncher.Desktop/Assets/Fonts/JetBrainsMono-Medium.ttf#",
                FontFallbacks = new[]
                {
                    new FontFallback
                    {
                        FontFamily = new FontFamily("avares://AvaloniaTest/Assets/Fonts/MSYHMONO.ttf#")
                    }
                }
            });
    }
}