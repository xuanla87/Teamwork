using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.Commons;
using ClassLibrary.Models;
using ClassLibrary.Services;


namespace NewNationals.Areas.AdminControlPanel.Controllers
{
    public class RoleController : Controller
    {
        // GET: AdminControlPanel/Role
        public ActionResult Index()
        {
            return View();
        }
    }
}