using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ClassLibrary.Commons;

namespace NewNationals.Areas.AdminControlPanel.Controllers
{
    public class BaseController : Controller
    {
        // GET: AdminControlPanel/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = Session[CommonsHelper.SessionAdminCp];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new {controller = "Login", action = "UserLogin", area = "AdminControlPanel"}));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}