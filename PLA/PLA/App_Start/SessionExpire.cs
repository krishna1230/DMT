using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLA.App_Start
{
    public class SessionExpireAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            HttpCookie langCookie1 = HttpContext.Current.Request.Cookies["EmailId"];
            HttpCookie langCookie2 = HttpContext.Current.Request.Cookies["Password"];
            HttpCookie langCookie3 = HttpContext.Current.Request.Cookies["AdminId"];
            HttpCookie langCookie4 = HttpContext.Current.Request.Cookies["AdminName"];
           
            if (langCookie1 != null)
            {
                HttpContext.Current.Session["EmailId"] = langCookie1.Value;
            }
            if (langCookie2 != null)
            {
                HttpContext.Current.Session["Password"] = langCookie2.Value;
            }
            if (langCookie3 != null)
            {
                HttpContext.Current.Session["AdminId"] = langCookie3.Value;
            }
            if (langCookie4 != null)
            {
                HttpContext.Current.Session["AdminName"] = langCookie3.Value;
            }
            


            // check  sessions here
            if (HttpContext.Current.Session["AdminId"] == null)
            {
                filterContext.Result = new RedirectResult("../Login/AdminLogin");
                return;
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
    public class SessionExpireUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            HttpCookie langCookie1 = HttpContext.Current.Request.Cookies["Name"];
            HttpCookie langCookie2 = HttpContext.Current.Request.Cookies["Id"];

            if (langCookie1 != null)
            {
                HttpContext.Current.Session["Name"] = langCookie1.Value;
            }
            if (langCookie2 != null)
            {
                HttpContext.Current.Session["Id"] = langCookie2.Value;
            }

            // check  sessions here
            //if (HttpContext.Current.Session["Name"] == null)
            //{
            //    filterContext.Result = new RedirectResult("../Home/Index");
            //    return;
            //}

            base.OnActionExecuting(filterContext);
        }
    }
}