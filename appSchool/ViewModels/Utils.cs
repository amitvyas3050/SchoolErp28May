using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DevExpress.Web.Internal;
using DevExpress.Web;

public static class Utils
{
    const string
        CurrentThemeCookieKey = "theme",
        DefaultTheme = "RedWin";

    static HttpContext Context
    {
        get { return HttpContext.Current; }
    }

    static HttpRequest Request
    {
        get { return Context.Request; }
    }

    public static string CurrentTheme
    {
        get
        {
            if (Request.Cookies[CurrentThemeCookieKey] != null)
                return HttpUtility.UrlDecode(Request.Cookies[CurrentThemeCookieKey].Value);
            return DefaultTheme;
        }
    }
}
