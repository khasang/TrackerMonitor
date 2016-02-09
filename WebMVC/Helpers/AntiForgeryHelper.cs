using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace WebMVC.Helpers
{
    public static class AntiForgeryHelper
    {
        public static string GetAntiForgeryToken(this HtmlHelper htmlHelper)
        {
            HttpRequestBase request = htmlHelper.ViewContext.HttpContext.Request;

            string cookieToken, formToken;

            HttpCookieCollection cookies = request.Cookies;
            HttpCookie antiForgeryCookie = cookies[AntiForgeryConfig.CookieName];
            string oldCookieToken = antiForgeryCookie == null ? null : antiForgeryCookie.Value;

            AntiForgery.GetTokens(oldCookieToken, out cookieToken, out formToken);

            if (oldCookieToken == null)
                cookies.Add(new HttpCookie(AntiForgeryConfig.CookieName, cookieToken));
            else if (cookieToken != null)
                cookies[AntiForgeryConfig.CookieName].Value = cookieToken;

            return formToken;
        }
    }
}