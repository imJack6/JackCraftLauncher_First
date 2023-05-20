using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JackCraftLauncher.CrossPlatform.Views.Menu;

public partial class SettingsUserControl : UserControl
{
    public SettingsUserControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}