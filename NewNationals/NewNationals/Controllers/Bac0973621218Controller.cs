using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ClassLibrary.Commons;

namespace NewNationals.Controllers
{
    public class Bac0973621218Controller : Controller
    {
        // GET: Bac0973621218
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = Session[CommonsHelper.SessionAdminCp];
            DateTime dtCheck=new DateTime();
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "UserLogin", area = "AdminControlPanel" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}