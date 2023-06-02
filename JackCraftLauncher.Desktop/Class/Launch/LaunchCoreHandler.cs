using System;
using ProjBobcat.Class.Model.MicrosoftAuth;
using ProjBobcat.DefaultComponent.Authenticator;
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
        MicrosoftAuthenticator.Configure(new MicrosoftAuthenticatorAPISettings
        {
            ClientId = "7a5e9883-e204-45da-8e98-b47c5370c5c8",
            TenentId = "consumers",
            Scopes = new[] { "XboxLive.signin", "offline_access", "openid", "profile", "email" }
        });
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