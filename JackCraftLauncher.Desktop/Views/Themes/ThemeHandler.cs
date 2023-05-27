using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;

namespace JackCraftLauncher.Desktop.Views.Themes;

public class ThemeHandler
{
    public static void SetTheme(ThemeVariant theme)
    {
        if (theme.Equals(ThemeVariant.Dark))
        {
            //ResourceInclude resourceInclude = new ResourceInclude(new Uri("/Views/Themes/DarkResourceDictionary.axaml"));
            var resourceDictionary = new ResourceDictionary();
            var resourceInclude =
                new ResourceInclude(
                    new Uri("avares://JackCraftLauncher.Desktop/Views/Themes/LightResourceDictionary.axaml"))
                {
                    Source = new Uri(
                        "avares://JackCraftLauncher.Desktop/Views/Themes/DarkResourceDictionary.axaml") // 新的资源路径
                };
            resourceDictionary.MergedDictionaries.Add(resourceInclude);
            Application.Current.Resources = resourceDictionary;
            //Application.Current!.Resources.MergedDictionaries[0] = resourceInclude;
        }
        else if (theme.Equals(ThemeVariant.Light))
        {
            var resourceDictionary = new ResourceDictionary();
            var resourceInclude =
                new ResourceInclude(
                    new Uri("avares://JackCraftLauncher.Desktop/Views/Themes/DarkResourceDictionary.axaml"))
                {
                    Source = new Uri(
                        "avares://JackCraftLauncher.Desktop/Views/Themes/LightResourceDictionary.axaml") // 新的资源路径
                };
            resourceDictionary.MergedDictionaries.Add(resourceInclude);
            Application.Current.Resources = resourceDictionary;
            //Application.Current!.Resources.MergedDictionaries[0] = resourceInclude;
        }

        Application.Current!.RequestedThemeVariant = theme;
    }

    public static ThemeVariant GetTheme()
    {
        return Application.Current!.RequestedThemeVariant!;
    }
}