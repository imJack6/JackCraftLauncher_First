using System.IO;
using JackCraftLauncher.CrossPlatform.Views.Menu;

namespace JackCraftLauncher.CrossPlatform.Class.Launch;

public class DownloadSourceHandler
{
    public enum DownloadSourceEnum
    {
        BMCL,
        MCBBS,
        Official
    }

    public enum DownloadTargetEnum
    {
        VersionInfoV1,
        VersionInfoV2,
        MinecraftJar,
        MinecraftJson,
        MinecraftLibraries,
        MinecraftAssets,
        MinecraftAssetsIndex
    }

    public static string GetDownloadSource(DownloadTargetEnum target, DownloadSourceEnum? source, string? minecraftVersion = "1.0")
    {
        if (source == null)
        {
            switch (SettingsUserControl.Instance.DownloadSourceSelectComboBox.SelectedIndex)
            {
                case 0:
                    source = DownloadSourceEnum.MCBBS;
                    break;
                case 1:
                    source = DownloadSourceEnum.BMCL;
                    break;
                case 2:
                    source = DownloadSourceEnum.Official;
                    break;
                default:
                    source = DownloadSourceEnum.BMCL;
                    break;
            }
        }
        var baseUrl = "";
        switch (source)
        {
            case DownloadSourceEnum.BMCL:
                baseUrl = "https://bmclapi2.bangbang93.com";
                break;
            case DownloadSourceEnum.MCBBS:
                baseUrl = "https://download.mcbbs.net";
                break;
            case DownloadSourceEnum.Official:
                if (DownloadTargetEnum.MinecraftJar == target || DownloadTargetEnum.MinecraftJson == target)
                    baseUrl = "https://download.mcbbs.net";
                else if (DownloadTargetEnum.MinecraftLibraries == target)
                    baseUrl = "https://libraries.minecraft.net";
                else if (DownloadTargetEnum.MinecraftAssets == target)
                    baseUrl = "http://resources.download.minecraft.net";
                else if (DownloadTargetEnum.MinecraftAssetsIndex == target)
                    baseUrl = "https://launchermeta.mojang.com";
                else
                    baseUrl = "http://launchermeta.mojang.com";
                break;
            default:
                throw new InvalidDataException($"Selected mirror field {source} does not found");
        }

        switch (target)
        {
            case DownloadTargetEnum.VersionInfoV1:
                return $"{baseUrl}/mc/game/version_manifest.json";
            case DownloadTargetEnum.VersionInfoV2:
                return $"{baseUrl}/mc/game/version_manifest_v2.json";
            case DownloadTargetEnum.MinecraftJar:
                return $"{baseUrl}/version/{minecraftVersion}/client";
            case DownloadTargetEnum.MinecraftJson:
                return $"{baseUrl}/version/{minecraftVersion}/json";
            case DownloadTargetEnum.MinecraftLibraries:
                if (DownloadSourceEnum.Official == source)
                    return $"{baseUrl}/";
                else
                    return $"{baseUrl}/maven/";
            case DownloadTargetEnum.MinecraftAssets:
                if (DownloadSourceEnum.Official == source)
                    return $"{baseUrl}/";
                else
                    return $"{baseUrl}/assets/";
            case DownloadTargetEnum.MinecraftAssetsIndex:
                return $"{baseUrl}/";
            default:
                throw new InvalidDataException($"Selected mirror field {source} does not found");
        }
    }

    public static string PistonMetaUrlHandle(DownloadSourceEnum source, string url)
    {
        var HandleString = url;
        var baseUrl = "";
        switch (source)
        {
            case DownloadSourceEnum.BMCL:
                baseUrl = "https://bmclapi2.bangbang93.com";
                break;
            case DownloadSourceEnum.MCBBS:
                baseUrl = "https://download.mcbbs.net";
                break;
            case DownloadSourceEnum.Official:
                baseUrl = "https://piston-meta.mojang.com";
                break;
            default:
                throw new InvalidDataException($"Selected mirror field {source} does not found");
        }

        HandleString.Replace("https://piston-meta.mojang.com", baseUrl);
        return HandleString;
    }
}