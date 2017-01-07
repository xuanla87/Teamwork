using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewNationals.Controllers
{
    public class LicenseController : Controller
    {
        // GET: License
        public ActionResult CheckLicense()
        {
            return View();
        }
    }
}