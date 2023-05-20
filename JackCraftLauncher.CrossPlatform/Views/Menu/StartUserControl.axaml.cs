using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JackCraftLauncher.CrossPlatform.Views.Menu;

public partial class StartUserControl : UserControl
{
    public StartUserControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}