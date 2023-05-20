using System.Linq;
using JackCraftLauncher.CrossPlatform.Views.Menu;

namespace JackCraftLauncher.CrossPlatform.Class.Launch;

public class GameHandler
{
    public static void RefreshLocalGameList()
    {
        GlobalVariable.LocalGameList = GlobalVariable.LaunchCore.GetCore().VersionLocator.GetAllGames().ToList();
        StartUserControl.Instance.SelectJavaGameVersionComboBox.ItemsSource = GlobalVariable.LocalGameList.Select(x => x.Name ?? x.Id);
    }
}