using System;
using ProjBobcat.DefaultComponent.Launch;
using ProjBobcat.DefaultComponent.Launch.GameCore;
using ProjBobcat.DefaultComponent.Logging;

namespace JackCraftLauncher.Desktop.Class.Launch;

public class LaunchCoreHandler
{
    private DefaultGameCore _core = new();

    public LaunchCoreHandler()
    {
        Init();
    }

    public void Init()
    {
        var clientToken = new Guid("11451419-1981-0114-5141-919810114514");
        var rootPath = "JCL/.minecraft/";
        _core = new DefaultGameCore
        {
            ClientToken = clientToken,
            RootPath = rootPath,
            VersionLocator = new DefaultVersionLocator(rootPath, clientToken)
            {
                LauncherProfileParser = new DefaultLauncherProfileParser(rootPath, clientToken),
                LauncherAccountParser = new DefaultLauncherAccountParser(rootPath, clientToken)
            },
            GameLogResolver = new DefaultGameLogResolver()
        };
    }

    public DefaultGameCore GetCore()
    {
        return _core;
    }

    public void SetCore(DefaultGameCore newCore)
    {
        _core = newCore;
    }
}