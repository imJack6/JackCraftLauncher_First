using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using JackCraftLauncher.Desktop.Class.Model.ErrorModels;
using JackCraftLauncher.Desktop.Views;
using JackCraftLauncher.Desktop.Views.MyWindow;

namespace JackCraftLauncher.Desktop.Class.MicrosoftLogin;

public class MicrosoftLogger
{
    /// <summary>
    /// 打开一个登入窗口，等待登入并返回Code,如果返回了空值代表用户登入失败或关闭了窗口
    /// </summary>
    /// <returns>Code</returns>
    public static async Task<string> OpenLoginView()
    {
        var view = new MicrosoftLoginWindow();
        try
        {
            await view.ShowDialog(MainWindow.Instance);
            return view.Code;
        }
        catch (Exception)
        {
            return "";
        }
    }
    
    /// <summary>
    /// 得到OAuth Token和Refresh Token 当然我还是愿意直接用refreshtoken
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static async Task<string[]> GetMicrosoftOAuthToken(string code)
    {
        string[] resultstrs = new string[5];
        var url = "https://login.live.com/oauth20_token.srf";
        Dictionary<string, string> dic = new()
        {
            { "client_id", "00000000402b5328" },
            { "code", code },
            { "grant_type", "authorization_code" },
            { "redirect_uri", "https://login.live.com/oauth20_desktop.srf" },
            { "scope", "service::user.auth.xboxlive.com::MBI_SSL" }
        };
        var response = await Utils.HttpUtils.Get(url, dic);
        var jobj = JsonDocument.Parse(response);
        try
        {
            resultstrs[0] = jobj.RootElement.GetProperty("access_token").GetString() ?? throw new Exception("获取MS OAuth Token失败");
            resultstrs[1] = jobj.RootElement.GetProperty("refresh_token").GetString() ?? throw new Exception("获取Refresh Token失败");
            resultstrs[2] = Convert.ToString(jobj.RootElement.GetProperty("expires_in").GetInt32()) ?? throw new Exception("获取Expires In失败");
            resultstrs[3] = jobj.RootElement.GetProperty("scope").GetString() ?? throw new Exception("获取Scope失败");
            resultstrs[4] = jobj.RootElement.GetProperty("token_type").GetString() ?? throw new Exception("获取Token Type失败");
            return resultstrs;
        } catch (Exception ex)
        {
            MyErrorWindow.CreateErrorWindow(ErrorType.InternalError,"获取值时出现了错误",ex.Message,"联系开发者",ex);
            return null!;
        }


    }
}