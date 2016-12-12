using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewNationals.Areas.AdminControlPanel.Controllers
{
    public class HomeController : BaseController
    {
        // GET: AdminControlPanel/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}