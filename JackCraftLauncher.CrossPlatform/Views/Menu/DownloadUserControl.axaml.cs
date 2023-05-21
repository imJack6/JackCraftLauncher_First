using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JackCraftLauncher.CrossPlatform.Views.Menu;

public partial class DownloadUserControl : UserControl
{
    public DownloadUserControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}