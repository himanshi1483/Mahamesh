using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mahamesh
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LocalizationAttribute("en"), 0);
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class NoDirectAccessAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (filterContext.HttpContext.Request.UrlReferrer == null ||
         filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host)
                {
                    filterContext.Result = new RedirectToRouteResult(new
                                              RouteValueDictionary(new { controller = "Home", action = "Index" }));
                }
            }
        }


        public class LocalizationAttribute : ActionFilterAttribute
        {
            private string _DefaultLanguage = "en";

            public LocalizationAttribute(string defaultLanguage)
            {
                _DefaultLanguage = defaultLanguage;
            }

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                string lang = (string)filterContext.RouteData.Values["lang"] ?? _DefaultLanguage;
                if (lang != _DefaultLanguage)
                {
                    try
                    {
                        Thread.CurrentThread.CurrentCulture =
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
                    }
                    catch (Exception e)
                    {
                        throw new NotSupportedException(String.Format("ERROR: Invalid language code '{0}'.", lang));
                    }
                }
            }
        }
    }
}
