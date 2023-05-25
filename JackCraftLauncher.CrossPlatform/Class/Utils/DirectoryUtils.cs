using System;
using System.IO;
using System.Runtime.InteropServices;

namespace JackCraftLauncher.CrossPlatform.Class.Utils
{
    public class DirectoryUtils
    {
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    throw new Exception($"创建文件夹 {path} 失败: {ex}");
                }
        }

        public static string GetSystemUserDirectory()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "/Users/" + Environment.UserName;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return "/home/" + Environment.UserName;
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }
    }
}
