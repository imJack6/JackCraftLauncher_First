using Avalonia.Controls;
using JackCraftLauncher.CrossPlatform.Class.Launch;

namespace JackCraftLauncher.CrossPlatform.Views.Menu;

public partial class DownloadUserControl : UserControl
{
    public static DownloadUserControl? Instance;

    public DownloadUserControl()
    {
        Instance = this;
        InitializeComponent();
        GameHandler.RefreshLocalMinecraftDownloadList();
    }
}