using System.Web.Http;
using System.Web.Mvc;

namespace PLA.Areas.HelpPage
{
    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "HelpPage",
               "Admin/{controller}/{action}/{apiId}",
               new { controller = "Login", action = "AdminLogin", apiId = UrlParameter.Optional });

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}