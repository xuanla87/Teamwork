using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewNationals.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Pages(string slug)
        {
            return PartialView();
        }

        public ActionResult Details(string slug)
        {
            return PartialView();
        }

        public ActionResult Categories(string slug, int? page)
        {
            return PartialView();
        }

        public ActionResult PageError()
        {
            return PartialView();
        }

        public ActionResult Tags(string slug)
        {
            return PartialView();
        }

        public ActionResult Default(string slug, int? page)
        {
            if (slug == null)
            {

            }
            else
            {

            }
            return View();
        }
    }
}