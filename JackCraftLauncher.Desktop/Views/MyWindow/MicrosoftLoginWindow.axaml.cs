using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Xilium.CefGlue.Avalonia;

namespace JackCraftLauncher.Desktop.Views.MyWindow;

public partial class MicrosoftLoginWindow : Window
{
    private AvaloniaCefBrowser _browser;

    public MicrosoftLoginWindow()
    {
        InitializeComponent();
    }

    public string Code { get; set; } = "";

    private void MicrosoftLoginWindow_OnLoaded(object? sender, RoutedEventArgs e)
    {
        _browser = new AvaloniaCefBrowser();
        _browser.Address =
            "https://login.live.com/oauth20_authorize.srf%20?client_id=00000000402b5328%20&response_type=code%20&scope=service%3A%3Auser.auth.xboxlive.com%3A%3AMBI_SSL%20&redirect_uri=https%3A%2F%2Flogin.live.com%2Foauth20_desktop.srf";
        _browser.IsVisible = false;
        _browser.AddressChanged += (sender, address) =>
        {
            var browser = (AvaloniaCefBrowser)sender;
            if (address.ToString().StartsWith("https://login.live.com/oauth20_desktop.srf?code="))
            {
                var code = address.Split('?')[1].Split('&')[0].Split('=')[1];
                Code = code;
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    if (IsVisible)
                        Close();
                });
            }
        };
        _browser.LoadEnd += (sender, args) =>
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (IsVisible)
                    _browser.IsVisible = true;
            });
        };
        WebBrowserWrapper.Child = _browser;
    }
}