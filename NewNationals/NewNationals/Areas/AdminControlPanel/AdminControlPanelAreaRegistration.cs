using System.Web.Mvc;

namespace NewNationals.Areas.AdminControlPanel
{
    public class AdminControlPanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdminControlPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminControlPanel_default",
                "AdminControlPanel/{controller}/{action}/{id}",
                new { action = "Index", controller = "Default", id = UrlParameter.Optional }
            );
        }
    }
}