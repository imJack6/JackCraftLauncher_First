using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JackCraftLauncher.Desktop.Views.Menu;

public partial class UserUserControl : UserControl
{
    public UserUserControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}